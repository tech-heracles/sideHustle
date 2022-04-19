using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLayer
{
    public interface ICache
    {
        void Set<TCacheValue>(string key, TCacheValue value, bool expire);
        TCacheValue Get<TCacheValue>(string key);
        bool Remove(string key);
        bool Add<TCacheValue>(string key, TCacheValue value);
        void Clear();
        /// <summary>
        /// nje liste me te gjithe keys te shtuar ne kete cache
        /// </summary>
        List<string> AllKeys { get; }


        /// <summary>
        /// kthen nje json string me te gjitha nje dictionary te serializuar <key,value>,
        /// NUK DUHET TE PERDORET PER PRODUCTION
        /// </summary>
        /// <returns></returns>
        string GetAllAsJson();
    }
}
