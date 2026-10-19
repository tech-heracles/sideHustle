using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using DbCore.IMBUtils.DataBase;
using DbCore.DbListPagesat;

namespace DbCore
{
    public static class StaticCache
    {
        // Komponentet e pages lexohen nga disa thread-e njeheresh (llogaritja e pages ne paralel, disa perdorues), prandaj
        // ruhen ne nje ConcurrentDictionary. Me pare ishte Dictionary + List qe shtoheshin pa sinkronizim: dy shtime njeheresh
        // mund te prishnin listen (gabime ose thread qe rrotullohet pa fund ne 100% CPU). Celesi perfshin databazen, sepse
        // id-te e komponenteve perseriten ne databaza te ndryshme.
        private static readonly ConcurrentDictionary<string, clsKomponentePage> komponentet = new ConcurrentDictionary<string, clsKomponentePage>();

        public static void ClearCache()
        {
            komponentet.Clear();
        }

        public static clsKomponentePage GetKomponentePage(int idKomponentePage, clsDatabazeListPagesa data)
        {
            var celesi = Databaza() + "|" + idKomponentePage;
            if (komponentet.TryGetValue(celesi, out var ruajtur))
            {
                Debug.WriteLine($"Cache - Mbush komponente page me id: {idKomponentePage}");
                return ruajtur.Clone();
            }

            var kompPage = new clsKomponentePage();
            data.ktheKomponentePage(idKomponentePage, kompPage);
            Debug.WriteLine($"DB - Mbush komponenten nga id: {idKomponentePage}");
            if (kompPage.IdKomponentePage == 0)
                throw new MyException($"komponentePage me id: {idKomponentePage} nuk ekziston");
            komponentet[celesi] = kompPage.Clone();
            return kompPage;
        }

        private static string Databaza()
        {
            try
            {
                return MyConnectionsManager.GetSelectedConNameServer();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
