using System;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class RouteInfoData: DomainEntityId
    {
        public Provider? Provider { get; set; } = null!;
        public Guid ProviderId { get; set; }

        public RouteInfo? RouteInfo { get; set; } = null!;
        public Guid RouteInfoId { get; set; }

        public Reservation? Reservation { get; set; } = null!;
        public Guid ReservationId { get; set; }
    }
}