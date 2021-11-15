using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.TravelDataDTO;
using App.Domain.TravelModels;
using App.Domain.TravelModels.Enums;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public enum ESortBy
    {
        Price,
        Distance,
        Time
    }

    public interface ITravelPricesRepository : IBaseRepository<TravelPrices>
    {
        Task<bool> IsTravelPriceValid(Guid travelPriceId);
    }

    public interface ICustomTravelPricesRepository : ITravelPricesRepository
    {
        Task<List<TravelData>> GetRouteTravelDataAsync(EPlanet from, EPlanet to, DateTime startDate, ESortBy sortBy,
            List<string> providers);
    }
}