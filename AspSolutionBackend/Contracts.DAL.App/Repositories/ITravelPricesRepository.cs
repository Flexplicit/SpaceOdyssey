using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.Base.Repositories;
using PublicApiDTO.TravelModels.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ITravelPricesRepository : IBaseRepository<TravelPrices>, ICustomTravelPricesRepository
    {
        Task<TravelPrices> GetLatestTravelPriceAsync();
    }

    public interface ICustomTravelPricesRepository
    {
        Task<List<TravelData>> GetRouteTravelDataAsync(EPlanet from, EPlanet to);
    }
}