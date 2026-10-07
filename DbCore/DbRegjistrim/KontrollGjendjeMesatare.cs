using System;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// Kontrolli i gjendjes negative per metodat mesatare (1, 2, 5, 6), i njejte me degen mesatare te
    /// prc_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullit_Modifikim (skripti 06), por mbi levizjet e artikullit te
    /// lexuara nje here. Perdoret vetem ne kontrollin e importit (asgje nuk shkruhet gjate tij), ku i njejti artikull ne
    /// te njejten magazine kontrollohet per shume rreshta: levizjet lexohen nje here per artikull/magazine.
    /// </summary>
    internal static class KontrollGjendjeMesatare
    {
        internal struct Levizje
        {
            public DateTime Data;    // T_TRUPIMAGAZINA.DATA
            public DateTime DtDok;   // T_KOKAMAGAZINA.DTDOK
            public double Q;         // SASIA * KOEFICENTI * SHENJA
        }

        internal struct Rezultati
        {
            public double Gjendje;
            public DateTime DataDok;
        }

        /// <summary>
        /// Kthen gjendjen e pare negative (si rreshti i pare i procedures), ose null kur gjendja nuk shkon negative.
        /// </summary>
        internal static Rezultati? Llogarit(IReadOnlyList<Levizje> levizjet, bool meDyData, DateTime data1, DateTime data2,
            bool kapDokMesDatave, bool eshteDalje, double sasiArtikulli, int llojVeprimi)
        {
            // si convert(datetime, cast(@data as varchar(20)), 103) i procedures: pa sekonda
            DateTime d1 = PaSekonda(data1), d2 = PaSekonda(data2);

            Func<DateTime, bool> filtri;
            if (!meDyData)
                filtri = dt => dt >= d1;
            else if (kapDokMesDatave)
                filtri = data1 < data2 ? (Func<DateTime, bool>)(dt => dt >= d1 && dt < d2) : (dt => dt >= d2 && dt < d1);
            else
                filtri = dt => dt >= d2;

            bool dalje = (eshteDalje && sasiArtikulli > 0) || (!eshteDalje && sasiArtikulli < 0);
            // @SASIARTIKULL eshte decimal(18,8) ne procedure
            double sasia = Rrumbullako(Math.Abs(sasiArtikulli));
            if (!dalje && (kapDokMesDatave || llojVeprimi == 2))
                sasia = 0;

            // gjendja progresive ne cdo date levizjeje (SUM() OVER (ORDER BY DATA))
            var sipasDates = levizjet.GroupBy(l => l.Data).OrderBy(g => g.Key).Select(g => new { Data = g.Key, Q = g.Sum(x => x.Q) }).ToList();
            var progresive = new Dictionary<DateTime, double>(sipasDates.Count);
            double shuma = 0;
            foreach (var d in sipasDates)
            {
                shuma += d.Q;
                progresive[d.Data] = shuma;
            }

            Rezultati? iPari = null;
            void Prov(DateTime data, double g)
            {
                double gj = Rrumbullako(g);
                bool negative = dalje ? (Rrumbullako(gj - sasia) < 0 || gj <= 0) : Rrumbullako(sasia + gj) < 0;
                if (!negative)
                    return;
                if (iPari == null || data < iPari.Value.DataDok)
                    iPari = new Rezultati { Gjendje = dalje ? Rrumbullako(gj - sasia) : Rrumbullako(sasia + gj), DataDok = data };
            }

            // datat me veprime pas dates se dokumentit (filtri mbi DTDOK, gjendja ne DATA)
            foreach (DateTime data in levizjet.Where(l => filtri(l.DtDok)).Select(l => l.Data).Distinct())
                Prov(data, progresive[data]);
            // dalja kontrollon edhe gjendjen deri ne daten e veprimit
            if (dalje)
                Prov(d1, levizjet.Where(l => l.Data <= d1).Sum(l => l.Q));
            return iPari;
        }

        private static DateTime PaSekonda(DateTime d) => new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);

        // ROUND(x, 8) i SQL Server-it (gjysma larg zeros)
        private static double Rrumbullako(double x) => Math.Round(x, 8, MidpointRounding.AwayFromZero);
    }
}
