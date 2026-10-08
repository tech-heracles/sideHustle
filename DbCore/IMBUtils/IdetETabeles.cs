using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace DbCore
{
    /// <summary>
    /// Bashkesia e vlerave te nje kolone (id) te nje DataTable qe vetem rritet (p.sh. rreshtat e paimportuar te nje importi),
    /// per te zevendesuar <c>tabela.Select("id = 'x'")</c> brenda ciklit: Select ndertonte nje indeks te ri mbi gjithe tabelen
    /// ne cdo thirrje. Rreshtat e shtuar nga kodi tjeter lexohen ne thirrjen pasardhese; nese tabela zvogelohet, bashkesia
    /// ndertohet nga e para.
    /// </summary>
    public sealed class IdetETabeles
    {
        private static readonly ConditionalWeakTable<DataTable, IdetETabeles> sipasTabeles = new ConditionalWeakTable<DataTable, IdetETabeles>();

        private readonly DataTable tabela;
        private readonly string kolona;
        private readonly HashSet<string> idet = new HashSet<string>(StringComparer.Ordinal);
        private int rreshtaTeLexuar;

        private IdetETabeles(DataTable tabela, string kolona)
        {
            this.tabela = tabela;
            this.kolona = kolona;
        }

        public static IdetETabeles Merr(DataTable tabela, string kolona)
        {
            IdetETabeles idet;
            if (!sipasTabeles.TryGetValue(tabela, out idet) || idet.kolona != kolona)
            {
                sipasTabeles.Remove(tabela);
                idet = new IdetETabeles(tabela, kolona);
                sipasTabeles.Add(tabela, idet);
            }
            idet.Sinkronizo();
            return idet;
        }

        /// <summary>
        /// Shton vleren nese nuk ekziston ne tabele dhe kthen true (thirresi shton rreshtin ne tabele menjehere pas saj).
        /// </summary>
        public bool Shto(object vlera)
        {
            if (!idet.Add(Celesi(vlera)))
                return false;
            rreshtaTeLexuar++;
            return true;
        }

        private void Sinkronizo()
        {
            if (tabela.Rows.Count < rreshtaTeLexuar)
            {
                idet.Clear();
                rreshtaTeLexuar = 0;
            }
            for (; rreshtaTeLexuar < tabela.Rows.Count; rreshtaTeLexuar++)
                idet.Add(Celesi(tabela.Rows[rreshtaTeLexuar][kolona]));
        }

        /// <summary>
        /// Si krahasimi i Select: numrat sipas vleres, tekstet pa dallim germash (DataTable.CaseSensitive = false) dhe pa
        /// hapesirat ne fund.
        /// </summary>
        private string Celesi(object vlera)
        {
            if (vlera is string s)
                return tabela.CaseSensitive ? s.TrimEnd() : s.TrimEnd().ToUpperInvariant();
            return Convert.ToString(vlera, CultureInfo.InvariantCulture);
        }
    }
}
