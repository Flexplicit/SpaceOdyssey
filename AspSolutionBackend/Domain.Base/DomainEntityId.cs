using System;
using Contracts.DAL.Domain;

namespace Domain.Base
{
    public class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
    {
    }

    public class DomainEntityId<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}