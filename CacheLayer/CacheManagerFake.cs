using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheManager.Core.Internal;

namespace CacheLayer
{
    public class CacheManagerFake : ICacheManager<object>
    {

        System.Runtime.Caching.ObjectCache cache;
        System.Runtime.Caching.CacheItemPolicy policy;
        Configs config;

        public CacheManagerFake(Configs config)
        {
            cache = new System.Runtime.Caching.MemoryCache("myCache");
            this.config = config;
            policy = new System.Runtime.Caching.CacheItemPolicy()
            {
                SlidingExpiration = config.ExpirationTime
            };
        }
        private System.Runtime.Caching.CacheItem getCacheItem(CacheItem<object> cacheItem)
        {
            return new System.Runtime.Caching.CacheItem(cacheItem.Key, cacheItem.Value);
        }
        private System.Runtime.Caching.CacheItem getCacheItem(CacheItem<object> cacheItem, string region)
        {
            return new System.Runtime.Caching.CacheItem(ComposedKey(cacheItem.Key, region), cacheItem.Value, region);
        }
        private CacheItem<object> getCacheItem(System.Runtime.Caching.CacheItem cacheItem)
        {
            return new CacheItem<object>(cacheItem.Key, cacheItem.RegionName, cacheItem.Value);
        }
        private string ComposedKey(string key, string region)
        {
            return $"{key}_{region}";
        }
        public object this[string key, string region] { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public IReadOnlyCacheManagerConfiguration Configuration() { throw new NotImplementedException(); }

        public string Name { get { throw new NotImplementedException(); } }

        public IEnumerable<BaseCacheHandle<object>> CacheHandles { get { throw new NotImplementedException(); } }

        IReadOnlyCacheManagerConfiguration ICacheManager<object>.Configuration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object this[string key] { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public event EventHandler<CacheActionEventArgs> OnAdd;
        public event EventHandler<CacheClearEventArgs> OnClear;
        public event EventHandler<CacheClearRegionEventArgs> OnClearRegion;
        public event EventHandler<CacheActionEventArgs> OnGet;
        public event EventHandler<CacheActionEventArgs> OnPut;
        public event EventHandler<CacheActionEventArgs> OnRemove;
        public event EventHandler<CacheActionEventArgs> OnUpdate;
        public event EventHandler<CacheItemRemovedEventArgs> OnRemoveByHandle;

        public bool Add(string key, object value)
        {
            cache.Set(key, value, policy);
            return true;
        }

        public bool Add(string key, object value, string region)
        {
            cache.Set(ComposedKey(key, region), value, policy);
            return true;
        }

        public bool Add(CacheItem<object> item)
        {
            cache.Set(getCacheItem(item), policy);
            return true;
        }

        public object AddOrUpdate(string key, object addValue, Func<object, object> updateValue)
        {
            throw new NotImplementedException();
        }

        public object AddOrUpdate(string key, string region, object addValue, Func<object, object> updateValue)
        {
            throw new NotImplementedException();
        }

        public object AddOrUpdate(string key, object addValue, Func<object, object> updateValue, int maxRetries)
        {
            throw new NotImplementedException();
        }

        public object AddOrUpdate(string key, string region, object addValue, Func<object, object> updateValue, int maxRetries)
        {
            throw new NotImplementedException();
        }

        public object AddOrUpdate(CacheItem<object> addItem, Func<object, object> updateValue)
        {
            throw new NotImplementedException();
        }

        public object AddOrUpdate(CacheItem<object> addItem, Func<object, object> updateValue, int maxRetries)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (var item in cache.ToList())
            {
                cache.Remove(item.Key);
            }
        }

        public void ClearRegion(string region)
        {
            foreach (var item in cache.ToList())
            {
                cache.Remove(ComposedKey(item.Key, region));
            }
        }

        public void Dispose()
        {
            cache = new System.Runtime.Caching.MemoryCache("myCache");
        }

        public void Expire(string key, ExpirationMode mode, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void Expire(string key, string region, ExpirationMode mode, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void Expire(string key, DateTimeOffset absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Expire(string key, string region, DateTimeOffset absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Expire(string key, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Expire(string key, string region, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }

        public object Get(string key, string region)
        {
            return cache.Get(ComposedKey(key, region));
        }

        public TOut Get<TOut>(string key)
        {
            var value = cache.Get(key);
            if (value != null)
            {
                try
                {
                    return (TOut)value;
                }
                catch (Exception)
                {
                    return (TOut)Convert.ChangeType(value, typeof(TOut));
                }

            }
            return default(TOut);
        }

        public TOut Get<TOut>(string key, string region)
        {
            var value = cache.Get(ComposedKey(key, region));
            if (value != null)
            {
                try
                {
                    return (TOut)value;
                }
                catch (Exception)
                {
                    return (TOut)Convert.ChangeType(value, typeof(TOut));
                }
            }
            return default(TOut);
            //return (TOut)Convert.ChangeType(cache.Get(ComposedKey(key, region)), typeof(TOut));
        }

        public CacheItem<object> GetCacheItem(string key)
        {
            return getCacheItem(cache.GetCacheItem(key));
        }

        public CacheItem<object> GetCacheItem(string key, string region)
        {
            return getCacheItem(cache.GetCacheItem(ComposedKey(key, region)));
        }

        public object GetOrAdd(string key, object value)
        {
            throw new NotImplementedException();
        }

        public object GetOrAdd(string key, string region, object value)
        {
            throw new NotImplementedException();
        }

        public object GetOrAdd(string key, Func<string, object> valueFactory)
        {
            throw new NotImplementedException();
        }

        public object GetOrAdd(string key, string region, Func<string, string, object> valueFactory)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, object value)
        {
            cache.Set(key, value, policy);

        }

        public void Put(string key, object value, string region)
        {
            cache.Set(ComposedKey(key, region), value, policy);

        }

        public void Put(CacheItem<object> item)
        {
            cache.Set(getCacheItem(item), policy);
        }

        public bool Remove(string key)
        {
            cache.Remove(key);
            return true;
        }

        public bool Remove(string key, string region)
        {
            cache.Remove(ComposedKey(key, region));
            return true;
        }

        public void RemoveExpiration(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveExpiration(string key, string region)
        {
            throw new NotImplementedException();
        }

        public bool TryGetOrAdd(string key, Func<string, object> valueFactory, out object value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetOrAdd(string key, string region, Func<string, string, object> valueFactory, out object value)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdate(string key, Func<object, object> updateValue, out object value)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdate(string key, string region, Func<object, object> updateValue, out object value)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdate(string key, Func<object, object> updateValue, int maxRetries, out object value)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdate(string key, string region, Func<object, object> updateValue, int maxRetries, out object value)
        {
            throw new NotImplementedException();
        }

        public object Update(string key, Func<object, object> updateValue)
        {
            throw new NotImplementedException();
        }

        public object Update(string key, string region, Func<object, object> updateValue)
        {
            throw new NotImplementedException();
        }

        public object Update(string key, Func<object, object> updateValue, int maxRetries)
        {
            throw new NotImplementedException();
        }

        public object Update(string key, string region, Func<object, object> updateValue, int maxRetries)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key, string region)
        {
            throw new NotImplementedException();
        }

        public CacheItem<object> GetOrAdd(string key, Func<string, CacheItem<object>> valueFactory)
        {
            throw new NotImplementedException();
        }

        public CacheItem<object> GetOrAdd(string key, string region, Func<string, string, CacheItem<object>> valueFactory)
        {
            throw new NotImplementedException();
        }

        public bool TryGetOrAdd(string key, Func<string, CacheItem<object>> valueFactory, out CacheItem<object> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetOrAdd(string key, string region, Func<string, string, CacheItem<object>> valueFactory, out CacheItem<object> item)
        {
            throw new NotImplementedException();
        }
    }
}
