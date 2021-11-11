using System.Collections.Generic;

namespace PublicApiDTO.TravelModels.v1
{
    public class SearchDTO
    {
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string SortBy { get; set; } = null!;
        public List<string> ProviderNames { get; set; } = null!;
    }
}