using System;
using App.Domain.TravelModels;

namespace App.Domain.TravelDataDTO
{
    public class RouteInfoProvider
    {
        public Guid Id { get; set; }
        public Planet From { get; set; } = null!;
        public Planet To { get; set; } = null!;
        public long Distance { get; set; }
        public Provider Provider { get; set; } = null!;
    }
}