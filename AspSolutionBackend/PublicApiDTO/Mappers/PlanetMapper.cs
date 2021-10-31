using AutoMapper;
using PublicApiDTO.Mappers.MappingProfiles;
using PublicDto = PublicApiDTO.TravelModels.v1;
using domainDTO = App.Domain.TravelModels;

namespace PublicApiDTO.Mappers
{
    public class PlanetMapper : BasePublicDtoMapper<PublicDto.Planet, domainDTO.Planet>
    {
        public PlanetMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}