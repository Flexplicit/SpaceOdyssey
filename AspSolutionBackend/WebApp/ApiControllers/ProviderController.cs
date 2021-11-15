using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApiDTO.Mappers.MappingProfiles;
using PublicApiDTO.TravelModels.v1;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CompanyController : ControllerBase
    {
        private IAppUnitOfWork _uow;
        private CompanyMapper _companyMapper;

        public CompanyController(IAppUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _companyMapper = new CompanyMapper(mapper);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Company>), StatusCodes.Status200OK)]
        public ActionResult<Company> GetAllDistinctCompanies()
            => Ok(_uow
                .Companies
                .GetDistinctCompanies()
                .Select(company => _companyMapper.Map(company)));
    }
}