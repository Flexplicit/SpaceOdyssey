using System;
using System.Collections.Generic;

namespace PublicApiDTO.TravelModels.v1
{
    public class Reservation : AddReservation
    {
        public double TotalQuotedPrice { get; set; }
        public int TotalQuotedTravelTimeInMinutes { get; set; }

        public List<RouteInfo> Routes { get; set; } = null!;
        public List<Company> TransportationCompanyNames { get; set; } = null!;

        // We can use this to check if it's still valid
        public Guid TravelPriceId { get; set; }
    }

    public class AddReservation : UpdateReservation
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }

    public class UpdateReservation
    {
        public Guid Id { get; set; }
    }
}