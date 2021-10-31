using System.Threading.Tasks;
using App.Domain.TravelModels;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.APP.Repositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<int> Test();
    }
}