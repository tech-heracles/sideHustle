using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLayer
{
   public interface ISessionCache : ICache
    {
        /// <summary>
        /// session id e userit,e cila perdoret si region per global cache
        /// </summary>
        string SessionID { get; }
        void Set<TCacheValue>(string key, TCacheValue value, bool meScopeID, bool expire);
        TCacheValue Get<TCacheValue>(string key, bool meScopeID);


    }
}
