using CacheManager.Core;
using CacheManager.Core.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheLayer
{
    public class SessionCache : Cache, ISessionCache
    {
        internal string TemporaryScopeId;
        public string SessionID { get; }

        public string ScopeID => !string.IsNullOrWhiteSpace(TemporaryScopeId) ? TemporaryScopeId : HttpContext.Current?.Request["scopeID"];
        /// <summary>
        /// kthen session ID e kesaj 
        /// </summary>
        internal static string GetCurrentSessionId => HttpContext.Current?.Session.SessionID;
        public List<string> AllKeys => _allKeys ?? new List<string>();

        /// <summary>
        /// Set/Get object in/from cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire">True - item should expire after configured time, False - no expire</param>
        /// <returns></returns>
        public object this[string key, bool expire = false]
        {
            get { return Get<object>(key); }
            set { Set(key, value, expire); }
        }

        public SessionCache(string sessionID, ICacheManager<object> cache, Configs configs)
        {
            SessionID = sessionID;
            _allKeys = new List<string>();
            myCache = cache;
            myCache.OnRemoveByHandle += (sender, arg) => RemoveKey(sender, arg);// (sender, arg) => RemoveKey(arg.Key);//RemoveKey(sender, arg);
            ExpirationMode = configs.ExpirationMode;
            ExpirationTime = configs.ExpirationTime;

        }

        private void RemoveKey(object sender, CacheItemRemovedEventArgs arg)
        {
            try
            {
                AllKeys.RemoveAll(x => x == arg.Key);
                #if DEBUG
                    System.Diagnostics.Debug.Write($"AllKeys = {AllKeys.Count} , {arg.Key} , {arg.Reason} , {arg.Level} , {arg.Region} \n");
                #endif
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Add<TCacheValue>(string key, TCacheValue value)
        {
            return Add(key, value, true);
        }
        public bool Add<TCacheValue>(string key, TCacheValue value, bool meScopeId)
        {

            Set(key, value, meScopeId, false);
            return true;
        }

        public void Clear()
        {
            myCache.ClearRegion(SessionID);
            AllKeys.Clear();
        }

        public bool Remove(string key) => Remove(key, true);

        public bool Remove(string key, bool meScopeId)
        {
            if (meScopeId)
                key = ComposedKey(key);
            AllKeys.Remove(key);
            return myCache.Remove(key, SessionID);
        }

        public TCacheValue Get<TCacheValue>(string key) => Get<TCacheValue>(key, true);
        public TCacheValue Get<TCacheValue>(string key, bool meScopeID)
        {
            if (meScopeID) key = ComposedKey(key);
            return myCache.Get<TCacheValue>(key, SessionID);
        }
        public void Set<TCacheValue>(string key, TCacheValue value, bool expire)
        {
            Set(key, value, true, expire);

        }
        public void Set<TCacheValue>(string key, TCacheValue value, bool meScopeID, bool expire)
        {
            var composedKey = key;
            if (meScopeID) composedKey = ComposedKey(key);
            if (null == value)
            {
                Remove(composedKey); //Remove(key);
                return;
            }

            myCache.Put(composedKey, value, SessionID);
            if (expire && Configs.SessionCacheExpirationTime.TotalSeconds > 0)
            {
                #if DEBUG
                    System.Diagnostics.Debug.Write($"AllKeys = {AllKeys.Count} , {composedKey} \n");
                #endif
                myCache.Expire(composedKey, SessionID, ExpirationMode.Sliding, Configs.SessionCacheExpirationTime);
            }

            if (AllKeys.IndexOf(composedKey) == -1)
            {
                AllKeys.Add(composedKey);
                return;
            }

        }
        private string ComposedKey(string key)
        {
            if (!string.IsNullOrEmpty(ScopeID) && !string.IsNullOrEmpty(key))
                return !(key.Contains(ScopeID)) ? $"{ScopeID}_{key}" : $"{key}";                
            else
                return $"{ScopeID}_{key}";
        }
        public string GetAllAsJson()
        {
#if !DEBUG
            throw new NotImplementedException("Kjo metode nuk eshte implementuar qe te perdoret ne production!");
#else
            var dicJson = new Dictionary<string, object>();
            AllKeys.ForEach(key => dicJson.Add(key, myCache.Get(key, SessionID)));
            return JsonConvert.SerializeObject(dicJson);
#endif
        }
    }
}
