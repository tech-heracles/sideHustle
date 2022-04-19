using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLayer
{
    /// <summary>
    /// super klase abstrakte per dy llojet e cache,
    /// </summary>
    public abstract class Cache
    {
        public TimeSpan ExpirationTime { get; protected set; }
        public ExpirationMode ExpirationMode { get; protected set; }
        public List<string> _allKeys { get; protected set; }
        public ICacheManager<object> myCache { get; protected set; }
    }
}
