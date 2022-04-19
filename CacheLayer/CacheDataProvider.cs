using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using CacheLayer;

namespace DbCore.IMBUtils.Cache
{
    public static class CacheDataProvider
    {

        static CacheDataProvider()
        {
            
        }
        /// <summary>
        /// funksion generic i cili kerkon nje objekt te nje tipi te caktuar ne cache ne baze te session key,nese e gjen 
        /// mapon objektin e gjetur ne objektin thirres,ne rast te kundert mbush objektin qe e thirri nga db duke thirrur funksionin,
        /// qe merr si parameter per te mbushur objektin e kerkuar. 
        /// </summary>
        /// <typeparam name="T">tipi</typeparam>
        /// <param name="predicate">funksioni sipas te cilit do te behet kerkimi ne listen qe ndodhet ne session</param>
        /// <param name="funksioniMbushesNgaDb">funksioni i cili do thirret nese objekti i kerkuar sipas filtrit te dhene nuk gjendet</param>
        /// <param name="objektiDest">objekti ne te cilin do kopjohet objekti i gjetur sipas filtrit</param>
        /// <param name="saveNewCopyToCache">ne rastet kur referenca perdoret per objekte te ndryshme,
        /// eshte e nevojshme per te ruajtur nje kopje te objektit te gjetur ne session,per te mos referuar tek i njejti objekt</param>
        /// <returns>objekti i kerkuar</returns>
        public static T FillObjectFromCache<T>(this ICache appCache, Func<T, bool> predicate, Func<T> funksioniMbushesNgaDb, T objektiDest,string customKey="", bool saveNewCopyToCache = false)
        {
            var sessionKey =$"{typeof(T).FullName}_{customKey}";
            //kerkojme ne session nese gjejme ndonje list me element te ketij tpi
            var ngaCache = appCache.Get<List<T>>(sessionKey) ?? new List<T>();
            var objektiKerkuar = ngaCache.FirstOrDefault(predicate);
            if (objektiKerkuar == null)
            {
                Debug.WriteLine($"objekt  FROM DB");
                objektiKerkuar = funksioniMbushesNgaDb.Invoke();

                ngaCache.Add(saveNewCopyToCache ? Mapper.Map<T, T>(objektiKerkuar) : objektiKerkuar);
                appCache.Set(sessionKey, ngaCache, false);
                return objektiKerkuar;
            }
            Mapper.Map(objektiKerkuar, objektiDest);
            Debug.WriteLine($"objekt  FROM SESSION");
            return objektiKerkuar;
        }
        /// <summary>
        /// heq nga session cache objektet me celsat e kaluar si param 
        /// </summary>
        /// <param name="keys"></param>
        public static void ClearSessionCache(params string[] keys)
        {

            var cache = GlobalCacheManager.MySessionCache;
            var keysToRemove = new List<string>();
            foreach (var key in cache.AllKeys)
            {
                if (ContainsAnyIgnoreCase(key,keys)) keysToRemove.Add(key);
            }
            keysToRemove.ForEach(key =>
            {
                cache.Remove(key, false);
            });
        }

        public static bool ContainsAnyIgnoreCase(string stringu, params string[] vlera)
        {
            return !string.IsNullOrEmpty(stringu) && vlera.Any(item => stringu.IndexOf(item, StringComparison.InvariantCultureIgnoreCase) > -1);
        }
    }
}
