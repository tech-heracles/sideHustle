using CacheManager.Core;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

namespace CacheLayer
{
    public static class GlobalCacheManager
    {
        private static ICacheManager<object> _globalCache;
        private static IApplicationCache _appCache;
        private static Configs _cacheConfiguration;

        internal static ConcurrentDictionary<string, ISessionCache> AllSessionCache { get; private set; }
        /// <summary>
        /// inicializon cache per perdoruesin
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="configs"></param>
        internal static void Initialize(ICacheManager<object> cacheManager, Configs configs)
        {
            _cacheConfiguration = configs;
            _globalCache = cacheManager;
            _globalCache.OnClear += (s, e) => { Debug.WriteLine("U pastrua cache!"); };
            AllSessionCache = new ConcurrentDictionary<string, ISessionCache>();
        }

        /// <summary>
        /// Ofron akses tek cache qe i perket ketij sessioni
        /// </summary>
        public static SessionCache MySessionCache => GetSessionCacheByKey(SessionCache.GetCurrentSessionId);

        public static PageCache MyPageCache => new PageCache(GetSessionCacheByKey(SessionCache.GetCurrentSessionId));

        /// <summary>
        /// ben te mundur te kapesh cache e nje faqeje ne baze te id-se
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static PageCache GetPageCacheByPageID(string pageID) => new PageCache(GetSessionCacheByKey(SessionCache.GetCurrentSessionId), pageID);

        /// <summary>
        /// ofron akses tek cache specifike per kte sesion,ose krijon nje te re
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public static SessionCache GetSessionCacheByKey(string sessionKey)
        {
            if (!AllSessionCache.TryGetValue(sessionKey, out ISessionCache sessionCache))
                return CreateNewSessionCache(sessionKey);
            return (SessionCache)sessionCache;
        }
        /// <summary>
        /// ofron akses tek cache ne nivel app
        /// </summary>
        public static ApplicationCache MyAppCache => _appCache == null ? new ApplicationCache(_globalCache, _cacheConfiguration) : (ApplicationCache)_appCache;
        /// <summary>
        /// inicializon nje objekt sessionCache per te mbajtur te izoluar nje region te caktuar ne applicationCache
        /// ky objekt vendoset ne concurrentDictionary
        /// </summary>
        /// <param name="sessionID"></param>
        public static SessionCache CreateNewSessionCache(string sessionID)
        {
            var newSessionCache = new SessionCache(sessionID, _globalCache, _cacheConfiguration);
            AllSessionCache[sessionID] = newSessionCache;
            Debug.WriteLine($"Session Cache u inicializua me sukses per sessionID {sessionID}!");
            return newSessionCache;
        }
        /// <summary>
        /// shkaterron objektin
        /// </summary>
        /// <param name="sessionID"></param>
        public static void DestroySessionCache(string sessionID)
        {
            if (!AllSessionCache.TryRemove(sessionID, out ISessionCache sessionCache))
                throw new Exception($"Deshtoi shkaterrimi i cache per sessionID {sessionID}");
            sessionCache.Clear();

            Debug.WriteLine($"Session Cache u shkaterrua me sukses per sessionID {sessionID}!");

        }

        public static void SetTemporaryScopeId(string scopeId)
        {
            MySessionCache.TemporaryScopeId = scopeId;
        }
        public static void RemoveTemporaryScopeId()
        {
            MySessionCache.TemporaryScopeId = string.Empty; ;

        }


        public static bool EshteScopeIdAktiv()
        {
            if (MySessionCache == null)
            {
                throw new Exception("NullError MySessionCacheis null");
            }
            var scope = MySessionCache.ScopeID;
            if (string.IsNullOrWhiteSpace(scope))
                return true;

            if (MySessionCache.AllKeys == null)
            {
                throw new Exception("NullError MySessionCache.AllKeys");
            }
            foreach (var keys in MySessionCache.AllKeys)
            {
                if (scope == null)
                {
                    throw new Exception("NullError scope") { Data = { { "mesazh", "scope == null" }, { "ObjektiMySessionCache", MySessionCache.GetAllAsJson().ToString() }, { "scope", scope } } };
                }
                if (keys != null && keys.Contains(scope))
                    return true;
            }

            ScopeManager.HiqScopeId(scope);
            return false;
        }
    }
}
