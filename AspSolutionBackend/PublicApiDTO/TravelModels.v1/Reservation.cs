using System;
using System.Collections.Generic;

namespace PublicApiDTO.TravelModels.v1
{
    public class Reservation : UpdateReservation
    {
        public double TotalQuotedPrice { get; set; }
        public double TotalQuotedTravelTimeInMinutes { get; set; }
        public List<RouteInfoData> RouteInfoData { get; set; } = null!;
    }

    public class AddReservation
    {
        public Guid TravelPricesId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        // public List<RouteInfoProvider> Routes { get; set; } = null!;
        public List<AddRouteInfoProvider> Routes { get; set; } = null!;
    }

    public class UpdateReservation : AddReservation
    {
        public Guid Id { get; set; }
    }
}