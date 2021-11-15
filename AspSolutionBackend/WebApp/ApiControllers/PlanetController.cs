using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.TravelModels.Enums;
using AutoMapper;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApiDTO.Mappers;
using PublicApiDTO.TravelModels.v1;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly PlanetMapper _planetMapper;

        public PlanetController(IAppUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _planetMapper = new PlanetMapper(mapper);
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<string>> GetPlanetNames()
        {
            var planets = _uow.Planets
                .GetPlanetNames();

            return Ok(planets);
        }
    }
}