using System;

namespace PublicApiDTO.TravelModels.v1
{
    public class RouteInfoData
    {
        public Guid Id { get; set; }
        public Provider Provider { get; set; } = null!;
        public RouteInfo RouteInfo { get; set; } = null!;
    }

    public class AddRouteInfoData
    {
        public Guid ProviderId { get; set; }
        public Guid RouteInfoId { get; set; }
    }
}