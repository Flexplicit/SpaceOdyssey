using AutoMapper;
using PublicApiDTO.Mappers.MappingProfiles;
using PublicApiDTO.TravelModels.v1;
using PublicDto = PublicApiDTO.TravelModels.v1;
using DomainDTO = App.Domain.TravelModels;

namespace PublicApiDTO.Mappers
{
    public class RouteInfoDataMapper : BasePublicDtoMapper<PublicDto.RouteInfoData, DomainDTO.RouteInfoData>
    {
        public RouteInfoDataMapper(IMapper mapper) : base(mapper)
        {
        }

        //TODO: do this
        // public DomainDTO.RouteInfoData MapPublicAddRouteInfoToDomain(AddRouteInfoData addRouteInfoData)
        // {
        //     return new DomainDTO.RouteInfoData
        //     {
        //         ProviderId = addRouteInfoData.ProviderId,
        //         RouteInfoId = addRouteInfoData.RouteInfoId
        //     };
        // }
        public DomainDTO.RouteInfoData MapPublicAddRouteInfoToDomain(AddRouteInfoProvider addRouteInfoData)
        {
            return new DomainDTO.RouteInfoData
            {
                ProviderId = addRouteInfoData.ProviderId,
                RouteInfoId = addRouteInfoData.RouteInfoId
            };
        }
    }
}