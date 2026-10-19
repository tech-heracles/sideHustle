using System;
using System.Collections.Generic;
using System.Globalization;

namespace DbCore.IMBUtils.Cache
{
    /// <summary>
    /// Cache per kerkimet e te dhenave reference (llogari, konfigurime, qytete, ...) gjate nje importi/kontrolli te vetem.
    /// Importet benin te njejtat kerkime per cdo rresht (p.sh. llogaria 411 tri here per rresht); brenda nje
    /// <see cref="Fillo"/> ato behen nje here.
    /// <para>Rregullat qe e mbajne te sakte:</para>
    /// <list type="bullet">
    /// <item>jeton vetem sa zgjat importi (using) dhe vetem ne thread-in e kerkeses; jashte tij metodat punojne si me pare;</item>
    /// <item>ruhen vetem rezultatet "e gjetura", qe nje objekt qe krijohet me vone gjate importit te gjendet;</item>
    /// <item>llojet qe i shkruan vete importi (p.sh. llogarite kur importohen llogari) nuk ruhen fare.</item>
    /// </list>
    /// </summary>
    public sealed class ImportCache : IDisposable
    {
        public const string Llogari = "Llogari";
        public const string Konfigurim = "Konfigurim";
        public const string MonedhaNdermarrje = "MonedhaNdermarrje";
        public const string Qyteti = "Qyteti";
        public const string NivelCmimi = "NivelCmimi";
        public const string Alternativa = "Alternativa";
        public const string KlientFurnitor = "KlientFurnitor";
        public const string Artikull = "Artikull";
        /// <summary>
        /// Te dhenat e lidhura me artikullin me id 0 (artikull i ri, ende pa id): asnje import nuk shkruan me id 0,
        /// keshtu qe ndryshe nga <see cref="Artikull"/> ruhen gjithmone.
        /// </summary>
        public const string ArtikullIRi = "ArtikullIRi";
        /// <summary>
        /// Te dhenat baze te artikullit sipas kodit (ekziston; rreshti me autorizime: njesite, klasa, aktiv, llogarite).
        /// Ruajtja e dokumenteve nuk i ndryshon (rivleresimi i kostos nuk shkruan ne T_ARTIKULLI), prandaj ndryshe nga
        /// <see cref="Artikull"/> ruhen edhe gjate importit te dokumenteve; i perjashtojne vetem importet e artikujve.
        /// </summary>
        public const string ArtikullKod = "ArtikullKod";
        /// <summary>
        /// Levizjet e artikullit ne magazine per kontrollin e gjendjes (KontrollGjendjeMesatare). Vetem ne kontroll:
        /// ruajtja i shton levizje, prandaj ne import perjashtohet gjithmone.
        /// </summary>
        public const string Gjendje = "Gjendje";
        public const string NivelZbritje = "NivelZbritje";
        public const string VlereDefault = "VlereDefault";
        public const string Ndermarrje = "Ndermarrje";
        public const string NjesiAdministrative = "NjesiAdministrative";
        public const string Skema = "Skema";
        public const string Njesi = "Njesi";
        public const string MetodeKostoje = "MetodeKostoje";
        public const string KlasaArtikulli = "KlasaArtikulli";
        public const string Kodbar = "Kodbar";
        public const string Taksa = "Taksa";
        public const string Kurset = "Kurset";
        public const string Periudha = "Periudha";
        public const string KonfigServeri = "KonfigServeri";
        public const string Operatori = "Operatori";
        public const string Procesi = "Procesi";
        public const string TipiEinvoice = "TipiEinvoice";
        public const string Viti = "Viti";
        public const string Autorizim = "Autorizim";
        public const string DegeAdministrative = "DegeAdministrative";
        public const string GrupKF = "GrupKF";
        public const string AgjentShitje = "AgjentShitje";

        [ThreadStatic] private static ImportCache aktiv;

        private readonly Dictionary<string, object> vlerat = new Dictionary<string, object>();
        private readonly HashSet<string> tePerjashtuara;
        private readonly ImportCache paraardhesi;
        private readonly bool vetemLexim;

        private ImportCache(bool vetemLexim, IEnumerable<string> llojetQeShkruhen)
        {
            this.vetemLexim = vetemLexim;
            tePerjashtuara = new HashSet<string>(llojetQeShkruhen ?? new string[0]);
            paraardhesi = aktiv;
            aktiv = this;
        }

        /// <param name="vetemLexim">kontroll pa import: asgje nuk shkruhet, prandaj ruhen edhe rezultatet "nuk u gjet"</param>
        /// <param name="llojetQeShkruhen">llojet e te dhenave qe ky import i krijon/modifikon (nuk ruhen ne cache)</param>
        public static ImportCache Fillo(bool vetemLexim, params string[] llojetQeShkruhen) => new ImportCache(vetemLexim, llojetQeShkruhen);

        public void Dispose()
        {
            if (aktiv == this)
                aktiv = paraardhesi;
        }

        /// <summary>Lloji i cache-it per te dhena sipas id se artikullit: <see cref="ArtikullIRi"/> per id &lt;= 0, perndryshe <see cref="Artikull"/>.</summary>
        public static string LlojiArtikullit(int idArtikulli) => idArtikulli <= 0 ? ArtikullIRi : Artikull;

        /// <summary>True kur ka nje import/kontroll aktiv ne kete thread.</summary>
        public static bool Aktiv => aktiv != null;

        /// <summary>True kur ka nje import/kontroll aktiv ne kete thread dhe ky lloj ruhet ne cache.</summary>
        public static bool Perdoret(string lloji) => aktiv != null && !aktiv.tePerjashtuara.Contains(lloji);

        /// <summary>
        /// Kthen vleren nga cache ose e llogarit; e ruan kur <paramref name="ruaj"/> e pranon rezultatin, ose gjithmone ne
        /// nje kontroll pa import (asgje nuk krijohet gjate tij, keshtu qe edhe "nuk u gjet" mbetet i vertete).
        /// Pa nje import aktiv thjesht therret <paramref name="llogarit"/>.
        /// </summary>
        public static T Merr<T>(string lloji, string celesi, Func<T> llogarit, Func<T, bool> ruaj)
        {
            var cache = aktiv;
            if (cache == null || cache.tePerjashtuara.Contains(lloji))
                return llogarit();
            string k = lloji + "\u0001" + celesi;
            if (cache.vlerat.TryGetValue(k, out object v))
                return (T)v;
            T rezultati = llogarit();
            if (cache.vetemLexim || ruaj(rezultati))
                cache.vlerat[k] = rezultati;
            return rezultati;
        }

        /// <summary>
        /// Kontroll ekzistence (p.sh. "ekziston artikulli me kete kod"). Si <see cref="Merr{T}"/> me ruajtje te "ekziston";
        /// por edhe kur importi e shkruan kete lloj, pergjigjja "nuk ekziston" mbahet derisa te behet shkrimi i radhes i
        /// llojit (<see cref="Shkruar"/> i fshin te gjitha). Keshtu kontrolli i dyte i te njejtit rresht (ne krijim dhe
        /// perseri ne ruajtje, para insert-it) nuk shkon me ne databaze.
        /// </summary>
        public static bool Ekziston(string lloji, string celesi, Func<bool> lexo)
        {
            var cache = aktiv;
            if (cache == null)
                return lexo();
            if (cache.vetemLexim || !cache.tePerjashtuara.Contains(lloji))
                return Merr(lloji, celesi, lexo, ekziston => ekziston);

            if (cache.mungesat.TryGetValue(lloji, out HashSet<string> mungojne) && mungojne.Contains(celesi))
                return false;
            bool rezultati = lexo();
            if (!rezultati)
            {
                if (mungojne == null)
                    cache.mungesat[lloji] = mungojne = new HashSet<string>(StringComparer.Ordinal);
                mungojne.Add(celesi);
            }
            return rezultati;
        }

        /// <summary>
        /// Thirret para cdo shkrimi te llojit kur nuk dihet cfare vlerash shton: harron pergjigjet "nuk ekziston" te tij
        /// dhe indekset e tij nuk perdoren me ne kete import.
        /// </summary>
        public static void Shkruar(string lloji)
        {
            var cache = aktiv;
            if (cache == null)
                return;
            cache.mungesat.Remove(lloji);
            cache.indeksePaVlere.Add(lloji);
            foreach (var k in new List<string>(cache.indekset.Keys))
                if (k.StartsWith(lloji + "\u0001", StringComparison.Ordinal))
                    cache.indekset.Remove(k);
        }

        /// <summary>
        /// Thirret para nje insert-i/update-i qe mund te beje te ekzistoje vetem <paramref name="vlera"/> (p.sh. kodi i
        /// artikullit te ri): harron pergjigjet "nuk ekziston" dhe e shton vleren ne indekset e llojit.
        /// </summary>
        public static void Shtuar(string lloji, string vlera)
        {
            var cache = aktiv;
            if (cache == null)
                return;
            if (vlera == null)
            {
                Shkruar(lloji);
                return;
            }
            cache.mungesat.Remove(lloji);
            foreach (var kv in cache.indekset)
                if (kv.Key.StartsWith(lloji + "\u0001", StringComparison.Ordinal))
                    kv.Value.Shto(vlera);
        }

        /// <summary>Thirret para nje fshirjeje te llojit: harron pergjigjet "nuk ekziston"; indekset mbeten te vlefshme (vetem me te gjere).</summary>
        public static void Fshire(string lloji)
        {
            aktiv?.mungesat.Remove(lloji);
        }

        /// <summary>
        /// Kthen true vetem kur <paramref name="vlera"/> me siguri nuk ekziston ne databaze (grupi = p.sh. ndermarrja), pa
        /// shkuar ne databaze. Pasi nje import ka bere <see cref="LeximePerIndeks"/> kontrolle ekzistence per te njejtin lloj
        /// dhe grup, ngarkon nje here te gjitha vlerat ekzistuese (<paramref name="ngarko"/>, me te njejtin filter si
        /// kontrolli); me pas vlerat qe nuk jane aty kthejne true. Krahasimi eshte me i gjere se ai i SQL-it (shih
        /// <see cref="KrahasuesiKodeve"/>), dhe vlerat jo ASCII nuk kontrollohen, keshtu qe nje vlere qe SQL-i e gjen nuk
        /// kthen kurre true; per te gjitha te tjerat (false) kontrolli behet ne databaze si me pare.
        /// </summary>
        /// <param name="gjatesia">gjatesia e parametrit varchar te kontrollit (SQL Server e shkurton vleren ne kete gjatesi)</param>
        public static bool Mungon(string lloji, string grupi, string vlera, int gjatesia, Func<IEnumerable<string>> ngarko)
        {
            var cache = aktiv;
            if (cache == null || vlera == null || cache.indeksePaVlere.Contains(lloji))
                return false;
            string k = lloji + "\u0001" + grupi;
            if (!cache.indekset.TryGetValue(k, out Indeksi indeksi))
            {
                cache.leximet.TryGetValue(k, out int n);
                cache.leximet[k] = ++n;
                if (n < LeximePerIndeks)
                    return false;
                indeksi = new Indeksi(gjatesia);
                foreach (var v in ngarko())
                    indeksi.Shto(v);
                cache.indekset[k] = indeksi;
            }
            return indeksi.Mungon(vlera);
        }

        /// <summary>Sa kontrolle ekzistence ne databaze behen para se te ngarkohet indeksi (importet e vogla nuk e ngarkojne).</summary>
        public const int LeximePerIndeks = 100;

        private sealed class Indeksi
        {
            private readonly int gjatesia;
            private readonly HashSet<string> vlerat = new HashSet<string>(KrahasuesiKodeve.Instanca);

            internal Indeksi(int gjatesia)
            {
                this.gjatesia = gjatesia;
            }

            private string Shkurto(string v) => v.Length > gjatesia ? v.Substring(0, gjatesia) : v;

            internal void Shto(string v)
            {
                if (v != null)
                    vlerat.Add(Shkurto(v));
            }

            internal bool Mungon(string v)
            {
                v = Shkurto(v);
                foreach (char c in v)
                    if (c < ' ' || c > '~')
                        return false;   // vetem ASCII e printueshme: per to krahasimi i SQL-it njihet sakte
                return !vlerat.Contains(v);
            }
        }

        private readonly Dictionary<string, HashSet<string>> mungesat = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, Indeksi> indekset = new Dictionary<string, Indeksi>();
        private readonly Dictionary<string, int> leximet = new Dictionary<string, int>();
        private readonly HashSet<string> indeksePaVlere = new HashSet<string>();
    }

    /// <summary>
    /// Krahasim kodesh me i gjere se ai i kolonave te SQL-it (pa dallim germash, theksash, gjeresie, simbolesh dhe
    /// hapesirash ne fund): nese SQL-i i quan dy kode te njejte, edhe ky. Perdoret aty ku nje "jo e njejte" lejon te
    /// anashkalohet databaza, ndersa nje "e njejte" thjesht con ne kontrollin e zakonshem.
    /// </summary>
    internal sealed class KrahasuesiKodeve : IEqualityComparer<string>
    {
        internal static readonly KrahasuesiKodeve Instanca = new KrahasuesiKodeve();
        private const CompareOptions Opsionet = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth
                                                | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols;
        private static readonly CompareInfo Info = CultureInfo.InvariantCulture.CompareInfo;
        public bool Equals(string x, string y) => Info.Compare((x ?? "").TrimEnd(), (y ?? "").TrimEnd(), Opsionet) == 0;
        public int GetHashCode(string s) => Info.GetHashCode((s ?? "").TrimEnd(), Opsionet);
    }
}
