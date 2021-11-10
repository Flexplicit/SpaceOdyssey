using System;
using App.Domain.TravelModels;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.APP.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IReservationRepository Reservations { get; }
        ICustomTravelPricesRepository TravelPrices { get; }

        IPlanetRepository Planets { get; }
        IBaseRepository<RouteInfoData, Guid> RouteInfoData { get; }
        
        ICompanyRepository Companies { get; }
    }
}