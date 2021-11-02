using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.App.Repositories;
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
    public class TravelPricesRepository : BaseRepository<TravelPrices, AppDbContext>, ITravelPricesRepository
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


        public async Task<List<TravelData>> GetRouteTravelDataAsync(EPlanet from, EPlanet to)
        {
            var query = CreateQuery(true);
            var travelPrices = await QueryTravelData(query);


            var graph = new Graph<EPlanet, DomainDTO.Provider>("");

            var planetRouteDict = new Dictionary<EPlanet, Vertex<EPlanet, DomainDTO.Provider>>();


            foreach (var leg in travelPrices.Legs!)
            {
                if (!planetRouteDict.TryGetValue(leg.RouteInfo.From.Name, out var fromVertex))
                {
                    fromVertex = graph.CreateVertex(leg.RouteInfo.From.Name.ToString(), leg.RouteInfo.From.Name);
                    planetRouteDict.Add(leg.RouteInfo.From.Name, fromVertex);
                }

                if (!planetRouteDict.TryGetValue(leg.RouteInfo.To.Name, out var toVertex))
                {
                    toVertex = graph.CreateVertex(leg.RouteInfo.To.Name.ToString(), leg.RouteInfo.To.Name);
                    planetRouteDict.Add(leg.RouteInfo.To.Name, toVertex);
                }

                leg.Providers!.ForEach(provider =>
                    graph.CreateArc($"{fromVertex.Id}-{toVertex.Id}", fromVertex, toVertex, leg,
                        (long?)provider.Price));
            }

            if (!planetRouteDict.TryGetValue(@from, out var vertexFrom) ||
                !planetRouteDict.TryGetValue(to, out var vertexTo)) return new List<TravelData>();
            
            var optimizedArcData = graph.YensKShortestPathFinder(vertexFrom, vertexTo);
            var optimizedLegRouteData = optimizedArcData.Select(GraphComponentMapper.MapDataFromArcs).ToList();
            // var travelDataList = new List<TravelData>(
            //     optimizedLegRouteData.Select(path => new TravelData()
            //     {
            //         Routes = path.Select(provider => new RouteInfoProvider()
            //         {
            //             Distance = provider!.Legs.RouteInfo.Distance,
            //             From = provider.Legs.RouteInfo.From,
            //             To = provider.Legs.RouteInfo.To,
            //             Provider = provider
            //         }),
            //         TotalDistanceInKilometers = path.Sum(provider => provider!.Legs.RouteInfo.Distance),
            //         TotalLengthInHours = path.Sum(provider =>
            //             CalculateHoursBetweenDates(provider!.FlightStart, provider.FlightEnd)),
            //         TotalPrice = path.Sum(provider => provider!.Price),
            //         ValidUntil = travelPrices.ValidUntil
            //     }).ToList()
            // );
            return travelDataList;


        }

        private static double CalculateHoursBetweenDates(DateTime start, DateTime end)
        {
            return (end - start).TotalHours;
        }

        private static async Task<TravelPrices> QueryTravelData(IQueryable<TravelPrices> query)
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