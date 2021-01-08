using System;
using System.Collections.Generic;

namespace EmprestimosJogos.Domain.Core.Types
{
    public class CustomEqualityComparer<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> CustomEquals { get; }
        public Func<T, int> CustomGetHashCode { get; }

        public CustomEqualityComparer(
            Func<T, T, bool> customEquals,
            Func<T, int> customGetHashCode)
        {
            CustomEquals = customEquals;
            CustomGetHashCode = customGetHashCode;
        }

        public bool Equals(T x, T y)
        {
            return CustomEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return CustomGetHashCode(obj);
        }
    }
}
