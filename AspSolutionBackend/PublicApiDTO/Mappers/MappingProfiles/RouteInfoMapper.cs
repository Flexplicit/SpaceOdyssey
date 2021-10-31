using App.Domain.TravelModels;
using AutoMapper;

namespace PublicApiDTO.Mappers.MappingProfiles
{
    public class RouteInfoMapper : BasePublicDtoMapper<RouteInfo, App.Domain.TravelModels.RouteInfo>
    {
        public RouteInfoMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}