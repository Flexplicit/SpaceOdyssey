﻿using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.APP.Repositories;
using Contracts.DAL.Base;
using DAL.Base.EF;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IReservationRepository Reservations { get; }
        ICustomTravelPricesRepository TravelPrices { get; }

        
        IPlanetRepository Planets { get; }

    }
}