using System;

namespace PublicApiDTO.TravelModels.v1
{
    public class RouteInfoProvider
        // : AddRouteInfoProvider
    {
        // public Guid Id { get; set; }
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public long Distance { get; set; }
        public Provider Provider { get; set; } = null!;
    }

    // public class AddRouteInfoProvider
    // {
    //     public Guid RouteInfoId { get; set; }
    //     public Guid ProviderId { get; set; }
    // }
}