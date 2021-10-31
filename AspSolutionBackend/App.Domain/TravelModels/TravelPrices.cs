using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Base;

namespace App.Domain.TravelModels
{
    public class TravelPrices : DomainEntityId
    {
        public DateTime ValidUntil { get; set; }
        public List<Legs>? Legs { get; set; } = null!;
    }
}