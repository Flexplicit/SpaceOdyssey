using AutoMapper;
using PublicApiDTO.TravelModels.v1;

namespace PublicApiDTO.Mappers.MappingProfiles
{
    public class BasePublicDtoMapper<TInEntity, TOutEntity>
    {
        protected IMapper Mapper = null!;

        public BasePublicDtoMapper(IMapper mapper)
        {
            Mapper = mapper;
        }

        public TOutEntity? Map(TInEntity? inEntity)
        {
            return Mapper.Map<TOutEntity>(inEntity);
        }

        public TInEntity? Map(TOutEntity? inEntity)
        {
            return Mapper.Map<TInEntity>(inEntity);
        }
    }
}