using System.Collections.Generic;
using Domain.Base;

namespace App.Domain
{
    public class ModelComparer<T> : IEqualityComparer<T>
        where T : DomainEntityId
    {
        public bool Equals(T? x, T? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(T obj)
        {
            var hashProductName = obj.Id == default ? default : obj.Id.GetHashCode();
            var hashProductCode = obj.Id.GetHashCode();
            return hashProductName ^ hashProductCode;
        }
    }
}