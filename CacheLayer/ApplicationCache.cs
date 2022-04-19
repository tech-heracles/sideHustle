using CacheManager.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLayer
{
    public class ApplicationCache : Cache, IApplicationCache
    {

        public ApplicationCache(ICacheManager<object> cache, Configs configs)
        {
            _allKeys = new List<string>();
            myCache = cache;
            _allKeys = new List<string>();
            ExpirationMode = configs.ExpirationMode;
            ExpirationTime = configs.ExpirationTime;
        }

        public List<string> AllKeys => _allKeys ?? new List<string>();

        public bool Add<TCacheValue>(string key, TCacheValue value)
        {
            return Add(key, value, ExpirationTime);
        }

        public bool Add<TCacheValue>(string key, TCacheValue value, TimeSpan expirationTime)
        {
            Set(key, value, ExpirationTime);
            return true;
        }

        public void Clear()
        {
            myCache.Clear();
            AllKeys.Clear();
        }

        public TCacheValue Get<TCacheValue>(string key)
        {
            return myCache.Get<TCacheValue>(key);
        }
        /// <summary>
        /// kekron ne cache per vleren me celesin cref="key" ,nese nuk e gjen therret funksionin e kaluar si parameter
        /// e ruan ne cache dhe kthen vleren e tij.
        /// </summary>
        /// <typeparam name="TCacheValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="sourceCallback">funksioni qe do thirret nese vlera nuk gjendet</param>
        /// <returns></returns>
        public TCacheValue Get<TCacheValue>(string key, Func<TCacheValue> sourceCallback)
        {
            var requiredValue = Get<TCacheValue>(key);
            if (requiredValue == null)
            {
                requiredValue = sourceCallback.Invoke();
                Set(key, requiredValue);
            }
            return requiredValue;
        }

        public string GetAllAsJson()
        {
#if !DEBUG
            throw new NotImplementedException("Kjo metode nuk eshte implementuar qe te perdoret ne production!");
#else
            var dicJson = new Dictionary<string, object>();
            AllKeys.ForEach(key => dicJson.Add(key, myCache.Get(key)));
            return JsonConvert.SerializeObject(dicJson);
#endif
        }

        public bool Remove(string key)
        {
            return myCache.Remove(key);
        }

        public void Set<TCacheValue>(string key, TCacheValue value)
        {
            var cacheItem = new CacheItem<TCacheValue>(key, value, ExpirationMode, ExpirationTime);
            Set(key, value, ExpirationTime);
        }
        /// <typeparam name="TCacheValue"></typeparam>
        /// <param name="key">identifikuesi ne cache</param>
        /// <param name="value">vlera qe do ruhet</param>
        /// <param name="sourceCallback">funksioni i cili duhet te thirret kur vlera e kerkuar nuk gjendet</param>


        public void Set<TCacheValue>(string key, TCacheValue value, TimeSpan timeExpiration)
        {
            myCache.Put(new CacheItem<object>(key, value, ExpirationMode, ExpirationTime));
        }

        public void Set<TCacheValue>(string key, TCacheValue value, bool expire)
        {
            myCache.Put(expire ? new CacheItem<object>(key, value, ExpirationMode, ExpirationTime) : new CacheItem<object>(key, value));
        }
    }
}
