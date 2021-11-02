using System;
using System.Collections.Generic;

namespace App.Domain.TravelDataDTO
{
    public class TravelData
    {
        public DateTime ValidUntil { get; set; }
        public long TotalDistanceInKilometers { get; set; }
        public double TotalPrice { get; set; }
        public double TotalLengthInHours { get; set; }

        public List<RouteInfoProvider> Routes { get; set; } = null!;
    }
}