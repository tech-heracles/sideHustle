using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLayer
{
    public class Configs
    {
        public string CacheName { get; set; }
        public  TimeSpan ExpirationTime { get; set; }
        public ExpirationMode ExpirationMode { get; set; }

        /// <summary>
        /// Get, Set Expiration time value of items in cache
        /// </summary>
        public static TimeSpan SessionCacheExpirationTime { get; set; }

    }
}
