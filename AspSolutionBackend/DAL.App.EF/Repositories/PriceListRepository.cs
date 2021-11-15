using System;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.ApiServices;
using DomainDTO = App.Domain.TravelModels;

namespace DAL.App.EF.Repositories
{
    public class TravelPricesRepository : BaseRepository<TravelPrices, AppDbContext>,
        ITravelPricesRepository
    {
        public TravelPricesRepository(AppDbContext ctx) : base(ctx)
        {
        }



        public async Task<bool> IsTravelPriceValid(Guid travelPriceId)
        {
            var result = await FirstOrDefaultAsync(travelPriceId);
            return result != null && result.ValidUntil > DateTime.Now;
        }

        private async Task<TravelPrices> RequestAndAddNewTravelPrices()
        {
            var res = await TravelPricesApi.GetCurrentTravelPrices();
            base.Add(res);
            return res;
        }


        protected static async Task<TravelPrices?> QueryTravelData(IQueryable<TravelPrices> query, DateTime startDate)
        {
            return await query
                .Where(priceList => priceList.ValidUntil > DateTime.Now)
                .Include(travelPrice => travelPrice.Legs)
                .ThenInclude(leg => leg.RouteInfo)
                .ThenInclude(route => route!.From)
                .Include(travelPrice => travelPrice.Legs)
                .ThenInclude(leg => leg.RouteInfo)
                .ThenInclude(route => route!.To)
                .Include(travelPrice => travelPrice.Legs)
                .ThenInclude(leg => leg.Providers)
                .ThenInclude(provider => provider.Company)
                .Where(travelPrices =>
                    travelPrices.Legs!.Any(leg => leg.Providers!.Any(provider => provider.FlightStart > startDate)))
                .FirstOrDefaultAsync();
        }
    }
}