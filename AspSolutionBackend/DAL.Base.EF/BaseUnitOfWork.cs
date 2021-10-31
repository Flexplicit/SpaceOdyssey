using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseUnitOfWork<TDbContext> : BaseUnitOfWorkCache
        where TDbContext : DbContext
    {
        protected TDbContext _uowContext;

        public BaseUnitOfWork(TDbContext ctx)
        {
            _uowContext = ctx;
        }

        public override Task<int> SaveChangesAsync()
        {
            return _uowContext.SaveChangesAsync();
        }
    }
}