using App.Domain.TravelModels;
using AutoMapper;
using PublicApiDTO.Mappers.MappingProfiles;
using PublicDto = PublicApiDTO.TravelModels.v1;
using DomainDTO = App.Domain.TravelModels;

namespace PublicApiDTO.Mappers
{
    public class ReservationMapper : BasePublicDtoMapper<PublicDto.Reservation, DomainDTO.Reservation>
    {
        public ReservationMapper(IMapper mapper) : base(mapper)
        {
        }
        
    }
}