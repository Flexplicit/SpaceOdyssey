using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Contracts.DAL.App;
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
        private readonly ReservationMapper _mapper;


        public ReservationController(IAppUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = new ReservationMapper(mapper);
        }

        // // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            var reservations = await _uow.Reservations.GetAllAsync();
            return Ok(reservations.Select(reservation => _mapper.Map(reservation)));
        }


        // GET: api/Reservation/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Reservation>> GetReservation(Guid id)
        {
            var reservation = await _uow.Reservations.FirstOrDefaultAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(reservation));
        }

        // POST: api/Reservation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            if (!await _uow.TravelPrices.IsTravelPriceValid(reservation.TravelPriceId))
            {
                return NotFound("Given price list is not valid");
            }
            

            var result = _uow.Reservations.Add(_mapper.Map(reservation)!);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = result.Id }, _mapper.Map(result));
        }
    }
}