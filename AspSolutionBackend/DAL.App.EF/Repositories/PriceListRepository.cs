using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.CustomRepositories;
using DAL.Base.EF.Repositories;
// using Dijkstra.NET.Graph;
// using Dijkstra.NET.ShortestPath;
using Graph;
using Graph.GraphModels;
using Microsoft.EntityFrameworkCore;
using PublicApiDTO.TravelModels.v1;
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


        public async Task<TravelPrices> GetLatestTravelPriceAsync()
        {
            var query = CreateQuery(true);

            var res = await query
                .Where(priceList => priceList.ValidUntil > DateTime.Now)
                .FirstOrDefaultAsync();

            if (res == null)
            {
                res = await RequestAndAddNewTravelPrices();
            }

            return res;
        }


        private async Task<TravelPrices> RequestAndAddNewTravelPrices()
        {
            var res = await TravelPricesApi.GetCurrentTravelPrices();

            // Add to db as well?
            // need some logic if over 15 pricelists exists, we must delete last.
            base.Add(res);


            return res;
        }


        protected static async Task<TravelPrices> QueryTravelData(IQueryable<TravelPrices> query)
        {
            return await query
                // .Where(priceList => priceList.ValidUntil > DateTime.Now)
                .Include(travelPrice => travelPrice.Legs)
                .ThenInclude(leg => leg.RouteInfo)
                .ThenInclude(route => route.From)
                .Include(travelPrice => travelPrice.Legs)
                .ThenInclude(leg => leg.RouteInfo)
                .ThenInclude(route => route.To)
                .Include(travelPrice => travelPrice.Legs)
                .ThenInclude(leg => leg.Providers)
                .ThenInclude(provider => provider.Company)
                .FirstOrDefaultAsync();
        }
    }
}