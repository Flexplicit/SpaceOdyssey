using System;
using System.Threading.Tasks;
using App.Domain.TravelModels.Enums;
using AutoMapper;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using PublicApiDTO.Mappers;
using PublicApiDTO.TravelModels.v1;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly PlanetMapper _mapper;

        public RouteController(IMapper mapper, IAppUnitOfWork uow)
        {
            _mapper = new PlanetMapper(mapper);
            _uow = uow;
        }

        [HttpGet("{from}/{to}")]
        public async Task<ActionResult<TravelData>> GetRouteInfoAccordingToQuery(string from, string to)
        {
            var fromPlanet = MapPlanetToEnum(from);
            var toPlanet = MapPlanetToEnum(to);
        
        
            await _uow.TravelPrices.GetRouteTravelDataAsync(fromPlanet, toPlanet);
            throw new Exception("not implemented");
        }


        private static EPlanet MapPlanetToEnum(string to)
        {
            return Enum.Parse<EPlanet>(to, true);
        }
    }
}