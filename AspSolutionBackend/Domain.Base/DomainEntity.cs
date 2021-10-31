using System;
using Contracts.DAL.Domain;

namespace Domain.Base
{
    public class DomainEntity : DomainEntity<Guid>
        // , IDomainEntity
    {
    }

    public class DomainEntity<TKey> : DomainEntityId<TKey>, IDomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}