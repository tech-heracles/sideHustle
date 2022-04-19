using CacheManager.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace CacheLayer
{
    public class PageCache
    {
        public ISessionCache MyCache;

        /// <summary>
        /// kthen session ID e kesaj kerkese
        /// </summary>
        public static string CurrentPageId
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;

                var currentHandler = (HttpContext.Current.CurrentHandler as IMyPage);
                return currentHandler == null ? string.Empty : currentHandler.MerrIdentifikuesFaqje();
            }
        }
        public string PageId { get; }
        public string ComposedItemKey(string key)
        {
            if (string.IsNullOrWhiteSpace(PageId))
                return $"{CurrentPageId}_{key}";
            return $"{PageId}_{key}";
        }


        public object this[string key, bool expire = false]
        {
            get { return Get<object>(key); }
            set { Set(key, value, expire); }
        }
        public PageCache(ISessionCache cache)
        {
            MyCache = cache;
        }
        public PageCache(ISessionCache cache, string pageID)
        {
            MyCache = cache;
            PageId = pageID;
        }
        public bool Add<TCacheValue>(string key, TCacheValue value)
        {
            Set(key, value, false);
            return true;
        }

        public void Clear()
        {
            MyCache.Clear();
        }

        public TCacheValue Get<TCacheValue>(string key)
        {
            return Get<TCacheValue>(key, true);
        }
        public TCacheValue Get<TCacheValue>(string key, bool shtoPageId)
        {
            if (shtoPageId)
                key = ComposedItemKey(key);
            return MyCache.Get<TCacheValue>(key);
        }
        public bool Remove(string key)
        {
            return MyCache.Remove(ComposedItemKey(key));
        }

        public void Set<TCacheValue>(string key, TCacheValue value, bool expire)
        {
            if (null == value)
            {
                Remove(key);
                return;
            }
            MyCache.Set(ComposedItemKey(key), value, expire);
        }


        public string GetAllAsJson()
        {
#if !DEBUG
            throw new NotImplementedException("Kjo metode nuk eshte implementuar qe te perdoret ne production!");
#else
            return MyCache.GetAllAsJson();
#endif
        }

    }
}
