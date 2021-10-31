using System.Threading.Tasks;
using App.Domain.TravelModels;
using Contracts.DAL.APP.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation, AppDbContext>, IReservationRepository
    {
        public ReservationRepository(AppDbContext ctx) : base(ctx)
        {
        }


        public Task<int> Test()
        {
            throw new System.NotImplementedException();
        }
    }
}