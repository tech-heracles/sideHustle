using System;
using CacheManager.Core;

namespace CacheLayer
{
    public class CacheConfiguration
    {
        /// <summary>
        /// Ben te mundur aktivizimin e cache layer per tu perdorur ne te gjithe applikacionin
        /// </summary>
        /// <param name="sessionTimeout">default timeout per kohen e qendrimit te nje elementi ne cache</param>
        public static void ConfigureCache(TimeSpan sessionTimeout)
        {

            var configs = new Configs
            {
                CacheName = "MyCache",
                ExpirationTime = sessionTimeout,
                ExpirationMode = ExpirationMode.None
            };
            ApplyConfiguration(configs);
        }
        /// <summary>
        /// inicializon cachemanagerin
        /// </summary>
        /// <param name="configs"></param>
        private static void ApplyConfiguration(Configs configs)
        {
            var configuration = new ConfigurationBuilder(configs.CacheName)
                         .WithSystemRuntimeCacheHandle()
                         .WithExpiration(configs.ExpirationMode, configs.ExpirationTime)
                         .EnablePerformanceCounters()
                         .EnableStatistics()
                         .Build();
            var myAppCache = CacheFactory.FromConfiguration<object>(configuration);
            //var myAppCache = new CacheManagerFake(configs);
            GlobalCacheManager.Initialize(myAppCache,configs);
        }

    }
}
