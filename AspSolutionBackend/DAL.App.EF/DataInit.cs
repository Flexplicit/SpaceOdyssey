using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain;
using App.Domain.TravelModels;
using Contracts.DAL.Domain;
using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Services.ApiServices;

namespace DAL.App.EF
{
    public static class DataInit
    {
        public static void DropDatabase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }

        public static async Task SeedData(AppDbContext ctx)
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
    }
}