using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Domain;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class Reservation : DomainEntityId
    {
        [StringLength(255)] [Required] public string FirstName { get; set; } = null!;
        [StringLength(255)] [Required] public string LastName { get; set; } = null!;


        public double TotalQuotedPrice { get; set; }
        public double TotalQuotedTravelTimeInMinutes { get; set; }

        public List<RouteInfoData>? RouteInfoData { get; set; } = null!;

        public TravelPrices? TravelPrice { get; set; }
        public Guid TravelPriceId { get; set; }
    }
}