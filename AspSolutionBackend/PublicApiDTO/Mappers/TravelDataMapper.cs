using AutoMapper;
using PublicApiDTO.Mappers.MappingProfiles;
using PublicDto = PublicApiDTO.TravelModels.v1;
using DomainDTO = App.Domain.TravelDataDTO;

namespace PublicApiDTO.Mappers
{
    public class TravelDataMapper : BasePublicDtoMapper<PublicDto.TravelData, DomainDTO.TravelData>
    {
        public TravelDataMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}