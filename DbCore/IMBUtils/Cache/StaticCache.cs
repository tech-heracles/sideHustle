using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.SessionState;
using AutoMapper;
using DbCore.DbAdmin;
using DbCore.DbListPagesat;

namespace DbCore
{
    public static class StaticCache
    {
        public const string KomponentePageKey = "kompPageKey";

        private static Dictionary<string, object> _cacheData;
        public static Dictionary<string, object> CacheData => _cacheData ?? (_cacheData = new Dictionary<string, object>());



        public static void ClearCache()
        {
            _cacheData = null;
        }

        public static clsKomponentePage GetKomponentePage(int idKomponentePage, clsDatabazeListPagesa data)
        {
            clsKomponentePage kompPage = null;
            colKomponentePage myColKompPage;
            object cachedData;
            if (CacheData.TryGetValue(KomponentePageKey, out cachedData))
            {
                myColKompPage = cachedData as colKomponentePage;
                kompPage = myColKompPage?.FirstOrDefault(x => (x.IdKomponentePage == idKomponentePage));
            }
            else
            {
                myColKompPage = new colKomponentePage();
            }
            if (kompPage == null)
            {
                kompPage = new clsKomponentePage();
                data.ktheKomponentePage(idKomponentePage, kompPage);
                Debug.WriteLine($"DB - Mbush komponenten nga id: {idKomponentePage}");
                if (kompPage.IdKomponentePage == 0)
                    throw new MyException($"komponentePage me id: {idKomponentePage} nuk ekziston");
                myColKompPage.Add(kompPage);
                _cacheData[KomponentePageKey] = myColKompPage;
                return kompPage;
            }
            Debug.WriteLine($"Cache - Mbush komponente page me id: {idKomponentePage}");
            return kompPage.Clone();
        }

        
    }
}