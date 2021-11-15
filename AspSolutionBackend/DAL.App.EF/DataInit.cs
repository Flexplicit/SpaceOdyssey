using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using App.Domain;
using App.Domain.TravelModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.ApiServices;
using Timer = System.Timers.Timer;

namespace DAL.App.EF
{
    public class DataInit
    {
        public static void DropDatabase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }

        public static async Task<TravelPrices> SeedData(AppDbContext ctx)
        {
            var currentTravelPrice = await TravelPricesApi.GetCurrentTravelPrices();
            var distinctCompanies = ExtractDistinctCompaniesFromTravelPrices(currentTravelPrice);


            RemoveCompaniesFromProviders(currentTravelPrice);

            var travelLegs = currentTravelPrice.Legs!;
            travelLegs.ForEach(leg => leg.TravelPricesId = currentTravelPrice.Id);

            await ctx.AddRangeAsync(travelLegs);
            currentTravelPrice.Legs = null;


            await ctx.AddRangeAsync(distinctCompanies);
            await ctx.AddAsync(currentTravelPrice);
            await ctx.SaveChangesAsync();
            return currentTravelPrice;
        }

        private static void RemoveCompaniesFromProviders(TravelPrices travelPrice)
        {
            foreach (var provider in travelPrice.Legs!.SelectMany(leg => leg.Providers!))
            {
                provider.Company = null;
            }
        }

        private static IEnumerable<Company> ExtractDistinctCompaniesFromTravelPrices(TravelPrices travelPrice)
        {
            return travelPrice
                .Legs!
                .SelectMany(leg => leg.Providers!
                    .Select(provider =>
                    {
                        provider.CompanyId = provider.Company!.Id;
                        return provider.Company!;
                    }))
                .Distinct(new ModelComparer<Company>())
                .ToList();
        }

        public static void UpdateData(IServiceProvider serviceProvider)
        {
            var timer = new Timer(910000);
            var autoEvent = new AutoResetEvent(true);


            timer.Elapsed += new ElapsedEventHandler(async (_, e) => await OnTimedEvent(autoEvent, e, serviceProvider));
            timer.Start();
        }

        private static async Task OnTimedEvent(object stateInfo, ElapsedEventArgs e, IServiceProvider services)
        {
            // AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            // autoEvent.Set();
            using var serviceScope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            await using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if (ctx == null) return;

            var currentPriceList = await GetCurrentPrices(ctx);
            if (currentPriceList.Count == 15)
            {
                Console.WriteLine("----Deleting old data----");
                var priceListToDelete = await GetTravelPriceWithAllPaths(currentPriceList[^1].Id, ctx);
                await RemoveLastPriceList(ctx, priceListToDelete);
                Console.WriteLine("----old Data deleted----");
            }

            var latestPriceList = currentPriceList.FirstOrDefault();
            if (latestPriceList != null)
            {
                if (DateTime.Now > latestPriceList.ValidUntil)
                {
                    Console.WriteLine("----Updating Database----");
                    var res = await SeedData(ctx);
                    Console.WriteLine("----Database database seeded----");

                    // change timer to new date
                }
            }
            else
            {
                await SeedData(ctx);
            }
        }

        private static async Task<TravelPrices> GetTravelPriceWithAllPaths(Guid id, AppDbContext ctx)
        {
            return await ctx.TravelPrices
                .Include(prices => prices.Legs)
                .ThenInclude(legs => legs.Providers)
                .ThenInclude(provider => provider.Company)
                .Include(prices => prices.Legs)
                .ThenInclude(legs => legs.RouteInfo)
                .ThenInclude(routeInfo => routeInfo!.From)
                .Include(prices => prices.Legs)
                .ThenInclude(legs => legs.RouteInfo)
                .ThenInclude(routeInfo => routeInfo!.To)
                .Include(prices => prices.Reservations)
                .ThenInclude(reservation => reservation.RouteInfoData)
                .Where(prices => prices.Id.Equals(id))
                .AsNoTracking()
                .FirstAsync();
        }

        //TODO: A more elegant approach would be better, refactor everything into repos and inject repo(via provider) not ctx
        private static async Task RemoveLastPriceList(DbContext ctx, TravelPrices priceList)
        {
            ctx.ChangeTracker.Clear();
            var distinctCompanies = ExtractDistinctCompaniesFromTravelPrices(priceList);
            var enumerable = distinctCompanies.ToList();
            enumerable.ForEach(x => x.Providers = null);
            ctx.RemoveRange(enumerable);

            priceList.Legs!.ForEach(leg =>
            {
                var fromPlanet = leg.RouteInfo!.From;
                var toPlanet = leg.RouteInfo.To;
                var routeInfo = leg.RouteInfo;
                leg.RouteInfo!.From = null;
                leg.RouteInfo!.To = null;

                leg.Providers = null;
                leg.RouteInfo = null;
                leg.TravelPrices = null;
                ctx.RemoveRange(leg, routeInfo, fromPlanet, toPlanet);
            });


            RemoveReservationsAndRouteInfoData(ctx, priceList);
            ctx.RemoveRange(priceList.Reservations!);
            ctx.Remove(priceList);
            await ctx.SaveChangesAsync();
        }


        private static void RemoveReservationsAndRouteInfoData(DbContext ctx, TravelPrices priceList)
        {
            foreach (var reservation in priceList.Reservations!)
            {
                reservation.RouteInfoData = null;
                reservation.TravelPrice = null;
                ctx.Remove(reservation);
            }
        }

        private static async Task<TravelPrices> GetLatestTravelPrice(AppDbContext ctx)
        {
            return await ctx.TravelPrices
                .AsNoTracking()
                .OrderByDescending(prices => prices.ValidUntil)
                .FirstAsync();
        }

        private static async Task<List<TravelPrices>> GetCurrentPrices(AppDbContext ctx)
        {
            return await ctx.TravelPrices
                .AsNoTracking()
                .OrderByDescending(prices => prices.ValidUntil)
                .ToListAsync();
        }
    }
}