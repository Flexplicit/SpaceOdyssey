using System.Collections.Generic;
using System.Linq;
using App.Domain.TravelModels;
using AutoMapper;
using PublicApiDTO.Mappers.MappingProfiles;
using Utils;
using PublicDto = PublicApiDTO.TravelModels.v1;
using DomainDTO = App.Domain.TravelModels;

namespace PublicApiDTO.Mappers
{
    public class ReservationMapper : BasePublicDtoMapper<PublicDto.Reservation, DomainDTO.Reservation>
    {
        public ReservationMapper(IMapper mapper) : base(mapper)
        {
        }


        public DomainDTO.Reservation MapPublicAddedReservationToDomain(PublicDto.AddReservation addReservation,
            List<Provider> providers)
        {
            return new Reservation
            {
                TravelPriceId = addReservation.TravelPricesId,
                FirstName = addReservation.FirstName,
                LastName = addReservation.LastName,
                TotalQuotedPrice = providers.Sum((provider => provider.Price)),
                TotalQuotedTravelTimeInMinutes =
                    DateUtils.CalculateMinutesBetweenDates(providers.First().FlightStart, providers.Last().FlightEnd)
            };
        }
    }
}