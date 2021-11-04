using AutoMapper;
using PublicDto = PublicApiDTO.TravelModels.v1;
using DomainDTO = App.Domain.TravelModels;
namespace PublicApiDTO.Mappers.MappingProfiles
{
    public class CompanyMapper: BasePublicDtoMapper<PublicDto.Company,DomainDTO.Company>
    {
        public CompanyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}