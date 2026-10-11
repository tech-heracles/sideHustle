using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbCore.DbInventari
{
    /// <summary>
    /// Kapja e ruajtjes se nje artikulli te ri te importit: kur eshte aktive ne thread, clsDatabaseInventari.ruajArt dhe
    /// ruajKodbar ndertojne parametrat si zakonisht, por i ruajne ketu ne vend qe t'i ekzekutojne. Keshtu artikulli
    /// kalon neper te njejtin kod (clsArtikulli.ruajArtikull) dhe me te njejtat vlera, dhe ruajtja behet me pas ne grup me
    /// prc_T_ARTIKULLI_ruajImport (shiko RuajtjeArtikujshNeGrup).
    /// </summary>
    public sealed class KapjaRuajtjes
    {
        [ThreadStatic] private static KapjaRuajtjes aktive;

        internal static KapjaRuajtjes Aktive => aktive;

        /// <summary>Parametrat e prc_T_ARTIKULLI_ins pervec @IDARTIKULLI (output), ne rradhen e tyre.</summary>
        internal List<SqlParameter> Artikulli { get; private set; }

        /// <summary>Kodbaret ne rradhen e ruajtjes: pershkrimi, njesia, detajim1, detajim2 (si ne prc_T_KODBARI_ins).</summary>
        internal List<object[]> Kodbare { get; } = new List<object[]>();

        internal IDisposable Fillo()
        {
            var paraardhesi = aktive;
            aktive = this;
            return new Mbyllja(() => aktive = paraardhesi);
        }

        internal void KapArtikullin(IList<IDbDataParameter> parametrat)
        {
            var lista = new List<SqlParameter>(parametrat.Count);
            foreach (IDbDataParameter p in parametrat)
            {
                if (p.Direction != ParameterDirection.Input)
                    continue;
                // kopje e plote e parametrit (tipi i nxjerre nga vlera mbetet i njejti)
                lista.Add((SqlParameter)((ICloneable)p).Clone());
            }
            Artikulli = lista;
        }

        internal void KapKodbarin(IList<IDbDataParameter> parametrat)
        {
            object Vlera(string emri)
            {
                foreach (IDbDataParameter p in parametrat)
                    if (string.Equals(p.ParameterName, emri, StringComparison.OrdinalIgnoreCase))
                        return p.Value;
                return DBNull.Value;
            }
            Kodbare.Add(new[] { Vlera("@PERSHKRIMI"), Vlera("@NJESIA"), Vlera("@DETAJIM1"), Vlera("@DETAJIM2") });
        }

        private sealed class Mbyllja : IDisposable
        {
            private Action veprimi;
            public Mbyllja(Action veprimi) { this.veprimi = veprimi; }
            public void Dispose() { veprimi?.Invoke(); veprimi = null; }
        }
    }
}
