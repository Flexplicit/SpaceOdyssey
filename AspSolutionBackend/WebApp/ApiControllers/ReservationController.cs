using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using PublicApiDTO.Mappers;
using PublicApiDTO.TravelModels.v1;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ReservationMapper _reservationMapper;
        private readonly RouteInfoDataMapper _routeInfoDataMapper;


        public ReservationController(IAppUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _reservationMapper = new ReservationMapper(mapper);
            _routeInfoDataMapper = new RouteInfoDataMapper(mapper);
        }

        // // GET: api/Reservation
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Reservation>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            var reservations = await _uow.Reservations.GetAllAsync();
            return Ok(reservations.Select(reservation => _reservationMapper.Map(reservation)));
        }


        // GET: api/Reservation/5
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        public async Task<ActionResult<Reservation>> GetReservation(Guid id)
        {
            var reservation = await _uow.Reservations.FirstOrDefaultAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(_reservationMapper.Map(reservation));
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Reservation>> PostReservation(AddReservation reservation)
        {
            var providerIds = reservation.Routes.Select(route => route.ProviderId).ToList();
            var providers =
                await _uow.Providers.GetProviderByIdAndConfirmIfItExistsInTravelPrice(providerIds,
                    reservation.TravelPricesId);
            if (providers.Count == 0 || !await _uow.TravelPrices.IsTravelPriceValid(reservation.TravelPricesId))
            {
                return NotFound("Given price list is not valid");
            }
            var addedReservation = _uow.Reservations.Add(_reservationMapper.MapPublicAddedReservationToDomain(reservation, providers));

            reservation.Routes.ForEach(route =>
            {
                var mappedRoute = _routeInfoDataMapper.MapPublicAddRouteInfoToDomain(route);
                mappedRoute.ReservationId = addedReservation.Id;
                _uow.RouteInfoData.Add(mappedRoute);
            });

            await _uow.SaveChangesAsync();
            return CreatedAtAction("GetReservation", new { id = addedReservation.Id },
                _reservationMapper.Map(addedReservation));
        }
    }
}