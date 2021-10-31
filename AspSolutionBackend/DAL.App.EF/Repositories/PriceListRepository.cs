﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Graph;
using Graph.GraphModels;
using Microsoft.EntityFrameworkCore;
using PublicApiDTO.TravelModels.v1;
using Services.ApiServices;
using Utils;
using Planet = App.Domain.TravelModels.Planet;
using RouteInfo = App.Domain.TravelModels.RouteInfo;

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


            var graph = new Graph<EPlanet, Legs>("travelGraph");
            var routeInfos = travelPrices.Legs!;
            var planetRouteDict = new Dictionary<EPlanet, Vertex<EPlanet, Legs>>();


            foreach (var leg in routeInfos)
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

                graph.CreateArc($"{fromVertex.Id}-{toVertex.Id}", fromVertex, toVertex, leg);
            }

            if (planetRouteDict.TryGetValue(from, out var vertexFrom) &&
                planetRouteDict.TryGetValue(to, out var vertexTo))
            {
                var optimizedArcData = graph.GetArcDepthFirstSearch(vertexFrom, vertexTo);
                var optimizedLegRouteData = optimizedArcData.Select(GraphComponentMapper.MapDataFromArcs).ToList();
                var travelDataList = new List<TravelData>();


                // foreach (var legRoutes in optimizedLegRouteData)
                // {
                //     foreach (var leg in legRoutes)
                //     {
                //         leg.
                //     }
                // }
                // optimizedLegRouteData.ForEach(routeLeg =>
                // {
                //     travelDataList.Add(new TravelData()
                //     {
                //         routeLeg
                //     });
                // });
            }


            return new List<TravelData>();
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