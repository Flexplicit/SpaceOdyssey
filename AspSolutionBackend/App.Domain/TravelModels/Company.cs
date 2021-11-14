using System;
using System.Collections.Generic;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class Company : DomainEntityId
    {
        public string Name { get; set; } = null!;
    
        public Reservation? Reservation { get; set; }
        public Guid? ReservationId { get; set; }

        public List<Provider>? Providers { get; set; }
    }
}