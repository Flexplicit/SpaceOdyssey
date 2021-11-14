using System;
using System.Collections.Generic;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class Legs : DomainEntityId
    {
        public List<Provider>? Providers { get; set; } = null!;
        public RouteInfo? RouteInfo { get; set; } = null!;
        public Guid RouteInfoId { get; set; }

        public TravelPrices? TravelPrices { get; set; }
        public Guid TravelPricesId { get; set; }
    }
}