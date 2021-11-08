using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelDataDTO;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using Graph;
using Graph.GraphModels;
using Utils;

namespace DAL.App.EF.CustomRepositories
{
    public class CustomPriceListRepository : TravelPricesRepository, ICustomTravelPricesRepository
    {
        public CustomPriceListRepository(AppDbContext ctx) : base(ctx)
        {
        }


        public async Task<List<TravelData>> GetRouteTravelDataAsync(EPlanet @from, EPlanet to)
        {
            var query = CreateQuery(true);
            var travelPrices = await QueryTravelData(query);

            var graph = new Graph<EPlanet, Provider>("");

            var planetRouteDict = AddVerticesAndArcsToGraph(travelPrices, graph);

            if (!planetRouteDict.TryGetValue(@from, out var vertexFrom) ||
                !planetRouteDict.TryGetValue(to, out var vertexTo)) return new List<TravelData>();

            var optimizedArcData = graph.YensKShortestPathFinder(vertexFrom, vertexTo);
            var optimizedLegRouteData = optimizedArcData
                .Select(GraphComponentMapper.MapDataFromArcs)
                .ToList();

            if (optimizedLegRouteData.Count - 1 == 0) return new List<TravelData>();


            var travelDataList = ConvertPathsToTravelDataList(optimizedLegRouteData!, travelPrices);
            return travelDataList;
        }

        public async Task<bool> IsTravelPriceValid(Guid travelPriceId)
        {
            var result = await FirstOrDefaultAsync(travelPriceId);
            return result != null && result.ValidUntil > DateTime.Now;
        }


        private static Dictionary<EPlanet, Vertex<EPlanet, Provider>> AddVerticesAndArcsToGraph(
            TravelPrices travelPrices, Graph<EPlanet, Provider> graph)
        {
            var planetRouteDict = new Dictionary<EPlanet, Vertex<EPlanet, Provider>>();


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
                    graph.CreateArc($"{fromVertex.Id}-{toVertex.Id}", fromVertex, toVertex, provider,
                        (long?)provider.Price));
            }

            return planetRouteDict;
        }

        private static List<TravelData> ConvertPathsToTravelDataList(List<List<Provider>> optimizedLegRouteData,
            TravelPrices travelPrices)
        {
            return new List<TravelData>(
                optimizedLegRouteData.Select(path => new TravelData()
                {
                    TravelPricesId = travelPrices.Id,
                    Routes = MapPathProviderToRouteInfoProvider(path),
                    TotalDistanceInKilometers = path.Sum(provider => provider.Legs.RouteInfo.Distance),
                    TotalLengthInHours = path.Sum(provider =>
                        DateUtils.CalculateHoursBetweenDates(provider.FlightStart, provider.FlightEnd)),
                    TotalPrice = path.Sum(provider => provider.Price),
                    ValidUntil = travelPrices.ValidUntil
                }).ToList()
            );
        }

        //TODO : Routeinfo and provider only?
        private static List<RouteInfoProvider> MapPathProviderToRouteInfoProvider(List<Provider> path)
        {
            return path.Select(provider => new RouteInfoProvider()
            {
                RouteInfoId = provider.Legs.RouteInfoId,
                Distance = provider!.Legs.RouteInfo.Distance,
                From = provider.Legs.RouteInfo.From.Name.ToString(),
                To = provider.Legs.RouteInfo.To.Name.ToString(),
                Provider = provider,
            }).ToList();
        }
    }
}