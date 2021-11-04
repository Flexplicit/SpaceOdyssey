using System;
using Domain.Base;

namespace PublicApiDTO.TravelModels.v1
{
    public class Provider : DomainEntityId
    {
        public double Price { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
        public Company? Company { get; set; } = null!;
    }
}