using AutoMapper;

namespace PublicApiDTO.Mappers.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PublicApiDTO.TravelModels.v1.Reservation, App.Domain.TravelModels.Reservation>().ReverseMap();
            CreateMap<PublicApiDTO.TravelModels.v1.Company, App.Domain.TravelModels.Company>().ReverseMap();
            CreateMap<PublicApiDTO.TravelModels.v1.RouteInfo, App.Domain.TravelModels.RouteInfo>().ReverseMap();
            CreateMap<PublicApiDTO.TravelModels.v1.Planet, App.Domain.TravelModels.Planet>().ReverseMap();
        }
    }
}