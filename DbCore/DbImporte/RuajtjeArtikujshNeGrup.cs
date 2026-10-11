using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;
using DbCore.DbInventari;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbImporte
{
    /// <summary>
    /// Ruajtja ne grup e artikujve te rinj te importit. Artikujt qe ruajtja e te cilave shkruan vetem artikullin dhe
    /// kodbaret (clsArtikulli.MundTeRuhetNeGrup) kalojne neper ruajArtikull me kapje (parametrat ruhen) dhe ruhen me
    /// prc_T_ARTIKULLI_ruajImport (nje thirrje per artikull, ne transaksionin e vet si me pare).
    /// <para>Qe rezultati te jete i njejte me ruajtjen nje nga nje:</para>
    /// <list type="bullet">
    /// <item>para validimit te nje rreshti, nese kodi apo kodbari i tij mund te perputhet me nje artikull ne pritje,
    /// pritja ruhet me pare (validimi sheh databazen si me pare);</item>
    /// <item>para cdo ruajtjeje tjeter (modifikim, artikull qe nuk ruhet ne grup) pritja ruhet me pare (e njejta rradhe);</item>
    /// <item>gabimet e rreshtave qe vijne pas artikujve ne pritje mbahen edhe ato ne pritje dhe shtohen pas gabimeve te tyre,
    /// keshtu lista e gabimeve dhe rreshtat e paimportuar kane rradhen e skedarit.</item>
    /// </list>
    /// </summary>
    internal sealed class RuajtjeArtikujshNeGrup
    {
        private const int PritjaMaksimale = 200;

        private sealed class Hyrja
        {
            public KapjaRuajtjes Kapja;
            public clsArtikulli Artikulli;
            public DataRow Rreshti;
            public int I;
            public IDictionary<string, object> HfNrAuto;
            public string PrimaryKey, NdermarrjeKey;
            public clsMesazh Rezultati;
            // hyrje gabimesh: gabimet e nje rreshti qe erdhi pas artikujve ne pritje (shtohen pas tyre, ne rradhe)
            public List<object[]> Gabimet;
            public DataTable RreshtatPaImportuar;
            public int PaImportuaraPara;
        }

        private readonly List<Hyrja> pritja = new List<Hyrja>();
        private readonly HashSet<string> kodetNePritje = new HashSet<string>(Krahasuesi.Instanca);
        private readonly HashSet<string> kodbaretNePritje = new HashSet<string>(Krahasuesi.Instanca);
        private readonly DataTable gabime, tePaImportuara;
        private readonly int pozicionkodi;
        private Hyrja eFundit;

        private RuajtjeArtikujshNeGrup(DataTable gabime, DataTable tePaImportuara, int pozicionkodi)
        {
            this.gabime = gabime;
            this.tePaImportuara = tePaImportuara;
            this.pozicionkodi = pozicionkodi;
        }

        /// <summary>Kthen nje grup kur ruajtja ne grup mund te perdoret (importim i vertete, jo nga tabelat SQL, procedura ekziston).</summary>
        internal static RuajtjeArtikujshNeGrup Krijo(bool importo, bool vjenNgaImportSQL, DataTable gabime, DataTable tePaImportuara, int pozicionkodi)
        {
            if (!importo || vjenNgaImportSQL || !ProceduraEkziston())
                return null;
            return new RuajtjeArtikujshNeGrup(gabime, tePaImportuara, pozicionkodi);
        }

        private static bool ProceduraEkziston()
        {
            return ImportCache.Merr(ImportCache.Konfigurim, "prc_T_ARTIKULLI_ruajImport", () =>
            {
                using (var cn = new SqlConnection(ConnectionString()))
                using (var cmd = new SqlCommand("SELECT CASE WHEN EXISTS (SELECT 1 FROM sys.parameters WHERE object_id = OBJECT_ID(N'dbo.prc_T_ARTIKULLI_ruajImport', N'P') AND name = N'@KAKODBAR') THEN 1 ELSE 0 END", cn))
                {
                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                }
            }, _ => true);
        }

        /// <summary>Tipi i cdo parametri te prc_T_ARTIKULLI_ins (emri -> tipi, gjatesia, precision, scale).</summary>
        private static Dictionary<string, Tuple<string, int, byte, byte>> TipetEParametrave()
        {
            return ImportCache.Merr(ImportCache.Konfigurim, "prc_T_ARTIKULLI_ins|parametrat", () =>
            {
                var tipet = new Dictionary<string, Tuple<string, int, byte, byte>>(StringComparer.OrdinalIgnoreCase);
                using (var cn = new SqlConnection(ConnectionString()))
                using (var cmd = new SqlCommand("SELECT name, TYPE_NAME(system_type_id), max_length, precision, scale FROM sys.parameters WHERE object_id = OBJECT_ID(N'dbo.prc_T_ARTIKULLI_ins')", cn))
                {
                    cn.Open();
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            tipet[r.GetString(0)] = Tuple.Create(r.GetString(1), (int)r.GetInt16(2), r.GetByte(3), r.GetByte(4));
                }
                return tipet;
            }, t => t.Count > 0);
        }

        /// <summary>
        /// Parametrat e komandes deklarohen me tip te njejte ne cdo komande (perndryshe SqlClient i deklaron sipas vleres,
        /// p.sh. nvarchar(7), decimal(5,2), dhe cdo komande kompilohej me vete): tekstet si nvarchar(4000), pra vlera mberrin
        /// ne procedure si me pare; numrat me tipin e parametrit te procedures (te njejtin rrumbullakim, nje here).
        /// </summary>
        private static void VendosTipin(SqlParameter p, string emri, Dictionary<string, Tuple<string, int, byte, byte>> tipet)
        {
            if (!tipet.TryGetValue(emri, out var t))
                return;
            object v = p.Value;
            switch (t.Item1)
            {
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                    p.SqlDbType = SqlDbType.NVarChar;
                    p.Size = v is string s && s.Length > 4000 ? -1 : 4000;
                    break;
                case "decimal":
                case "numeric":
                    p.SqlDbType = SqlDbType.Decimal;
                    p.Precision = t.Item3;
                    p.Scale = t.Item4;
                    break;
                case "int":
                    p.SqlDbType = SqlDbType.Int;
                    break;
                case "bit":
                    p.SqlDbType = SqlDbType.Bit;
                    break;
                case "tinyint":
                    p.SqlDbType = SqlDbType.TinyInt;
                    break;
                case "datetime":
                    p.SqlDbType = SqlDbType.DateTime;
                    break;
            }
            p.Value = v;
        }

        private static string ConnectionString()
        {
            var cs = new DbManager(DataProviderType.SqlServer, MyConnectionsManager.GetSelectedConNameServer()).ConnectionString;
            // si ruajtja nje nga nje (MyTransactionScope RequiresNew): pa u lidhur me ndonje transaksion te jashtem
            return new SqlConnectionStringBuilder(cs) { Enlist = false }.ConnectionString;
        }

        internal bool KaPritje => pritja.Count > 0;

        /// <summary>Thirret ne fillim te cdo rreshti: rreshti i fundit ne pritje ka rendesi vetem nese eshte rreshti i fundit i skedarit.</summary>
        internal void FilloRreshtin() => eFundit = null;

        /// <summary>True kur kodi apo nje nga kodbaret mund te jene te njejte (sipas krahasimit te SQL) me nje artikull ne pritje.</summary>
        internal bool Perplaset(string kodi, IEnumerable<string> kodbaret)
        {
            if (pritja.Count == 0)
                return false;
            if (!string.IsNullOrEmpty(kodi) && kodetNePritje.Contains(kodi))
                return true;
            if (kodbaret != null)
                foreach (var k in kodbaret)
                    if (!string.IsNullOrEmpty(k) && kodbaretNePritje.Contains(k))
                        return true;
            return false;
        }

        /// <summary>
        /// Provon ta vendose artikullin ne pritje. Kthen false kur artikulli duhet ruajtur me rrugen e zakonshme
        /// (atehere pritja eshte ruajtur me pare, qe rradha e shkrimeve te mbetet e njejte).
        /// </summary>
        internal bool Shto(clsArtikulli art, IDictionary<string, object> hfNrAuto, DataRow dr, int i, string primaryKey, string ndermarrjeKey)
        {
            var template = new clsArtikullPerberesTemplateKoka();
            var cmime = new colCmimeArtikujsh();
            if (art.MundTeRuhetNeGrup(hfNrAuto, template, cmime))
            {
                var kapja = new KapjaRuajtjes();
                clsMesazh m = art.KapRuajtjenNeGrup(kapja, cmime, template);
                // procedura merr vetem nje kodbar (parametra te thjeshte); artikujt me me shume ruhen me rrugen e zakonshme
                if (m.Status && kapja.Artikulli != null && kapja.Kodbare.Count <= 1)
                {
                    var hyrja = new Hyrja { Kapja = kapja, Artikulli = art, Rreshti = dr, I = i, HfNrAuto = hfNrAuto, PrimaryKey = primaryKey, NdermarrjeKey = ndermarrjeKey };
                    pritja.Add(hyrja);
                    eFundit = hyrja;
                    kodetNePritje.Add(art.KodArtikulli ?? "");
                    foreach (var k in kapja.Kodbare)
                        kodbaretNePritje.Add(Convert.ToString(k[0], CultureInfo.InvariantCulture));
                    if (pritja.Count >= PritjaMaksimale)
                        Zbraz();
                    return true;
                }
            }
            Zbraz();
            return false;
        }

        /// <summary>
        /// Pas perpunimit te nje rreshti qe nuk u vu ne pritje: nese ai shtoi gabime ndersa kishte artikuj ne pritje,
        /// gabimet e tij hiqen nga tabelat dhe vihen ne pritje pas artikujve (Zbraz i shton ne rradhen e rreshtave).
        /// </summary>
        internal void PasRreshtit(int gabimePara, int paImportuaraPara)
        {
            if (pritja.Count == 0 || (gabime.Rows.Count == gabimePara && tePaImportuara.Rows.Count == paImportuaraPara))
                return;
            var hyrja = new Hyrja { Gabimet = new List<object[]>(), RreshtatPaImportuar = tePaImportuara.Clone(), PaImportuaraPara = paImportuaraPara };
            for (int k = gabimePara; k < gabime.Rows.Count; k++)
                hyrja.Gabimet.Add(gabime.Rows[k].ItemArray);
            for (int k = paImportuaraPara; k < tePaImportuara.Rows.Count; k++)
                hyrja.RreshtatPaImportuar.ImportRow(tePaImportuara.Rows[k]);
            while (gabime.Rows.Count > gabimePara)
                gabime.Rows.RemoveAt(gabime.Rows.Count - 1);
            while (tePaImportuara.Rows.Count > paImportuaraPara)
                tePaImportuara.Rows.RemoveAt(tePaImportuara.Rows.Count - 1);
            pritja.Add(hyrja);
        }

        /// <summary>Ruan artikujt ne pritje; gabimet shtohen ne rradhen e rreshtave, si ne ImportoArtikujShtim.</summary>
        internal void Zbraz()
        {
            if (pritja.Count == 0)
                return;
            var hyrjet = pritja.ToArray();
            pritja.Clear();
            kodetNePritje.Clear();
            kodbaretNePritje.Clear();

            var artikujt = Array.FindAll(hyrjet, h => h.Kapja != null);
            if (artikujt.Length > 0)
            {
                RuajNeDatabaze(artikujt);
                ImportCache.Shkruar(ImportCache.ArtikullKod);
                ImportCache.Shkruar(ImportCache.Kodbar);
            }

            foreach (var h in hyrjet)
            {
                if (h.Kapja == null)
                {
                    // gabimet e nje rreshti te mevonshem: kolona e trete mban numrin e rreshtit te paimportuar
                    int zhvendosja = tePaImportuara.Rows.Count - h.PaImportuaraPara;
                    foreach (DataRow r in h.RreshtatPaImportuar.Rows)
                        tePaImportuara.ImportRow(r);
                    foreach (var vlerat in h.Gabimet)
                    {
                        if (zhvendosja != 0 && int.TryParse(Convert.ToString(vlerat[2], CultureInfo.InvariantCulture), out int nr) && nr > h.PaImportuaraPara)
                            vlerat[2] = nr + zhvendosja;
                        gabime.Rows.Add(vlerat);
                    }
                    continue;
                }
                if (h.Rezultati == null)   // gabim i papritur ne procedure: ruajtja e zakonshme jep mesazhin e sakte
                    h.Rezultati = h.Artikulli.ruaj(h.HfNrAuto, new colCmimeArtikujsh(), new clsArtikullPerberesTemplateKoka(), false, "0", "", h.PrimaryKey, h.NdermarrjeKey);
                if (!h.Rezultati.Status)
                {
                    object[] arr = { h.Rreshti[pozicionkodi], h.Rezultati.PershkrimMesazhi, h.I };
                    gabime.Rows.Add(arr);
                    tePaImportuara.ImportRow(h.Rreshti);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                }
            }
        }

        /// <summary>Mesazhi i rreshtit te fundit kur ai ishte ne pritje (pas Zbraz), perndryshe null.</summary>
        internal clsMesazh MesazhiIFundit() => eFundit?.Rezultati;

        private static void RuajNeDatabaze(Hyrja[] hyrjet)
        {
            // nje thirrje RPC per artikull ne te njejtin lidhje: procedura kompilohet nje here (nje batch me shume EXEC
            // kompilohej per cdo madhesi dhe zinte shume memorie ne cache), dhe ne vend te ~6 thirrjeve per artikull behet nje
            var tipet = TipetEParametrave();
            using (var cn = new SqlConnection(ConnectionString()))
            {
                cn.Open();
                foreach (var h in hyrjet)
                {
                    var kapja = h.Kapja;
                    using (var cmd = new SqlCommand("dbo.prc_T_ARTIKULLI_ruajImport", cn) { CommandType = CommandType.StoredProcedure, CommandTimeout = 600 })
                    {
                        foreach (SqlParameter p0 in kapja.Artikulli)
                        {
                            var p = (SqlParameter)((ICloneable)p0).Clone();
                            if (!p.ParameterName.StartsWith("@"))
                                p.ParameterName = "@" + p.ParameterName;
                            VendosTipin(p, p.ParameterName, tipet);
                            cmd.Parameters.Add(p);
                        }
                        cmd.Parameters.Add(new SqlParameter("@IDARTIKULLI", SqlDbType.Decimal) { Precision = 18, Scale = 0, Direction = ParameterDirection.Output });

                        var pGabimi = cmd.Parameters.Add(new SqlParameter("@GABIMI", SqlDbType.Int) { Direction = ParameterDirection.Output });
                        try
                        {
                            object[] kb = kapja.Kodbare.Count == 1 ? kapja.Kodbare[0] : null;
                            cmd.Parameters.Add(new SqlParameter("@KAKODBAR", SqlDbType.Bit) { Value = kb != null });
                            cmd.Parameters.Add(new SqlParameter("@KODBARI", SqlDbType.NVarChar, 4000) { Value = Vlera(kb, 0, v => Convert.ToString(v, CultureInfo.InvariantCulture)) });
                            cmd.Parameters.Add(new SqlParameter("@KODBARINJESIA", SqlDbType.Int) { Value = Vlera(kb, 1, v => Convert.ToInt32(v, CultureInfo.InvariantCulture)) });
                            cmd.Parameters.Add(new SqlParameter("@KODBARIDETAJIM1", SqlDbType.Decimal) { Precision = 18, Scale = 0, Value = Vlera(kb, 2, v => Convert.ToDecimal(v, CultureInfo.InvariantCulture)) });
                            cmd.Parameters.Add(new SqlParameter("@KODBARIDETAJIM2", SqlDbType.Decimal) { Precision = 18, Scale = 0, Value = Vlera(kb, 3, v => Convert.ToDecimal(v, CultureInfo.InvariantCulture)) });
                            cmd.Parameters.Add(new SqlParameter("@KODBARIGABIM", SqlDbType.NVarChar, 4000) { Direction = ParameterDirection.Output });
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex) when (!(ex is SqlException))
                        {
                            // vlere qe SqlClient nuk e konverton (para dergimit, asgje nuk eshte ekzekutuar):
                            // artikulli ruhet me rrugen e zakonshme, qe jep edhe mesazhin e sakte
                            h.Rezultati = null;
                            continue;
                        }

                        int gabimi = pGabimi.Value == DBNull.Value ? 3 : Convert.ToInt32(pGabimi.Value);
                        switch (gabimi)
                        {
                            case 0:
                                h.Rezultati = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                                break;
                            case 1:   // si kontrolloArtikull ne ruaj
                                h.Rezultati = new clsMesazh(false, "Ekziston nje artikull me kete kod!");
                                break;
                            case 2:   // si ruajArtikull
                                h.Rezultati = new clsMesazh(false, String.Format("Barkodi {0} ekziston!", kapja.Kodbare[0][0]));
                                break;
                            default:
                                h.Rezultati = null;   // ruhet me rrugen e zakonshme
                                break;
                        }
                    }
                }
            }
        }

        private static object Vlera(object[] kodbari, int i, Func<object, object> konverto)
        {
            return kodbari == null || kodbari[i] == null || kodbari[i] == DBNull.Value ? DBNull.Value : konverto(kodbari[i]);
        }

        /// <summary>
        /// Krahasim me i gjere se ai i kolonave te SQL-it (pa dallim germash, theksash, gjeresie, simbolesh dhe hapesirash ne
        /// fund): nese SQL-i i quan dy kode te njejte, edhe ky; e kunderta vetem shkakton nje ruajtje te parakohshme te pritjes.
        /// </summary>
        private sealed class Krahasuesi : IEqualityComparer<string>
        {
            internal static readonly Krahasuesi Instanca = new Krahasuesi();
            private const CompareOptions Opsionet = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth
                                                    | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols;
            private static readonly CompareInfo Info = CultureInfo.InvariantCulture.CompareInfo;
            public bool Equals(string x, string y) => Info.Compare((x ?? "").TrimEnd(), (y ?? "").TrimEnd(), Opsionet) == 0;
            public int GetHashCode(string s) => Info.GetHashCode((s ?? "").TrimEnd(), Opsionet);
        }
    }
}
