using System;
using System.Numerics;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.APP.Repositories;
using DAL.App.EF.CustomRepositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowCtx) : base(uowCtx)
        {
        }

        public IReservationRepository Reservations =>
            GetRepository(() => new ReservationRepository(_uowContext));

        public ICustomTravelPricesRepository TravelPrices =>
            GetRepository(() => new CustomPriceListRepository(_uowContext));


        public IPlanetRepository Planets =>
            GetRepository(() => new PlanetRepository(_uowContext));
    }
}