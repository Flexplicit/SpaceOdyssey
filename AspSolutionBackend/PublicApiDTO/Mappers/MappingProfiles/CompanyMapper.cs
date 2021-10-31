using App.Domain.TravelModels;
using AutoMapper;

namespace PublicApiDTO.Mappers.MappingProfiles
{
    public class CompanyMapper: BasePublicDtoMapper<Company,App.Domain.TravelModels.Company>
    {
        public CompanyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}