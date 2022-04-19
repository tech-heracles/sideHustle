using System;
using System.Collections.Generic;

namespace DbCore.IMBUtils.Types
{
    /// <summary>
    /// generic comparer per te krahesuar dy objekte duke marr si parameter nje lambda e
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuncEqualityComparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> _comparer;
        readonly Func<T, int> _hash;

        public FuncEqualityComparer(Func<T, T, bool> comparer)
            : this(comparer, t => 0) 
        {
        }

        public FuncEqualityComparer(Func<T, T, bool> comparer, Func<T, int> hash)
        {
            _comparer = comparer;
            _hash = hash;
        }

        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _hash(obj);
        }
    }
}

