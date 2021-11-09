using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels.Enums;
using AutoMapper;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using PublicApiDTO.Mappers;
using PublicApiDTO.TravelModels.v1;
using PublicDto = PublicApiDTO.TravelModels.v1;
using DomainDTO = App.Domain.TravelModels;


namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly PlanetMapper _planetMapper;
        private readonly TravelDataMapper _travelDataMapper;

        public RouteController(IMapper mapper, IAppUnitOfWork uow)
        {
            _planetMapper = new PlanetMapper(mapper);
            _travelDataMapper = new TravelDataMapper(mapper);
            _uow = uow;
        }

        [HttpGet("{from}/{to}/{startDate}")]
        public async Task<ActionResult<List<PublicDto.TravelData>>> GetRoutesBetweenTwoPlanets(string from, string to,
            string startDate)
        {
            var fromPlanet = MapPlanetToEnum(from);
            var toPlanet = MapPlanetToEnum(to);
            var date = DateTime.Parse(startDate);
            
            var travelData = await _uow.TravelPrices.GetRouteTravelDataAsync(fromPlanet, toPlanet);
            var mappedData = travelData.Select(data => _travelDataMapper.Map(data)).Take(1);

            return Ok(mappedData);
        }


        private static EPlanet MapPlanetToEnum(string to)
        {
            return Enum.Parse<EPlanet>(to, true);
        }
    }
}