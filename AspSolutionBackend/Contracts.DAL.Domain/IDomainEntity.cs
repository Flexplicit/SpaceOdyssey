using System;

namespace Contracts.DAL.Domain
{
    public interface IDomainEntity : IDomainEntity<Guid>
    {
    }


    public interface IDomainEntity<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}