using App.Domain.TravelModels;
using AutoMapper;
using PublicApiDTO.Mappers.MappingProfiles;
using PublicDto = PublicApiDTO.TravelModels.v1;
using domainDTO = App.Domain.TravelModels;

namespace PublicApiDTO.Mappers
{
    public class ReservationMapper : BasePublicDtoMapper<PublicDto.Reservation, domainDTO.Reservation>
    {
        public ReservationMapper(IMapper mapper) : base(mapper)
        {
        }
        
    }
}