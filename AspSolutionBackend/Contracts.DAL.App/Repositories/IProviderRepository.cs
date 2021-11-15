using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.APP.Repositories
{
    public interface IProviderRepository : IBaseRepository<Provider>
    {
        Task<List<Provider>> GetProviderByIdAndConfirmIfItExistsInTravelPrice(List<Guid> providerId, Guid travelPricesId);
    }
}