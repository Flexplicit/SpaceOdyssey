using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Domain;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
        where TEntity : class, IDomainEntityId
    {
    }

    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);
        Task<TEntity> RemoveAsync(TKey id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        // void UpdateRange(IEnumerable<TEntity> entity);
        // Task<bool> ExistsAsync(TKey id);
    }
}