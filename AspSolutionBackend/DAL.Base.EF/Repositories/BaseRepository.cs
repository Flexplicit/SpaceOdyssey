using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.Base.Repositories;
using Contracts.DAL.Domain;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class
        BaseRepository<TDomainEntity, TDbContext> : BaseRepository<TDomainEntity, Guid,
            TDbContext>
        where TDomainEntity : class, IDomainEntityId
        where TDbContext : DbContext
    {
        public BaseRepository(TDbContext ctx) : base(ctx)
        {
        }
    }


    public class BaseRepository<TDomainEntity, TKey, TDbContext> : IBaseRepository<TDomainEntity, TKey>
        where TDomainEntity : class, IDomainEntityId<TKey>
        where TDbContext : DbContext
        where TKey : IEquatable<TKey>
    {
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TDomainEntity> RepoDbSet;

        public BaseRepository(TDbContext ctx)
        {
            RepoDbContext = ctx;
            RepoDbSet = ctx.Set<TDomainEntity>();
        }


        protected IQueryable<TDomainEntity> CreateQuery(bool noTracking)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public virtual async Task<IEnumerable<TDomainEntity>> GetAllAsync(bool noTracking = true) =>
            await CreateQuery(noTracking)
                .ToListAsync();

        public virtual async Task<TDomainEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
            => await CreateQuery(noTracking).FirstOrDefaultAsync(e => e.Id.Equals(id));


        public virtual async Task<TDomainEntity> RemoveAsync(TKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            return Remove(entity!);
        }

        public virtual TDomainEntity Add(TDomainEntity entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public virtual TDomainEntity Update(TDomainEntity entity)
        {
            return RepoDbSet.Update(entity).Entity;
        }

        public virtual TDomainEntity Remove(TDomainEntity entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }
    }
}