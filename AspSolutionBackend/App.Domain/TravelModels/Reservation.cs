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

        public List<RouteInfo> Routes { get; set; } = null!;
        public List<Company> TransportationCompanyNames { get; set; } = null!;

        //TODO:
        // Might be better to just give the reference instead of loading the whole damn database into memory for verifications.
        public TravelPrices? TravelPrice { get; set; }
    }
}