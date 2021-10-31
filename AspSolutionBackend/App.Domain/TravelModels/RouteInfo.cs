using System;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class RouteInfo : DomainEntity
    {
        public Planet From { get; set; } = null!;
        public Guid FromId { get; set; }
        public Planet To { get; set; } = null!;
        public Guid ToId { get; set; }
        
        public Reservation? Reservation { get; set; }
        public Guid? ReservationId { get; set; }

        public long Distance { get; set; }
    }
}