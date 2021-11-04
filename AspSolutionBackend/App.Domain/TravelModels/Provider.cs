using System;
using System.Collections.Generic;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class Provider : DomainEntityId
    {
        public double Price { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
        public Company? Company { get; set; } = null!;
        public Guid CompanyId { get; set; }

        public List<RouteInfoData>? RouteInfoData { get; set; }


        public Legs Legs { get; set; } = null!;
        public Guid LegsId { get; set; }
    }
}