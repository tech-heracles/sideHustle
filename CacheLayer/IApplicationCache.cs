using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLayer
{
    public interface IApplicationCache:ICache
    {
        void Set<TCacheValue>(string key, TCacheValue value,TimeSpan timeExpiration);
        bool Add<TCacheValue>(string key, TCacheValue value, TimeSpan timeExpiration);
    }
}
