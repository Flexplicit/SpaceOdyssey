using System;

namespace PublicApiDTO.TravelModels.v1
{
    public class RouteInfoData
    {
        public Guid Id { get; set; }
        public Provider Provider { get; set; } = null!;
        public RouteInfo RouteInfo { get; set; } = null!;
        public Reservation Reservation { get; set; } = null!;
        public Guid ReservationId { get; set; }
    }

    public class AddRouteInfoData
    {
        public Guid ProviderId { get; set; }
        public Guid RouteInfoId { get; set; }
    }
}