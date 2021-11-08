using System;
using System.Numerics;
using App.Domain.TravelModels;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.APP.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.CustomRepositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowCtx) : base(uowCtx)
        {
            IBaseRepository<RouteInfoData, Guid> repo =
                new BaseRepository<RouteInfoData, Guid, AppDbContext>(_uowContext);
        }

        public IReservationRepository Reservations =>
            GetRepository(() => new ReservationRepository(_uowContext));

        public ICustomTravelPricesRepository TravelPrices =>
            GetRepository(() => new CustomPriceListRepository(_uowContext));


        public IPlanetRepository Planets =>
            GetRepository(() => new PlanetRepository(_uowContext));

        public IBaseRepository<RouteInfoData, Guid> RouteInfoData =>
            GetRepository(() => new BaseRepository<RouteInfoData, Guid, AppDbContext>(_uowContext));
    }
}