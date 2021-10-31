using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.APP.Repositories;
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

        public ITravelPricesRepository TravelPrices =>
            GetRepository(() => new TravelPricesRepository(_uowContext));
    
    }
}