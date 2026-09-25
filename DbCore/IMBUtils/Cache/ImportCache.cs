using System;
using System.Collections.Generic;

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
        public const string Periudha = "Periudha";

        [ThreadStatic] private static ImportCache aktiv;

        private readonly Dictionary<string, object> vlerat = new Dictionary<string, object>();
        private readonly HashSet<string> tePerjashtuara;
        private readonly ImportCache paraardhesi;

        private ImportCache(IEnumerable<string> llojetQeShkruhen)
        {
            tePerjashtuara = new HashSet<string>(llojetQeShkruhen ?? new string[0]);
            paraardhesi = aktiv;
            aktiv = this;
        }

        /// <param name="llojetQeShkruhen">llojet e te dhenave qe ky import i krijon/modifikon (nuk ruhen ne cache)</param>
        public static ImportCache Fillo(params string[] llojetQeShkruhen) => new ImportCache(llojetQeShkruhen);

        public void Dispose()
        {
            if (aktiv == this)
                aktiv = paraardhesi;
        }

        /// <summary>
        /// Kthen vleren nga cache ose e llogarit; e ruan vetem kur <paramref name="ruaj"/> e pranon rezultatin.
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
            if (ruaj(rezultati))
                cache.vlerat[k] = rezultati;
            return rezultati;
        }
    }
}
