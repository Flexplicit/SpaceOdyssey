using System;

namespace PublicApiDTO.TravelModels.v1
{
    public class Planet 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }   
}