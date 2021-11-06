using System;
using App.Domain.TravelModels;
using Domain.Base;

namespace PublicApiDTO.TravelModels.v1
{
    public class RouteInfo
    {
        public Planet From { get; set; } = null!;
        public Planet To { get; set; } = null!;
        public long Distance { get; set; }
    }
    
}