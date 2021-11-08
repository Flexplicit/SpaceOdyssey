using System;
using App.Domain.TravelModels;

namespace App.Domain.TravelDataDTO
{
    public class RouteInfoProvider
    {
        public Guid RouteInfoId { get; set; }
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;

        public long Distance { get; set; }

        public Provider Provider { get; set; } = null!;
    }
}