using System;
using App.Domain.TravelModels;

namespace App.Domain.TravelDataDTO
{
    public class RouteInfoProvider
    {
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;

        public long Distance { get; set; }

        public Provider Provider { get; set; } = null!;
    }
}