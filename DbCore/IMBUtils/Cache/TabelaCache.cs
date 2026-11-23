using System;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using AlphaWeb.Core.Interfaces.Data;
using NLog;

namespace DbCore.IMBUtils.Cache
{
    /// <summary>
    /// Cache per rezultatet e procedurave qe lexojne vetem nga nje grup tabelash qe ndryshojne rralle (konfigurimi i
    /// gridave, te drejtat e roleve). Rezultati i ruajtur perdoret vetem nese asnje nga tabelat nuk ka ndryshuar qe kur u
    /// lexua: koha e ndryshimit te fundit lexohet nga sys.dm_db_index_usage_stats (nje query ~1 ms). Keshtu kapet cdo
    /// ndryshim, edhe ai qe behet jashte aplikacionit ose nga procedura te tjera.
    /// <para>Koha e ndryshimit lexohet nga databaza me se shumti nje here ne <see cref="IntervaliStampes"/> per cdo
    /// databaze dhe grup tabelash; pra nje ndryshim duket me se voni pas kaq kohe.</para>
    /// <para>Nuk ruhet asgje qe lexohet menjehere pas nje ndryshimi (mund te jete ende brenda nje transaksioni te pambyllur),
    /// dhe thirresi merr gjithmone nje kopje, qe ndryshimet e tij te mos prekin cache-in.</para>
    /// <para>Nese perdoruesi i databazes nuk ka VIEW SERVER STATE, ose stampa nuk lexohet, cdo lexim behet nga databaza si me pare.</para>
    /// </summary>
    public sealed class TabelaCache
    {
        public static readonly TimeSpan IntervaliStampes = TimeSpan.FromSeconds(2);
        private static readonly TimeSpan PritjaPasNdryshimit = TimeSpan.FromSeconds(10);
        private static readonly TimeSpan JetegjatesiaMax = TimeSpan.FromMinutes(30);
        private const int NrMaxHyrjesh = 5000;

        private sealed class Hyrje
        {
            public DataTable Tabela;
            public DateTime? Stampa;
            public DateTime Koha;
        }

        private sealed class Stampe
        {
            public DateTime? Vlera;
            public DateTime TaniDb;
            public long LexuarMe;   // Stopwatch.GetTimestamp()
        }

        private readonly string emri;
        private readonly string sqlStampa;
        private readonly ConcurrentDictionary<string, Hyrje> hyrjet = new ConcurrentDictionary<string, Hyrje>();
        private readonly ConcurrentDictionary<string, Stampe> stampat = new ConcurrentDictionary<string, Stampe>();
        private volatile bool stampaNukLexohet;

        /// <param name="emri">emri per log-un</param>
        /// <param name="tabelat">tabelat nga te cilat lexojne procedurat e ruajtura ne kete cache</param>
        public TabelaCache(string emri, params string[] tabelat)
        {
            this.emri = emri;
            sqlStampa = "SELECT MAX(last_user_update), GETDATE() FROM sys.dm_db_index_usage_stats " +
                        "WHERE database_id = DB_ID() AND object_id IN (" +
                        string.Join(", ", Array.ConvertAll(tabelat, t => "OBJECT_ID('" + t + "')")) + ")";
        }

        /// <param name="dbManager">lidhja me te cilen lexohet (dhe databaza, qe hyn ne celes)</param>
        /// <param name="celesi">procedura dhe te gjithe parametrat qe ndikojne ne rezultat</param>
        /// <param name="lexo">leximi nga databaza; vendos vete parametrat e veta</param>
        public DataTable Merr(IDbManager dbManager, string celesi, Func<DataTable> lexo)
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

        private bool LexoStampen(IDbManager dbManager, out DateTime? stampa, out DateTime tani)
        {
            stampa = null;
            tani = DateTime.MinValue;
            string cs = dbManager.ConnectionString;
            long ticks = Stopwatch.GetTimestamp();
            if (stampat.TryGetValue(cs, out Stampe s))
            {
                TimeSpan kaluar = TimeSpan.FromSeconds((ticks - s.LexuarMe) / (double)Stopwatch.Frequency);
                if (kaluar >= TimeSpan.Zero && kaluar < IntervaliStampes)
                {
                    stampa = s.Vlera;
                    tani = s.TaniDb + kaluar;
                    return true;
                }
            }
            try
            {
                DataRow r = dbManager.ExecuteDataSet(CommandType.Text, sqlStampa).Tables[0].Rows[0];
                stampa = r[0] == DBNull.Value ? (DateTime?)null : (DateTime)r[0];
                tani = (DateTime)r[1];
                stampat[cs] = new Stampe { Vlera = stampa, TaniDb = tani, LexuarMe = ticks };
                return true;
            }
            catch (Exception ex)
            {
                // perdoruesi i databazes pa VIEW SERVER STATE: punohet pa cache, si me pare
                if (ex is System.Data.SqlClient.SqlException se && se.Number == 300)
                    stampaNukLexohet = true;
                LogManager.GetCurrentClassLogger().Warn(ex, emri + ": nuk lexohet koha e ndryshimeve, leximi behet pa cache");
                return false;
            }
        }
    }
}
