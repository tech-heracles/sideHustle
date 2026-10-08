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
    }
}
