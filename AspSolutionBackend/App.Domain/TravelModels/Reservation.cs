using System;
using System.Collections.Generic;
using Contracts.DAL.Domain;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class Reservation : DomainEntityId
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;


        public double TotalQuotedPrice { get; set; }
        public int TotalQuotedTravelTimeInMinutes { get; set; }

        public List<RouteInfoData> RouteInfoData { get; set; } = null!;

        public TravelPrices? TravelPrice { get; set; }
        public Guid TravelPricesId { get; set; }
    }
}