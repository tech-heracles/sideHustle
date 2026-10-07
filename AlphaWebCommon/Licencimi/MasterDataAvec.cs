using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace DbCore.IMBUtils.Licencimi
{
    /// <summary>
    /// Dergon te dhenat baze (artikuj, kliente, magazina, kategori, grupe) te kompanive AVEC ne Firebase
    /// (funksioni avecMasterData), qe POS-i dhe Manager t'i lexojne si ato te Financa5.
    /// <para>Cdo 5 minuta, per cdo kompani me licence te vlefshme: hapet databaza e saj (lidhja nga licencat) dhe
    /// merret ndermarrja: kodi i vendosur ne Manager > Data Source, ose ndermarrja ku hyjne perdoruesit e kesaj
    /// kompanie (ruhet ne hyrje, App_Data/avec-ndermarrjet.json). Pa asnjeren nuk dergohet asgje. Artikujt dhe klientet
    /// dergohen sipas ndryshimeve (DTMODIFIKIMI) qe nga sinkronizimi i fundit, dhe te plote nje here ne dite;
    /// magazinat, kategorite dhe grupet (te pakta) te plota cdo here. Ne nje dergim te plote, dokumentet qe nuk
    /// ekzistojne me ne AVEC fshihen ne Firebase.</para>
    /// <para>Rregullat e te dhenave: cmimi baze = niveli i cmimit me kod 1; SalesPrices = te gjithe nivelet; cmimet
    /// ne AVEC jane pa TVSH, prandaj u shtohet TVSH-ja e artikullit. Kategoria = kodifikimi 1, grupi = kodifikimi 2.</para>
    /// </summary>
    public static class MasterDataAvec
    {
        private static readonly TimeSpan Intervali = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan PlotCdo = TimeSpan.FromHours(24);
        private static readonly TimeSpan Mbivendosje = TimeSpan.FromMinutes(5);   // ndryshimet afer kufirit dergohen dy here
        private const int Copa = 1000;
        private static readonly HttpClient klienti = new HttpClient { Timeout = TimeSpan.FromSeconds(120) };
        private static readonly Logger log = LogManager.GetLogger("MasterDataAvec");
        private static readonly Dictionary<string, DateTime> paralajmeruar = new Dictionary<string, DateTime>();
        private static Timer kohematesi;
        private static int neEkzekutim;

        public static void Fillo()
        {
            if (kohematesi == null)
                kohematesi = new Timer(_ => Ekzekuto(), null, TimeSpan.FromMinutes(1), Intervali);
        }

        public static void Ndalo()
        {
            kohematesi?.Dispose();
            kohematesi = null;
        }

        /// <summary>Nje kalim per te gjitha kompanite (nuk nis nese nje tjeter eshte ende ne pune).</summary>
        public static void Ekzekuto()
        {
            if (Interlocked.Exchange(ref neEkzekutim, 1) == 1)
                return;
            try
            {
                IReadOnlyList<LicencaAvec> licencat;
                try
                {
                    licencat = LicencatAvec.Merr();
                }
                catch (Exception ex)
                {
                    log.Warn(ex, "Te dhenat baze nuk u derguan: lista e licencave nuk u mor");
                    return;
                }
                foreach (LicencaAvec licenca in licencat.Where(l => l.Kontrollo(DateTime.Now) == null))
                {
                    try
                    {
                        Sinkronizo(licenca);
                    }
                    catch (Exception ex)
                    {
                        Paralajmero(licenca.Id + "|gabim", ex, $"Te dhenat baze te {licenca.Emri} nuk u derguan");
                    }
                }
            }
            finally
            {
                Interlocked.Exchange(ref neEkzekutim, 0);
            }
        }

        private static void Sinkronizo(LicencaAvec licenca)
        {
            var lidhja = new SqlConnectionStringBuilder(licenca.ConnectionString) { ConnectTimeout = 10 };
            using (var db = new SqlConnection(lidhja.ConnectionString))
            {
                try
                {
                    db.Open();
                }
                catch (SqlException)
                {
                    // databaza e kesaj kompanie nuk eshte ne kete server (lista e licencave eshte e perbashket per te gjithe)
                    return;
                }

                // ndermarrja: kodi i vendosur ne Manager > Data Source; ndryshe ajo ku hyne perdoruesi i fundit
                decimal idNderm;
                if (!string.IsNullOrWhiteSpace(licenca.KodiNdermarrjes))
                {
                    var nderm = Lexo(db, "SELECT IDNDERMARJE FROM T_NDERMARJE WHERE IDSTATUSDOK <> 2 AND LTRIM(RTRIM(NDERMARJEKODI)) = @kodi",
                        null, new SqlParameter("@kodi", licenca.KodiNdermarrjes.Trim()));
                    if (nderm.Count != 1)
                    {
                        Paralajmero(licenca.Id + "|kodi", null, $"Ne databazen e {licenca.Emri} {(nderm.Count == 0 ? "nuk ka" : "ka " + nderm.Count)} ndermarrje me kodin \"{licenca.KodiNdermarrjes}\" (Manager > Data Source): te dhenat baze nuk dergohen.");
                        return;
                    }
                    idNderm = Convert.ToDecimal(nderm[0]["IDNDERMARJE"]);
                }
                else
                {
                    int? eHyrjes = NdermarrjaEHyrjes(licenca.Id);
                    if (eHyrjes == null)
                        return;   // askush s'ka hyre ende ne kete kompani dhe Manager s'ka kod ndermarrjeje
                    idNderm = eHyrjes.Value;
                }
                DateTime tani = (DateTime)Lexo(db, "SELECT GETDATE() AS t", null)[0]["t"];   // ora e serverit SQL (= DTMODIFIKIMI)

                JObject gjendja = Dergo(licenca.Id, new JObject { ["action"] = "state" });
                string kufiri = Iso(tani);

                // te pakta: te plota cdo here (pa gjurmim ndryshimesh)
                DergoTePlota(licenca.Id, "LOCATION", Lexo(db, SqlMagazinat, null, P(idNderm)), "code", kufiri);
                DergoTePlota(licenca.Id, "ITEM_GROUP", Lexo(db, SqlGrupet, null, P(idNderm)), "code", kufiri);
                var kategorite = Lexo(db, SqlKategorite, null, P(idNderm));
                foreach (var k in kategorite)
                    k["groupCodes"] = k["groupCodes"] is string s ? JArray.Parse(s) : new JArray();
                DergoTePlota(licenca.Id, "ITEM_CATEGORY", kategorite, "code", kufiri);

                // artikuj dhe kliente: sipas ndryshimeve; te plote kur s'ka gjurme ose cdo 24 ore
                DergoNdryshimet(db, licenca.Id, "ITEM", SqlArtikujt, "Code", idNderm, gjendja, tani, kufiri);
                DergoNdryshimet(db, licenca.Id, "CUSTOMER", SqlKlientet, "code", idNderm, gjendja, tani, kufiri);
            }
        }

        private static void DergoTePlota(string idKompanie, string entiteti, List<Dictionary<string, object>> rreshtat, string fushaKodi, string kufiri)
        {
            string run = Guid.NewGuid().ToString("N");
            foreach (var copa in Copeza(rreshtat))
                Dergo(idKompanie, Upsert(entiteti, run, copa, fushaKodi, new List<string>()));
            Dergo(idKompanie, new JObject { ["action"] = "finish", ["entity"] = entiteti, ["runId"] = run, ["full"] = true, ["watermark"] = kufiri });
        }

        private static void DergoNdryshimet(SqlConnection db, string idKompanie, string entiteti, string sql, string fushaKodi,
            decimal idNderm, JObject gjendja, DateTime tani, string kufiri)
        {
            JToken meta = gjendja["entities"]?[entiteti];
            DateTime? gjurma = Date(meta?["watermark"]), plotiFundit = Date(meta?["lastFullSync"]);
            bool plote = gjurma == null || plotiFundit == null || DateTime.UtcNow - plotiFundit.Value.ToUniversalTime() > PlotCdo;
            object nga = plote ? (object)DBNull.Value : gjurma.Value - Mbivendosje;

            var rreshtat = Lexo(db, sql, 120, P(idNderm), new SqlParameter("@since", SqlDbType.DateTime) { Value = nga });
            var tefshire = rreshtat.Where(r => true.Equals(r["Deleted"])).Select(r => Convert.ToString(r[fushaKodi])).ToList();
            var tegjalle = rreshtat.Where(r => !true.Equals(r["Deleted"])).ToList();
            string run = plote ? Guid.NewGuid().ToString("N") : null;

            // ne dergimin e plote te fshiret thjesht nuk dergohen (finish i heq); ne ate me ndryshime fshihen me emer
            var fshirjet = plote ? new List<string>() : tefshire;
            foreach (var copa in Copeza(tegjalle))
                Dergo(idKompanie, Upsert(entiteti, run, copa, fushaKodi, new List<string>()));
            for (int i = 0; i < fshirjet.Count; i += Copa)
                Dergo(idKompanie, Upsert(entiteti, run, new List<Dictionary<string, object>>(), fushaKodi, fshirjet.Skip(i).Take(Copa).ToList()));
            Dergo(idKompanie, new JObject { ["action"] = "finish", ["entity"] = entiteti, ["runId"] = run, ["full"] = plote, ["watermark"] = kufiri });
            if (plote || rreshtat.Count > 0)
                log.Info($"{idKompanie} {entiteti}: {(plote ? "i plote" : "ndryshime")}, {tegjalle.Count} dokumente, {fshirjet.Count} te fshira");
        }

        // ===== Ndermarrja ku hyjne perdoruesit, per kompani (licence): App_Data/avec-ndermarrjet.json =====

        private static readonly object bllokimiSkedarit = new object();

        private static string SkedariHyrjeve =>
            System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/avec-ndermarrjet.json")
            ?? System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "avec-ndermarrjet.json");

        /// <summary>
        /// Thirret kur nje perdorues zgjedh ndermarrjen ne hyrje: ruan per kompanine (licencen) e lidhjes se zgjedhur
        /// ndermarrjen ku punohet, qe te dhenat baze te dergohen per te kur Manager nuk ka kod ndermarrjeje.
        /// </summary>
        public static void RuajNdermarrjenEHyrjes(string emriLidhjes, int idNdermarrje)
        {
            try
            {
                LicencaAvec licenca = LicencatAvec.GjejSipasLidhjes(emriLidhjes);
                if (licenca == null)
                    return;
                lock (bllokimiSkedarit)
                {
                    var hyrjet = LexoHyrjet();
                    if (hyrjet.TryGetValue(licenca.Id, out int ekzistuese) && ekzistuese == idNdermarrje)
                        return;
                    hyrjet[licenca.Id] = idNdermarrje;
                    string skedari = SkedariHyrjeve;
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(skedari));
                    System.IO.File.WriteAllText(skedari, JsonConvert.SerializeObject(hyrjet, Formatting.Indented));
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex, "Ndermarrja e hyrjes nuk u ruajt");
            }
        }

        private static int? NdermarrjaEHyrjes(string idLicence)
        {
            lock (bllokimiSkedarit)
                return LexoHyrjet().TryGetValue(idLicence, out int id) ? id : (int?)null;
        }

        private static Dictionary<string, int> LexoHyrjet()
        {
            try
            {
                string skedari = SkedariHyrjeve;
                if (System.IO.File.Exists(skedari))
                    return JsonConvert.DeserializeObject<Dictionary<string, int>>(System.IO.File.ReadAllText(skedari)) ?? new Dictionary<string, int>();
            }
            catch (Exception ex)
            {
                log.Warn(ex, "avec-ndermarrjet.json nuk u lexua");
            }
            return new Dictionary<string, int>();
        }

        private static JObject Upsert(string entiteti, string run, List<Dictionary<string, object>> rreshtat, string fushaKodi, List<string> fshirjet)
        {
            var docs = new JArray();
            foreach (var r in rreshtat)
            {
                var te_dhenat = new JObject();
                foreach (var kv in r.Where(kv => kv.Key != "Deleted"))
                    te_dhenat[kv.Key] = kv.Value == null ? JValue.CreateNull()
                        : kv.Value is DateTime d ? new JValue(Iso(d))
                        : JToken.FromObject(kv.Value);
                if (entiteti == "ITEM")
                    te_dhenat["originalCode"] = te_dhenat["Code"];
                docs.Add(new JObject { ["id"] = Convert.ToString(r[fushaKodi]), ["data"] = te_dhenat });
            }
            return new JObject
            {
                ["action"] = "upsert",
                ["entity"] = entiteti,
                ["runId"] = run,
                ["docs"] = docs,
                ["deletes"] = new JArray(fshirjet),
            };
        }

        private static JObject Dergo(string idKompanie, JObject trupi)
        {
            string url = WebConfigurationManager.AppSettings["AvecMasterDataUrl"];
            if (string.IsNullOrWhiteSpace(url))
                url = (WebConfigurationManager.AppSettings["AvecLicenseUrl"] ?? "").Replace("avecLicenses", "avecMasterData");
            trupi["installationId"] = WebConfigurationManager.AppSettings["AvecInstallationId"];
            trupi["companyId"] = idKompanie;
            using (var kerkesa = new HttpRequestMessage(HttpMethod.Post, url))
            {
                kerkesa.Headers.Add("X-Avec-Key", WebConfigurationManager.AppSettings["AvecInstallationKey"]);
                kerkesa.Content = new StringContent(trupi.ToString(Formatting.None), Encoding.UTF8, "application/json");
                using (HttpResponseMessage pergjigja = klienti.SendAsync(kerkesa).GetAwaiter().GetResult())
                {
                    string teksti = pergjigja.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!pergjigja.IsSuccessStatusCode)
                        throw new InvalidOperationException($"avecMasterData {trupi["action"]} {trupi["entity"]} ktheu {(int)pergjigja.StatusCode}: {teksti}");
                    return string.IsNullOrEmpty(teksti) ? new JObject() : JObject.Parse(teksti);
                }
            }
        }

        private static List<Dictionary<string, object>> Lexo(SqlConnection db, string sql, int? afati, params SqlParameter[] parametrat)
        {
            var rreshtat = new List<Dictionary<string, object>>();
            using (var cmd = new SqlCommand(sql, db) { CommandTimeout = afati ?? 30 })
            {
                cmd.Parameters.AddRange(parametrat);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    do
                    {
                        if (r.FieldCount == 0)
                            continue;
                        while (r.Read())
                        {
                            var rresht = new Dictionary<string, object>(r.FieldCount);
                            for (int i = 0; i < r.FieldCount; i++)
                                rresht[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
                            rreshtat.Add(rresht);
                        }
                    } while (r.NextResult());
                }
            }
            return rreshtat;
        }

        private static SqlParameter P(decimal idNderm) => new SqlParameter("@nd", SqlDbType.Decimal) { Precision = 18, Scale = 0, Value = idNderm };

        private static IEnumerable<List<Dictionary<string, object>>> Copeza(List<Dictionary<string, object>> rreshtat)
        {
            for (int i = 0; i < rreshtat.Count; i += Copa)
                yield return rreshtat.Skip(i).Take(Copa).ToList();
        }

        // datat e databazes jane ne oren lokale te serverit (IIS dhe SQL ne te njejten makine): dergohen me zonen
        private static string Iso(DateTime d) =>
            new DateTimeOffset(DateTime.SpecifyKind(d, DateTimeKind.Local)).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture);

        // gjurma kthehet si u dergua (me zone) -> ore lokale e serverit, si DTMODIFIKIMI
        private static DateTime? Date(JToken t)
        {
            if (t == null || t.Type == JTokenType.Null)
                return null;
            if (t.Type == JTokenType.Date)
                return t.Value<DateTime>().ToLocalTime();
            return DateTimeOffset.TryParse(t.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset d)
                ? d.LocalDateTime : (DateTime?)null;
        }

        // nje paralajmerim i njejte shkruhet ne log nje here ne 6 ore (kohematesi punon cdo 5 minuta)
        private static void Paralajmero(string celesi, Exception ex, string mesazhi)
        {
            lock (paralajmeruar)
            {
                if (paralajmeruar.TryGetValue(celesi, out DateTime kur) && DateTime.UtcNow - kur < TimeSpan.FromHours(6))
                    return;
                paralajmeruar[celesi] = DateTime.UtcNow;
            }
            if (ex == null) log.Warn(mesazhi);
            else log.Error(ex, mesazhi);
        }

        // ===== Pyetjet (@nd = IDNDERMARJE, @since = NULL per te gjitha) =====

        // Artikujt e ndryshuar (ose me cmime te ndryshuara) qe nga @since; per secilin kod merret rreshti me i mire
        // i kompanise (aktiv para te fshirit), qe nje artikull i fshire me kod te ripërdorur te mos fshije kodin e gjalle.
        private const string SqlArtikujt = @"
SET NOCOUNT ON;
DECLARE @tani datetime = GETDATE();
CREATE TABLE #kode (Code nvarchar(100) PRIMARY KEY);
INSERT INTO #kode
SELECT DISTINCT UPPER(LTRIM(RTRIM(a.KODARTIKULLI))) FROM T_ARTIKULLI a
WHERE a.IDNDERMARJE = @nd AND LTRIM(RTRIM(ISNULL(a.KODARTIKULLI, ''))) <> ''
  AND (@since IS NULL OR ISNULL(a.DTMODIFIKIMI, a.DTKRIJIMI) >= @since
       OR EXISTS (SELECT 1 FROM T_CMIMARTIKULLI c WHERE c.IDARTIKULLI = a.IDARTIKULLI AND ISNULL(c.DTMODIFIKIMI, c.DTKRIJIMI) >= @since));

SELECT a.IDARTIKULLI, Deleted = CAST(CASE WHEN a.IDSTATUSDOK = 2 THEN 1 ELSE 0 END AS bit),
       rn = ROW_NUMBER() OVER (PARTITION BY k.Code ORDER BY CASE WHEN a.IDSTATUSDOK = 2 THEN 1 ELSE 0 END, a.IDARTIKULLI DESC)
INTO #zgj
FROM #kode k JOIN T_ARTIKULLI a ON a.IDNDERMARJE = @nd AND UPPER(LTRIM(RTRIM(a.KODARTIKULLI))) = k.Code;
DELETE FROM #zgj WHERE rn > 1;
CREATE CLUSTERED INDEX IX_zgj ON #zgj (IDARTIKULLI);

SELECT c.IDARTIKULLI, n.KODNIVELCMIMI,
       Bruto = CAST(ROUND(c.CMIMI * (1 + ISNULL(tx.NORMAPERQINDJE, 0) / 100), 2) AS decimal(18,2)),
       rn = ROW_NUMBER() OVER (PARTITION BY c.IDARTIKULLI, c.IDNIVELCMIMI ORDER BY c.DTFILLIMIT DESC, c.IDCMIMARTIKULLI DESC)
INTO #cm
FROM #zgj z
JOIN T_CMIMARTIKULLI c ON c.IDARTIKULLI = z.IDARTIKULLI
JOIN T_NIVELCMIMI n ON n.IDNIVELCMIMI = c.IDNIVELCMIMI AND n.IDSTATUSDOK <> 2
JOIN T_ARTIKULLI a ON a.IDARTIKULLI = c.IDARTIKULLI
LEFT JOIN T_TAKSAT tx ON tx.IDTAKSA = a.IDTVSH
WHERE z.Deleted = 0 AND c.IDSTATUSDOK <> 2 AND c.CMIMI IS NOT NULL
  AND (c.DTFILLIMIT IS NULL OR c.DTFILLIMIT <= @tani) AND (c.DTMBARIMIT IS NULL OR c.DTMBARIMIT >= @tani)
  AND (c.IDNJESIA IS NULL OR c.IDNJESIA = a.NJESI1ARTIKULLI) AND ISNULL(c.SASIMIN, 0) <= 1 AND c.IdDetajim IS NULL;
DELETE FROM #cm WHERE rn > 1;
CREATE CLUSTERED INDEX IX_cm ON #cm (IDARTIKULLI, KODNIVELCMIMI);

SELECT
    Deleted         = z.Deleted,
    Code            = UPPER(LTRIM(RTRIM(a.KODARTIKULLI))),
    Description     = ISNULL(a.PERSHKRIMARTIKULLI, ''),
    LongDescription = ISNULL(a.PERSHKRIMARTIKULLI, ''),
    BaseUOM         = ISNULL(nj.KODNJESIA, ''),
    VAT             = CAST(ISNULL(tx.NORMAPERQINDJE, 0) AS decimal(9,4)),
    CategoryCode    = ISNULL(NULLIF(LTRIM(RTRIM(k1.KODKODIFIKIMI)), ''), 'PA KATEGORI'),
    GroupCode       = ISNULL(NULLIF(LTRIM(RTRIM(k2.KODKODIFIKIMI)), ''), 'PA GRUPIM'),
    BasePrice       = (SELECT p.Bruto FROM #cm p WHERE p.IDARTIKULLI = a.IDARTIKULLI AND p.KODNIVELCMIMI = '1'),
    ItemType        = CASE a.KLASA WHEN 3 THEN N'Sherbim' ELSE N'Inventar' END,
    ExemptFromVAT   = CASE WHEN tx.EPERJASHTUAR = 1 THEN ISNULL(tx.TIPIIPERJASHTIMIT, N'E') ELSE N'' END,
    Active          = CAST(ISNULL(a.AKTIV, 1) AS bit),
    ModifiedDate    = ISNULL(a.DTMODIFIKIMI, a.DTKRIJIMI),
    BaseBarcode     = (SELECT TOP 1 b.PERSHKRIMI FROM T_KODBARI b WHERE b.IDARTIKULLI = a.IDARTIKULLI ORDER BY b.IDKODBARI),
    Barcodes        = (SELECT '[' + STRING_AGG('""' + STRING_ESCAPE(UPPER(LTRIM(RTRIM(b.PERSHKRIMI))), 'json') + '""', ',') + ']'
                       FROM T_KODBARI b WHERE b.IDARTIKULLI = a.IDARTIKULLI AND LTRIM(RTRIM(ISNULL(b.PERSHKRIMI, ''))) <> ''),
    SalesPrices     = (SELECT '[' + STRING_AGG('{""priceLevel"":""' + STRING_ESCAPE(p.KODNIVELCMIMI, 'json') + '"",""value"":'
                                + CAST(p.Bruto AS nvarchar(40)) + '}', ',') WITHIN GROUP (ORDER BY p.KODNIVELCMIMI) + ']'
                       FROM #cm p WHERE p.IDARTIKULLI = a.IDARTIKULLI AND p.Bruto > 0)
FROM #zgj z
JOIN T_ARTIKULLI a ON a.IDARTIKULLI = z.IDARTIKULLI
LEFT JOIN T_NJESIARTIKULLI nj ON nj.IDNJESIA = a.NJESI1ARTIKULLI
LEFT JOIN T_TAKSAT tx ON tx.IDTAKSA = a.IDTVSH
LEFT JOIN T_KODIFIKIMARTIKULLI k1 ON k1.IDKODIFIKIMI = a.KODIFIKIMI1ARTIKULLI
LEFT JOIN T_KODIFIKIMARTIKULLI k2 ON k2.IDKODIFIKIMI = a.KODIFIKIMI2ARTIKULLI;";

        // Klientet (LLOJIKF = 1); joaktiv, i fshire ose i kthyer ne furnitor = i fshire ne Firebase
        private const string SqlKlientet = @"
SET NOCOUNT ON;
CREATE TABLE #kode (code nvarchar(100) PRIMARY KEY);
INSERT INTO #kode
SELECT DISTINCT UPPER(LTRIM(RTRIM(k.KODKLIENTFURNITOR))) FROM T_KLIENTFURNITOR k
WHERE k.IDNDERMARJE = @nd AND LTRIM(RTRIM(ISNULL(k.KODKLIENTFURNITOR, ''))) <> ''
  AND (@since IS NULL OR ISNULL(k.DTMODIFIKIMI, k.DTKRIJIMI) >= @since);

SELECT Deleted, code, description, nipt, priceLevel, modifiedDate FROM (
    SELECT Deleted = CAST(CASE WHEN k.IDSTATUSDOK = 2 OR ISNULL(k.AKTIVKF, 1) = 0 OR k.LLOJIKF <> 1 THEN 1 ELSE 0 END AS bit),
           code = x.code,
           description = REPLACE(REPLACE(ISNULL(k.EMERTIMIKF, ''), ';', ''), '|', ''),
           nipt = UPPER(LTRIM(RTRIM(ISNULL(k.NIPTKF, '')))),
           priceLevel = ISNULL(n.KODNIVELCMIMI, ''),
           modifiedDate = ISNULL(k.DTMODIFIKIMI, k.DTKRIJIMI),
           rn = ROW_NUMBER() OVER (PARTITION BY x.code ORDER BY
                    CASE WHEN k.IDSTATUSDOK = 2 OR ISNULL(k.AKTIVKF, 1) = 0 OR k.LLOJIKF <> 1 THEN 1 ELSE 0 END, k.IDKLIENTFURNITOR DESC)
    FROM #kode x
    JOIN T_KLIENTFURNITOR k ON k.IDNDERMARJE = @nd AND UPPER(LTRIM(RTRIM(k.KODKLIENTFURNITOR))) = x.code
    LEFT JOIN T_NIVELCMIMI n ON n.IDNIVELCMIMI = k.IDNIVELCMIMI
) t WHERE rn = 1;";

        private const string SqlMagazinat = @"
SELECT code = LTRIM(RTRIM(m.KODI)), description = ISNULL(m.PERSHKRIMI, ''), modifiedDate = ISNULL(m.DTMODIFIKIMI, m.DTKRIJIMI)
FROM T_NJESIADMINISTRATIVE m
WHERE m.IDNDERMARJE = @nd AND m.IDSTATUSDOK <> 2 AND ISNULL(m.AKTIV, 1) = 1 AND LTRIM(RTRIM(ISNULL(m.KODI, ''))) <> '';";

        private const string SqlGrupet = @"
SELECT code = ISNULL(NULLIF(LTRIM(RTRIM(k2.KODKODIFIKIMI)), ''), 'PA GRUPIM'), description = MAX(ISNULL(k2.PERSHKRIMKODIFIKIMI, ''))
FROM T_ARTIKULLI a LEFT JOIN T_KODIFIKIMARTIKULLI k2 ON k2.IDKODIFIKIMI = a.KODIFIKIMI2ARTIKULLI
WHERE a.IDNDERMARJE = @nd AND a.IDSTATUSDOK <> 2
GROUP BY ISNULL(NULLIF(LTRIM(RTRIM(k2.KODKODIFIKIMI)), ''), 'PA GRUPIM');";

        private const string SqlKategorite = @"
SELECT code = c.code, description = MAX(c.description),
       groupCodes = '[' + STRING_AGG('""' + STRING_ESCAPE(c.groupCode, 'json') + '""', ',') + ']'
FROM (
    SELECT DISTINCT code = ISNULL(NULLIF(LTRIM(RTRIM(k1.KODKODIFIKIMI)), ''), 'PA KATEGORI'),
                    description = ISNULL(k1.PERSHKRIMKODIFIKIMI, ''),
                    groupCode = ISNULL(NULLIF(LTRIM(RTRIM(k2.KODKODIFIKIMI)), ''), 'PA GRUPIM')
    FROM T_ARTIKULLI a
    LEFT JOIN T_KODIFIKIMARTIKULLI k1 ON k1.IDKODIFIKIMI = a.KODIFIKIMI1ARTIKULLI
    LEFT JOIN T_KODIFIKIMARTIKULLI k2 ON k2.IDKODIFIKIMI = a.KODIFIKIMI2ARTIKULLI
    WHERE a.IDNDERMARJE = @nd AND a.IDSTATUSDOK <> 2
) c
GROUP BY c.code;";
    }
}
