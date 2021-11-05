using System;

namespace PublicApiDTO.TravelModels.v1
{
    public class RouteInfoProvider: AddRouteInfoProvider
    {
        public Guid Id { get; set; }
        public RouteInfo RouteInfo { get; set; } = null!;
        public Provider Provider { get; set; } = null!;
    }

    public class AddRouteInfoProvider
    {
        public Guid RouteInfoId { get; set; }
        public Guid ProviderId { get; set; }
    }
}