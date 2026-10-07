using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using AlphaWeb.Infrastructure.Data.AdoNet;
using Newtonsoft.Json;
using NLog;

namespace DbCore.IMBUtils.Licencimi
{
    /// <summary>
    /// Nje kompani (klient) e AVEC Accounting sipas licences ne Firebase.
    /// </summary>
    public sealed class LicencaAvec
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("name")] public string Emri { get; set; }
        [JsonProperty("startDate")] public DateTime? Fillimi { get; set; }
        [JsonProperty("endDate")] public DateTime? Mbarimi { get; set; }
        /// <summary>onPremise ose cloud</summary>
        [JsonProperty("clientType")] public string Lloji { get; set; }
        [JsonProperty("active")] public bool Aktive { get; set; } = true;
        /// <summary>I dekriptuar nga <see cref="ConnectionStringEnc"/>; ruhet vetem ne kopjen lokale (DPAPI).</summary>
        [JsonProperty("connectionString")] public string ConnectionString { get; set; }
        /// <summary>Si vjen nga serveri i licencave: i enkriptuar me celesin e ketij instalimi.</summary>
        [JsonProperty("connectionStringEnc")] public string ConnectionStringEnc { get; set; }

        /// <summary>Emri me te cilin lidhja regjistrohet ne ConnectionStringsManager.</summary>
        [JsonIgnore] public string EmriLidhjes => PrefiksiLidhjes + Id;

        public const string PrefiksiLidhjes = "avec_";

        /// <summary>
        /// Kthen null kur licenca lejon hyrjen ne daten e dhene, perndryshe mesazhin per perdoruesin.
        /// Datat jane dite te plota: fillimi dhe mbarimi perfshihen.
        /// </summary>
        public string Kontrollo(DateTime sot)
        {
            if (!Aktive)
                return $"Licenca e kompanise {Emri} eshte e çaktivizuar.";
            // datat vijne ne UTC; nje date e vendosur ne mesnate ore lokale (konsola e Firebase) eshte 22:00 UTC e dites
            // se meparshme, prandaj krahasimi behet ne oren lokale te serverit
            DateTime? fillimi = Fillimi?.ToLocalTime().Date, mbarimi = Mbarimi?.ToLocalTime().Date;
            if (fillimi.HasValue && sot.Date < fillimi.Value)
                return $"Licenca e kompanise {Emri} fillon me {fillimi.Value:dd/MM/yyyy}.";
            if (mbarimi.HasValue && sot.Date > mbarimi.Value)
                return $"Licenca e kompanise {Emri} ka skaduar me {mbarimi.Value:dd/MM/yyyy}.";
            if (string.IsNullOrWhiteSpace(ConnectionString))
                return $"Kompania {Emri} nuk ka ende nje databaze te lidhur.";
            return null;
        }
    }

    public sealed class LicencaAvecException : Exception
    {
        public LicencaAvecException(string message, Exception inner = null) : base(message, inner) { }
    }

    /// <summary>
    /// Lista e kompanive te ketij instalimi, nga funksioni avecLicenses ne Firebase (projekti i menaxherit).
    /// Zevendeson T_SERVER_CONNECTIONSTRINGS / T_LICENCA te databazes kryesore si burim i kompanive ne login.
    /// <para>Konfigurimi (appSettings, ne avecLicense.config qe nuk hyn ne git): AvecLicenseUrl, AvecInstallationId, AvecInstallationKey.</para>
    /// <para>Ne hyrje (<see cref="MerrPerHyrje"/>) lista lexohet gjithmone nga Firebase; gjate punes rifreskohet cdo 5 minuta.
    /// Vetem kur Firebase nuk arrihet perdoret pergjigja e fundit (ne memorie ose e ruajtur e enkriptuar ne App_Data), per
    /// 2 dite nga rifreskimi i fundit i suksesshem; pas kesaj hyrja bllokohet.</para>
    /// </summary>
    public static class LicencatAvec
    {
        private static readonly TimeSpan RifreskoPas = TimeSpan.FromMinutes(5);
        // hapat e nje hyrjeje (lista e kompanive, zgjedhja e kompanise) ndodhin brenda pak sekondash: nje lexim per hyrje
        private static readonly TimeSpan FreskiPerHyrje = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan PritPasDeshtimit = TimeSpan.FromMinutes(1);
        public static readonly TimeSpan MaxPaLidhje = TimeSpan.FromDays(2);
        private static readonly byte[] Entropia = Encoding.UTF8.GetBytes("AVEC.Licencat.v1");
        private static readonly HttpClient klienti = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        private static readonly object bllokimi = new object();
        private static readonly Logger log = LogManager.GetLogger("Licencat");

        private static List<LicencaAvec> licencat;
        private static DateTime? rifreskuarMe;          // UTC, rifreskimi i fundit i suksesshem
        private static DateTime deshtuarMe = DateTime.MinValue;

        private sealed class Ruajtja
        {
            public DateTime RifreskuarMe { get; set; }
            public List<LicencaAvec> Licencat { get; set; }
        }

        private sealed class Pergjigja
        {
            [JsonProperty("companies")] public List<LicencaAvec> Kompanite { get; set; }
        }

        /// <summary>Kompanite e ketij instalimi. Hedh <see cref="LicencaAvecException"/> kur lista nuk mund te merret.</summary>
        public static IReadOnlyList<LicencaAvec> Merr(bool rifresko = false) => Merr(RifreskoPas, rifresko);

        /// <summary>
        /// Per hyrjen: lista lexohet nga Firebase (jo nga lista 5-minuteshe ne memorie). Vetem kur Firebase nuk arrihet
        /// perdoret pergjigja e fundit, deri ne 2 dite (<see cref="MaxPaLidhje"/>).
        /// </summary>
        public static IReadOnlyList<LicencaAvec> MerrPerHyrje() => Merr(FreskiPerHyrje, false);

        private static IReadOnlyList<LicencaAvec> Merr(TimeSpan moshaMax, bool rifresko)
        {
            lock (bllokimi)
            {
                DateTime tani = DateTime.UtcNow;
                if (licencat == null)
                    NgarkoNgaDisku();
                bool eFreskete = licencat != null && rifreskuarMe.HasValue && tani - rifreskuarMe.Value < moshaMax;
                bool sapoDeshtoi = tani - deshtuarMe < PritPasDeshtimit;
                if (eFreskete && !rifresko)
                    return licencat;

                if (!sapoDeshtoi || rifresko)
                {
                    try
                    {
                        Vendos(Shkarko(), tani);
                        RuajNeDisk();
                        return licencat;
                    }
                    catch (LicencaAvecException ex)
                    {
                        // pergjigje e qarte (pa konfigurim, ose celesi u refuzua = instalimi u revokua): lista e vjeter nuk vlen me
                        deshtuarMe = tani;
                        log.Error(ex, "Licencat: " + ex.Message);
                        licencat = null;
                        rifreskuarMe = null;
                        FshiNgaDisku();
                        throw;
                    }
                    catch (Exception ex)
                    {
                        // Firebase nuk arrihet: vazhdohet me listen e fundit brenda kufirit 2-ditor
                        deshtuarMe = tani;
                        log.Error(ex, "Lista e licencave nuk u mor nga Firebase");
                    }
                }

                if (licencat != null && rifreskuarMe.HasValue && tani - rifreskuarMe.Value < MaxPaLidhje)
                    return licencat;
                throw new LicencaAvecException(licencat == null
                    ? "Lista e kompanive nuk mund te merret nga serveri i licencave. Kontrolloni lidhjen me internetin."
                    : $"Licencat nuk jane verifikuar qe prej {rifreskuarMe.Value.ToLocalTime():dd/MM/yyyy HH:mm}. Lidheni serverin me internetin per te vazhduar.");
            }
        }

        public static LicencaAvec Gjej(string id) =>
            string.IsNullOrEmpty(id) ? null : Merr().FirstOrDefault(l => string.Equals(l.Id, id, StringComparison.Ordinal));

        /// <summary>Sipas id-se ose emrit (p.sh. "organization" qe dergojne sherbimet e automatizimit).</summary>
        public static LicencaAvec GjejSipasIdOseEmrit(string vlera) =>
            string.IsNullOrEmpty(vlera) ? null : Merr().FirstOrDefault(l =>
                string.Equals(l.Id, vlera, StringComparison.Ordinal) || string.Equals(l.Emri, vlera, StringComparison.OrdinalIgnoreCase));

        /// <summary>Licenca qe i perket nje lidhjeje te zgjedhur (null per lidhjen default ose te panjohur).</summary>
        public static LicencaAvec GjejSipasLidhjes(string emriLidhjes)
        {
            if (string.IsNullOrEmpty(emriLidhjes) || !emriLidhjes.StartsWith(LicencaAvec.PrefiksiLidhjes, StringComparison.Ordinal))
                return null;
            return Gjej(emriLidhjes.Substring(LicencaAvec.PrefiksiLidhjes.Length));
        }

        private static List<LicencaAvec> Shkarko()
        {
            string url = WebConfigurationManager.AppSettings["AvecLicenseUrl"];
            string instalimi = WebConfigurationManager.AppSettings["AvecInstallationId"];
            string celesi = WebConfigurationManager.AppSettings["AvecInstallationKey"];
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(instalimi) || string.IsNullOrWhiteSpace(celesi))
                throw new LicencaAvecException("Licencimi nuk eshte konfiguruar ne kete server (avecLicense.config).");

            using (var kerkesa = new HttpRequestMessage(HttpMethod.Post, url))
            {
                kerkesa.Headers.Add("X-Avec-Key", celesi);
                kerkesa.Content = new StringContent(JsonConvert.SerializeObject(new { installationId = instalimi }), Encoding.UTF8, "application/json");
                using (HttpResponseMessage pergjigja = klienti.SendAsync(kerkesa).GetAwaiter().GetResult())
                {
                    string trupi = pergjigja.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (pergjigja.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new LicencaAvecException("Ky server nuk njihet nga serveri i licencave (celes ose instalim i gabuar).");
                    if (!pergjigja.IsSuccessStatusCode)
                        throw new InvalidOperationException($"avecLicenses ktheu {(int)pergjigja.StatusCode}");
                    List<LicencaAvec> kompanite = JsonConvert.DeserializeObject<Pergjigja>(trupi)?.Kompanite ?? new List<LicencaAvec>();
                    foreach (LicencaAvec l in kompanite)
                    {
                        // vetem lidhja e enkriptuar pranohet nga rrjeti; nje e pa enkriptuar injorohet
                        l.ConnectionString = Dekripto(l.ConnectionStringEnc, celesi);
                        l.ConnectionStringEnc = null;
                    }
                    return kompanite;
                }
            }
        }

        /// <summary>
        /// Pale e encrypt() te avecLicenses: "v1:" + base64(iv[16] | AES-256-CBC | HMAC-SHA256(iv | shifra)).
        /// Celesat nxirren nga SHA256 i celesit te instalimit (keyHash qe serveri i licencave ruan).
        /// Kthen null kur mungon ose kur nuk verifikohet (kompania del pa databaze, si para konfigurimit).
        /// </summary>
        internal static string Dekripto(string teDhenat, string celesiInstalimit)
        {
            if (string.IsNullOrEmpty(teDhenat) || !teDhenat.StartsWith("v1:", StringComparison.Ordinal))
                return null;
            try
            {
                byte[] paketa = Convert.FromBase64String(teDhenat.Substring(3));
                if (paketa.Length < 16 + 16 + 32)
                    return null;
                string keyHash;
                using (var sha = SHA256.Create())
                    keyHash = Hex(sha.ComputeHash(Encoding.UTF8.GetBytes(celesiInstalimit)));
                byte[] encKey, macKey;
                using (var sha = SHA256.Create())
                {
                    encKey = sha.ComputeHash(Encoding.UTF8.GetBytes("AVEC.conn.enc|" + keyHash));
                    macKey = sha.ComputeHash(Encoding.UTF8.GetBytes("AVEC.conn.mac|" + keyHash));
                }
                int gjatesia = paketa.Length - 32;
                byte[] mac;
                using (var hmac = new HMACSHA256(macKey))
                    mac = hmac.ComputeHash(paketa, 0, gjatesia);
                int ndryshim = 0;   // krahasim ne kohe konstante
                for (int i = 0; i < 32; i++)
                    ndryshim |= mac[i] ^ paketa[gjatesia + i];
                if (ndryshim != 0)
                {
                    log.Error("Lidhja e enkriptuar e nje kompanie nuk u verifikua (celes instalimi i ndryshem?)");
                    return null;
                }
                using (var aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = encKey;
                    aes.IV = paketa.Take(16).ToArray();
                    using (ICryptoTransform dekriptuesi = aes.CreateDecryptor())
                        return Encoding.UTF8.GetString(dekriptuesi.TransformFinalBlock(paketa, 16, gjatesia - 16));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Lidhja e enkriptuar e nje kompanie nuk u lexua");
                return null;
            }
        }

        private static string Hex(byte[] b)
        {
            var sb = new StringBuilder(b.Length * 2);
            foreach (byte x in b)
                sb.Append(x.ToString("x2"));
            return sb.ToString();
        }

        private static void Vendos(List<LicencaAvec> reja, DateTime koha)
        {
            foreach (LicencaAvec l in reja.Where(l => !string.IsNullOrWhiteSpace(l.Id) && !string.IsNullOrWhiteSpace(l.ConnectionString)))
                ConnectionStringsManager.Instance.SetConnectionstring(l.EmriLidhjes, l.ConnectionString);
            licencat = reja;
            rifreskuarMe = koha;
        }

        private static string Skedari =>
            HostingEnvironment.MapPath("~/App_Data/licencat-avec.dat")
            ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "licencat-avec.dat");

        // ruhet e enkriptuar me DPAPI te makines: permban connection string-et
        private static void RuajNeDisk()
        {
            try
            {
                string skedari = Skedari;
                if (skedari == null)
                    return;
                Directory.CreateDirectory(Path.GetDirectoryName(skedari));
                byte[] te_dhenat = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Ruajtja { RifreskuarMe = rifreskuarMe.Value, Licencat = licencat }));
                File.WriteAllBytes(skedari, ProtectedData.Protect(te_dhenat, Entropia, DataProtectionScope.LocalMachine));
            }
            catch (Exception ex)
            {
                log.Warn(ex, "Lista e licencave nuk u ruajt ne disk");
            }
        }

        private static void FshiNgaDisku()
        {
            try
            {
                string skedari = Skedari;
                if (skedari != null && File.Exists(skedari))
                    File.Delete(skedari);
            }
            catch (Exception ex)
            {
                log.Warn(ex, "Lista e ruajtur e licencave nuk u fshi");
            }
        }

        private static void NgarkoNgaDisku()
        {
            try
            {
                string skedari = Skedari;
                if (skedari == null || !File.Exists(skedari))
                    return;
                byte[] te_dhenat = ProtectedData.Unprotect(File.ReadAllBytes(skedari), Entropia, DataProtectionScope.LocalMachine);
                var ruajtja = JsonConvert.DeserializeObject<Ruajtja>(Encoding.UTF8.GetString(te_dhenat));
                if (ruajtja?.Licencat != null)
                    Vendos(ruajtja.Licencat, DateTime.SpecifyKind(ruajtja.RifreskuarMe, DateTimeKind.Utc));
            }
            catch (Exception ex)
            {
                log.Warn(ex, "Lista e ruajtur e licencave nuk u lexua");
            }
        }
    }
}
