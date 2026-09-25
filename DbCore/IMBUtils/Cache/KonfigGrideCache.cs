using System;
using System.Collections.Concurrent;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using NLog;

namespace DbCore.IMBUtils.Cache
{
    /// <summary>
    /// Cache per konfigurimin e gridave (T_GRIDATRUPI e tabelat qe lidhen me te). Keto lexohen disa here per cdo faqe dhe
    /// T_GRIDATRUPI ka mbi nje milion rreshta pa indeks per GRIDAKOKAID, keshtu qe cdo lexim kushtonte 100-250 ms.
    /// <para>Nje rezultat i ruajtur perdoret vetem nese tabelat nuk kane ndryshuar qe kur u lexua: ne cdo kerkese lexohet
    /// koha e ndryshimit te fundit nga sys.dm_db_index_usage_stats (nje query ~1 ms). Keshtu kapet cdo ndryshim, edhe ai
    /// qe behet jashte aplikacionit ose nga procedura te tjera.</para>
    /// <para>Nuk ruhet asgje qe lexohet menjehere pas nje ndryshimi (mund te jete ende brenda nje transaksioni te pambyllur),
    /// dhe thirresi merr gjithmone nje kopje, qe ndryshimet e tij te mos prekin cache-in.</para>
    /// </summary>
    public static class KonfigGrideCache
    {
        private const string SqlStampa =
            "SELECT MAX(last_user_update), GETDATE() FROM sys.dm_db_index_usage_stats " +
            "WHERE database_id = DB_ID() AND object_id IN (OBJECT_ID('T_GRIDATRUPI'), OBJECT_ID('T_GRIDAKOKA'), " +
            "OBJECT_ID('T_LUPAMULTIPLE'), OBJECT_ID('T_KONFIGAMBJENTE'), OBJECT_ID('T_KOMPONENTE'))";

        private static readonly TimeSpan PritjaPasNdryshimit = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan JetegjatesiaMax = TimeSpan.FromMinutes(30);
        private const int NrMaxHyrjesh = 5000;

        private sealed class Hyrje
        {
            public DataTable Tabela;
            public DateTime? Stampa;
            public DateTime Koha;
        }

        private static readonly ConcurrentDictionary<string, Hyrje> hyrjet = new ConcurrentDictionary<string, Hyrje>();
        private static volatile bool stampaNukLexohet;

        /// <param name="dbManager">lidhja me te cilen lexohet (dhe databaza, qe hyn ne celes)</param>
        /// <param name="celesi">procedura dhe parametrat</param>
        /// <param name="lexo">leximi nga databaza; vendos vete parametrat e veta</param>
        public static DataTable Merr(IDbManager dbManager, string celesi, Func<DataTable> lexo)
        {
            DateTime? stampa;
            DateTime tani;
            if (stampaNukLexohet || !LexoStampen(dbManager, out stampa, out tani))
                return lexo();

            string k = dbManager.ConnectionString + "\u0001" + celesi;
            if (hyrjet.TryGetValue(k, out Hyrje h) && h.Stampa == stampa && tani - h.Koha < JetegjatesiaMax)
                return h.Tabela.Copy();

            DataTable rezultati = lexo();
            if (rezultati != null && (stampa == null || tani - stampa.Value > PritjaPasNdryshimit))
            {
                if (hyrjet.Count >= NrMaxHyrjesh)
                    hyrjet.Clear();
                hyrjet[k] = new Hyrje { Tabela = rezultati.Copy(), Stampa = stampa, Koha = tani };
            }
            return rezultati;
        }

        private static bool LexoStampen(IDbManager dbManager, out DateTime? stampa, out DateTime tani)
        {
            stampa = null;
            tani = DateTime.MinValue;
            try
            {
                DataRow r = dbManager.ExecuteDataSet(CommandType.Text, SqlStampa).Tables[0].Rows[0];
                stampa = r[0] == DBNull.Value ? (DateTime?)null : (DateTime)r[0];
                tani = (DateTime)r[1];
                return true;
            }
            catch (Exception ex)
            {
                // perdoruesi i databazes pa VIEW SERVER STATE: punohet pa cache, si me pare
                if (ex is System.Data.SqlClient.SqlException se && se.Number == 300)
                    stampaNukLexohet = true;
                LogManager.GetCurrentClassLogger().Warn(ex, "KonfigGrideCache: nuk lexohet koha e ndryshimeve, leximi behet pa cache");
                return false;
            }
        }
    }
}
