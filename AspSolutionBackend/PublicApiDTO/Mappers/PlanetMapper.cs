using AutoMapper;
using PublicApiDTO.Mappers.MappingProfiles;
using PublicDto = PublicApiDTO.TravelModels.v1;
using DomainDTO = App.Domain.TravelModels;

namespace PublicApiDTO.Mappers
{
    public class PlanetMapper : BasePublicDtoMapper<PublicDto.Planet, DomainDTO.Planet>
    {
        public PlanetMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}