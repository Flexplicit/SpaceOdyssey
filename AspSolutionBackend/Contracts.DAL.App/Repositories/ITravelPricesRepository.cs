using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.TravelDataDTO;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface ITravelPricesRepository : IBaseRepository<TravelPrices>
    {
        Task<TravelPrices> GetLatestTravelPriceAsync();
    }

    public interface ICustomTravelPricesRepository : ITravelPricesRepository
    {
        Task<List<TravelData>> GetRouteTravelDataAsync(EPlanet from, EPlanet to, DateTime startDate);
        Task<bool> IsTravelPriceValid(Guid travelPriceId);
    }
}