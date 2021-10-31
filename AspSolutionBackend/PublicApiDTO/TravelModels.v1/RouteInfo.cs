using System;
using App.Domain.TravelModels;

namespace PublicApiDTO.TravelModels.v1
{
    public class RouteInfo
    {
        // public Guid Id { get; set; }
        public Planet From { get; set; } = null!;
        public Planet To { get; set; } = null!;
        public long Distance { get; set; }
    }

    public class RouteInfoProvider : RouteInfo
    {
        public Provider Provider { get; set; } = null!;
    }
}