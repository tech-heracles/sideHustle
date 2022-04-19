using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using CacheLayer;
using DbCore.IMBUtils.Cache;

namespace DbCore.DbAdmin
{
    public class clsTeDrejtaRoli : IDataBaseReader
    {
        //public const string mySessionKey = "clsTeDrejtaRoli";
        #region Atributet

        private int idDrejta;
        private int idPeme; //id qe i vendoset te drejtave gjate krijimit te pemes
        private Nullable<Int32> idPrindPeme;
        private Nullable<Int32> idPrindi;
        private int idNdermarrje;
        private int idViti;
        private int idModul;
        private int idLlojLicence;
        private String textModuli;
        private int idKomponente;
        private String pershkrimKomponente;
        private int idRoli;
        private String textRoli;
        private bool dPlot;
        private bool dMod;
        private bool dFsh;
        private bool dAmb = true;
        private bool dShtim;
        private int tipi;
        private bool dGjitheDok;
        private bool dNdryshoCmimShitje;
        private bool dNdryshoCmimBlerje;
        private bool dNdryshoZbritjeAnalitike;
        private bool dNdryshoZbritjeTotale;
        private bool dKonvertimi;
        private bool dKonvertimSipasUrdherShitje;
        private bool dShtimDraft;
        private bool dModifikimDraft;
        private Permbledhes permbledhes;
        private bool eshteGjethe;
        private bool expanded = false;
        private int niveli;
        private bool loaded = false;
        private int idRaport;
        private int idPerdoruesi;
        private int idDrejtaKoka;
        private Nullable<Int32> idDrejtaPrindi;
        private Nullable<Int32> idRaporti;
        private string tabi;
        private Nullable<Int32> idLayer;
        private bool dKerko;
        private bool dEksporto;
        private bool dPrinto;
        private bool dArkiva;
        private bool dKonverto;
        private bool dPezullo;
        private bool dAutoKonverto;
        private string komponente;
        private int idNivelRegjistrimi;
        private int idKategoria;
        #endregion

        #region Properties

        /// <summary>
        /// get apo set permbledhes qe tregon nese eshte permbledhes rreshti apo jo. sherben vetem per vizualizimin ne peme te te drejtave.
        /// merr tre vlera: 
        /// 0 - nuk eshte permbledhes
        /// 1 - eshte permbledhes moduli
        /// </summary>
        public Permbledhes Permbledhes
        {
            get
            {
                return permbledhes;
            }
            set
            {
                if (permbledhes == value)
                    return;
                permbledhes = value;
            }
        }

        /// <summary>
        /// get dhe set emri i modulit
        /// </summary>
        public String TextModuli
        {
            get
            {
                return textModuli;
            }
            set
            {
                if (textModuli == value)
                    return;
                textModuli = value;
            }
        }

        public String PershkrimKomponente
        {
            get
            {
                return pershkrimKomponente;
            }
            set
            {
                if (pershkrimKomponente == value)
                    return;
                pershkrimKomponente = value;
            }
        }

        public String TextRoli
        {
            get
            {
                return textRoli;
            }
            set
            {
                if (textRoli == value)
                    return;
                textRoli = value;
            }
        }

        public int IdRoli
        {
            get
            {
                return idRoli;
            }
            set
            {
                if (idRoli == value)
                    return;
                idRoli = value;
            }
        }

        /// <summary>
        /// get dhe set id-ne e komponentes
        /// </summary>
        public int IdKomponente
        {
            get
            {
                return idKomponente;
            }
            set
            {
                if (idKomponente == value)
                    return;
                idKomponente = value;
            }
        }

        /// <summary>
        /// get dhe set id-ne e komponentes
        /// </summary>
        public int IdLlojLicence
        {
            get
            {
                return idLlojLicence;
            }
            set
            {
                if (idLlojLicence == value)
                    return;
                idLlojLicence = value;
            }
        }

        public int IdModul
        {
            get
            {
                return idModul;
            }
            set
            {
                if (idModul == value)
                    return;
                idModul = value;
            }
        }

        public int IdViti
        {
            get
            {
                return idViti;
            }
            set
            {
                if (idViti == value)
                    return;
                idViti = value;
            }
        }

        public int IdNdermarrje
        {
            get
            {
                return idNdermarrje;
            }
            set
            {
                if (idNdermarrje == value)
                    return;
                idNdermarrje = value;
            }
        }

        public Nullable<Int32> IdPrindi
        {
            get
            {
                return idPrindi;
            }
            set
            {
                if (idPrindi == value)
                    return;
                idPrindi = value;
            }
        }

        public Nullable<Int32> IdPrindPeme
        {
            get
            {
                return idPrindPeme;
            }
            set
            {
                if (idPrindPeme == value)
                    return;
                idPrindPeme = value;
            }
        }

        public int IdDrejta
        {
            get
            {
                return idDrejta;
            }
            set
            {
                if (idDrejta == value)
                    return;
                idDrejta = value;
            }
        }

        public int IdRaport
        {
            get
            {
                return idRaport;
            }
            set
            {
                idRaport = value;
            }
        }

        public bool DAmb
        {
            get
            {
                return dAmb;
            }
            set
            {
                if (dAmb == value)
                    return;
                dAmb = value;
            }
        }

        public bool DFsh
        {
            get
            {
                return dFsh;
            }
            set
            {
                if (dFsh == value)
                    return;
                dFsh = value;
            }
        }

        public bool DPlot
        {
            get
            {
                return dPlot;
            }
            set
            {
                dPlot = value;
                if (value)
                {
                    dMod = true;
                    dAmb = true;
                    dShtim = true;
                    dPrinto = true;
                    dShtimDraft = true;
                    dModifikimDraft = true;
                    dArkiva = true;
                    dKonverto = true;
                    dPezullo = true;
                    dKerko = true;
                    dAutoKonverto = true;
                    dEksporto = true;
                }
            }
        }

        public bool DMod
        {
            get
            {
                return dMod;
            }
            set
            {
                if (dMod == value)
                    return;
                dMod = value;
            }
        }

        public bool DShtim
        {
            get
            {
                return dShtim;
            }
            set
            {
                dShtim = value;
            }
        }

        public int Tipi
        {
            get
            {
                return tipi;
            }
            set
            {
                tipi = value;
            }
        }

        /// <summary>
        /// Get/Set a mund t'i shohe perdoruesi te gjitha dokumentat, apo vetem ato qe ka krijuar vet
        /// </summary>
        public bool DGjitheDok
        {
            get
            {
                return dGjitheDok;
            }
            set
            {
                if (dGjitheDok == value)
                    return;
                dGjitheDok = value;
            }
        }

        /// <summary>
        /// Get Set: NdryshoCmimShitje
        /// </summary>
        public bool DNdryshoCmimShitje
        {
            get
            {
                return dNdryshoCmimShitje;
            }
            set
            {
                dNdryshoCmimShitje = value;
            }
        }

        /// <summary>
        /// Get Set: NdryshoCmimBlerje
        /// </summary>
        public bool DNdryshoCmimBlerje
        {
            get
            {
                return dNdryshoCmimBlerje;
            }
            set
            {
                dNdryshoCmimBlerje = value;
            }
        }

        /// <summary>
        /// Get Set: NdryshoZbritjeAnalitike
        /// </summary>
        public bool DNdryshoZbritjeAnalitike
        {
            get
            {
                return dNdryshoZbritjeAnalitike;
            }
            set
            {
                dNdryshoZbritjeAnalitike = value;
            }
        }

        /// <summary>
        /// Get Set: NdryshoZbritjeTotale
        /// </summary>
        public bool DNdryshoZbritjeTotale
        {
            get
            {
                return dNdryshoZbritjeTotale;
            }
            set
            {
                dNdryshoZbritjeTotale = value;
            }
        }

        /// <summary>
        /// Get Set: Vetem te drejte konvertimi ne Fature shitje
        /// </summary>
        public bool DKonvertimi
        {
            get
            {
                return dKonvertimi;
            }
            set
            {
                dKonvertimi = value;
            }
        }

        /// <summary>
        /// Get Set: Ne qofte se eshte true, atehere roli nuk mund te beje modifikime gjate konvertimit te fatures
        /// </summary>
        public bool DKonvertimSipasUrdherShitje
        {
            get
            {
                return dKonvertimSipasUrdherShitje;
            }
            set
            {
                dKonvertimSipasUrdherShitje = value;
            }
        }

        /// <summary>
        /// Get Set: Vetem te drejte konvertimi ne Fature shitje
        /// </summary>
        public bool DShtimDraft
        {
            get
            {
                return dShtimDraft;
            }
            set
            {
                dShtimDraft = value;
            }
        }

        /// <summary>
        /// Get Set: Ne qofte se eshte true, atehere roli nuk mund te beje modifikime gjate konvertimit te fatures
        /// </summary>
        public bool DModifikimDraft
        {
            get
            {
                return dModifikimDraft;
            }
            set
            {
                dModifikimDraft = value;
            }
        }

        /// <summary>
        /// Perdoren per ndertimin e treegrides
        /// </summary>
        public int IdPeme
        {
            get
            {
                return idPeme;
            }
            set
            {
                if (idPeme == value)
                    return;
                idPeme = value;
            }
        }

        /// <summary>
        /// Perdoren per ndertimin e treegrides
        /// </summary>
        public int Niveli
        {
            get
            {
                return niveli;
            }
            set
            {
                niveli = value;
            }
        }

        /// <summary>
        /// Perdoren per ndertimin e treegrides
        /// </summary>
        public bool Loaded
        {
            get
            {
                return loaded;
            }
            set
            {
                loaded = value;
            }
        }

        /// <summary>
        /// Perdoren per ndertimin e treegrides
        /// </summary>
        public bool EshteGjethe
        {
            get
            {
                return eshteGjethe;
            }
            set
            {
                if (eshteGjethe == value)
                    return;
                eshteGjethe = value;
            }
        }

        /// <summary>
        /// Perdoren per ndertimin e treegrides
        /// </summary>
        public bool Expanded
        {
            get
            {
                return expanded;
            }
            set
            {
                if (expanded == value)
                    return;
                expanded = value;
            }
        }

        public int IdDrejtaKoka
        {
            get { return idDrejtaKoka; }
            set { if (idDrejtaKoka == value) return; idDrejtaKoka = value; }
        }
        public Nullable<Int32> IdDrejtaPrindi
        {
            get { return idDrejtaPrindi; }
            set { if (idDrejtaPrindi == value) return; idDrejtaPrindi = value; }
        }
        public Nullable<Int32> IdRaporti
        {
            get { return idRaporti; }
            set { if (idRaporti == value) return; idRaporti = value; }
        }
        public string Tabi
        {
            get { return tabi; }
            set { if (tabi == value) return; tabi = value; }
        }
        public Nullable<Int32> IdLayer
        {
            get { return idLayer; }
            set { if (idLayer == value) return; idLayer = value; }
        }
        public bool DKerko
        {
            get { return dKerko; }
            set { if (dKerko == value) return; dKerko = value; }
        }
        public bool DEksporto
        {
            get { return dEksporto; }
            set { if (dEksporto == value) return; dEksporto = value; }
        }
        public bool DPrinto
        {
            get { return dPrinto; }
            set { if (dPrinto == value) return; dPrinto = value; }
        }
        public bool DArkiva
        {
            get { return dArkiva; }
            set { if (dArkiva == value) return; dArkiva = value; }
        }
        public bool DKonverto
        {
            get { return dKonverto; }
            set { if (dKonverto == value) return; dKonverto = value; }
        }
        public bool DPezullo
        {
            get { return dPezullo; }
            set { if (dPezullo == value) return; dPezullo = value; }
        }
        public bool DAutoKonverto
        {
            get { return dAutoKonverto; }
            set { if (dAutoKonverto == value) return; dAutoKonverto = value; }
        }

        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }

            set
            {
                idPerdoruesi = value;
            }
        }

        public string Komponente
        {
            get
            {
                return komponente;
            }

            set
            {
                komponente = value;
            }
        }

        public int IdNivelRegjistrimi { get { return idNivelRegjistrimi; } set { idNivelRegjistrimi = value; } }
        public int IdKategoria { get { return idKategoria; } set { idKategoria = value; } }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public clsTeDrejtaRoli()
        {
        }

        /// <summary>
        /// Perdoret vetem per te krijuar pemen
        /// <see cref="colTeDrejtaRoli.krijoPemePerTreeGrid"/>
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <param name="idPrindi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idModul"></param>
        /// <param name="textModuli"></param>
        /// <param name="idKomponente"></param>
        /// <param name="pershkrimKomponente"></param>
        /// <param name="idRoli"></param>
        /// <param name="dMod"></param>
        /// <param name="dFsh"></param>
        /// <param name="dAmb"></param>
        /// <param name="permbledhes"></param>
        internal clsTeDrejtaRoli(int idDrejta, Nullable<Int32> idPrindi, int idNdermarrje, int idViti, int idModul, string textModuli, int idKomponente, string pershkrimKomponente, int idRoli, bool dMod, bool dFsh, bool dAmb, bool dShtim, Permbledhes permbledhes, int idllojlicence, bool dGjitheDok, bool ndryshoCmimShitje, bool ndryshoCmimBlerje, bool ndryshoZbritjeAnalitike, bool ndryshoZbritjeTotale, bool eshtegjethe, int niveli, bool loaded, int idPeme, int idRap, Nullable<Int32> idPrindPeme, bool dKonvertimi, bool dKonvSipasUSH, bool dShtimDraft, bool dModifikimDraft, bool dKerko, bool dEksporto, bool dPrinto, bool dArkiva, bool dKonverto, bool dPezullo, bool dAutoKonverto, int idDrejtaKoka, Nullable<Int32> idLayer, int idNivelRegjistrimi, int idKategoria)
        {
            this.idPeme = idPeme;
            this.idDrejta = idDrejta;
            this.idPrindi = idPrindi;
            this.idPrindPeme = idPrindPeme;
            this.idNdermarrje = idNdermarrje;
            this.idViti = idViti;
            this.idModul = idModul;
            this.textModuli = textModuli;
            this.idKomponente = idKomponente;
            this.pershkrimKomponente = pershkrimKomponente;
            this.idRoli = idRoli;
            this.dMod = dMod;
            this.dFsh = dFsh;
            this.dAmb = dAmb;
            this.dShtim = dShtim;
            this.dGjitheDok = dGjitheDok;
            this.dShtimDraft = dShtimDraft;
            this.dModifikimDraft = dModifikimDraft;

            this.dKerko = dKerko;
            this.dEksporto = dEksporto;
            this.dPrinto = dPrinto;
            this.dArkiva = dArkiva;
            this.dKonverto = dKonverto;
            this.dPezullo = dPezullo;
            this.dAutoKonverto = dAutoKonverto;
            this.dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok && this.dShtimDraft && this.dModifikimDraft && this.dKerko && this.dEksporto && this.dPrinto && this.dArkiva && this.dPezullo && this.dAutoKonverto;
            this.permbledhes = permbledhes;
            this.idLlojLicence = idllojlicence;
            this.dNdryshoCmimShitje = ndryshoCmimShitje;
            this.dNdryshoCmimBlerje = ndryshoCmimBlerje;
            this.dNdryshoZbritjeAnalitike = ndryshoZbritjeAnalitike;
            this.dNdryshoZbritjeTotale = ndryshoZbritjeTotale;
            this.eshteGjethe = eshtegjethe;
            this.niveli = niveli;
            this.loaded = loaded;
            this.idRaport = idRap;
            this.idNivelRegjistrimi = idNivelRegjistrimi;
            this.idKategoria = idKategoria;
            this.dKonvertimi = dKonvertimi;
            this.dKonvertimSipasUrdherShitje = dKonvSipasUSH;

            this.idDrejtaKoka = idDrejtaKoka;
            this.idLayer = idLayer;
        }
        public clsTeDrejtaRoli(DataRow rreshti)
        {

            mbushTeDrejten(rreshti);
        }

        #endregion

        #region Metoda Publike

        public void merrTeDrejtaPerKeteKomponente(int idperdoruesi, int idNdermarrje, int idviti, string komponente)
        {
            GlobalCacheManager.MySessionCache.FillObjectFromCache(x => x.IdNdermarrje == idNdermarrje && x.IdViti == idviti && x.Komponente == komponente && x.IdPerdoruesi == idperdoruesi
                , () =>
                {
                    using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                    {
                        db.merrTeDrejtaSipasKomponentesDheRaportet(idperdoruesi, idNdermarrje, idviti, komponente, this);
                        return this;
                    }
                }, this, "", true
            );
            //nese raportit ose komponentjes nuk i jane vendosur te drejta,default duhet te ket
            if (IdKomponente == 0 && IdRaport == 0) DPlot = true;

        }

        public void merrTeDrejtaPerKeteKomponenteDheKategori(int idperdoruesi, int idNdermarrje, int idviti, string komponente, int idKategoria)
        {
            GlobalCacheManager.MySessionCache.FillObjectFromCache(x => x.IdNdermarrje == idNdermarrje && x.IdViti == idviti && x.Komponente == komponente && x.IdPerdoruesi == idperdoruesi && x.IdKategoria == idKategoria
                , () =>
                {
                    using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                    {
                        db.merrTeDrejtaSipasKomponentesDheRaportetDheKategori(idperdoruesi, idNdermarrje, idviti, komponente, idKategoria, this);
                        return this;
                    }
                }, this, "", true
            );
            //nese raportit ose komponentjes nuk i jane vendosur te drejta,default duhet te ket
            if (IdKomponente == 0 && IdRaport == 0) DPlot = true;

        }

        public void merrTeDrejtaPerKeteKomponenteDheNivelRegjistrimi(int idperdoruesi, int idNdermarrje, int idviti, string komponente, int idNivelRegjistrimi)
        {
            GlobalCacheManager.MySessionCache.FillObjectFromCache(x => x.IdNdermarrje == idNdermarrje && x.IdViti == idviti && x.Komponente == komponente && x.IdPerdoruesi == idperdoruesi && x.IdNivelRegjistrimi == idNivelRegjistrimi
                , () =>
                {
                    using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                    {
                        db.merrTeDrejtaSipasKomponentesDheNivelRegjistrimi(idperdoruesi, idNdermarrje, idviti, komponente, idNivelRegjistrimi, this);
                        return this;
                    }
                }, this, "", true
            );
            //nese raportit ose komponentjes nuk i jane vendosur te drejta,default duhet te ket
            if (IdKomponente == 0 && IdRaport == 0) DPlot = true;

        }

        public static bool kaTeDrejtaRoliPerNdermarrjeDheVit(int idRoli, int idNdermarrje, int idViti, clsDatabaseAdmin db)
        {
            //if (db == null)
            //    db = new clsDatabaseAdmin();
            bool ekziston = db.kaTeDrejtaRoliPerNdermarrjeDheVit(idRoli, idNdermarrje, idViti);
            return ekziston;
        }

        #endregion

        #region Metoda Internal

        public void Mbush(IDataRecord rreshti)
        {

            idDrejta = Convert.ToInt32(rreshti["IDDREJTA"]);
            idPrindi = colTeDrejtaRoli.rootPrindi;
            //idPrindi = Convert.ToInt32(rreshti["IDPRINDI"]);
            idNdermarrje = Convert.ToInt32(rreshti["IDNDERMARRJE"]);
            idViti = Convert.ToInt32(rreshti["IDVITI"]);
            idModul = Convert.ToInt32(rreshti["IDMODULI"]);
            textModuli = Convert.ToString(rreshti["TEXTMODUL"]);
            idKomponente = Convert.ToInt32(rreshti["IDKOMPONENTE"]);
            pershkrimKomponente = Convert.ToString(rreshti["PERSHKRIMKOMPONENTE"]);
            idRoli = Convert.ToInt32(rreshti["IDROLI"]);
            dMod = Convert.ToBoolean(rreshti["D_MOD"]);
            dFsh = Convert.ToBoolean(rreshti["D_FSH"]);
            dAmb = Convert.ToBoolean(rreshti["D_AMB"]);
            dShtim = Convert.ToBoolean(rreshti["D_SHTIM"]);
            dGjitheDok = Convert.ToBoolean(rreshti["D_GJITHEDOK"]);
            dNdryshoCmimShitje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMSHITJE"]);
            dNdryshoCmimBlerje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMBLERJE"]);
            dNdryshoZbritjeAnalitike = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJEANALITIKE"]);
            dNdryshoZbritjeTotale = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJETOTALE"]);
            dKonvertimi = Convert.ToBoolean(rreshti["D_KONVERTIMI"]);
            dKonvertimSipasUrdherShitje = Convert.ToBoolean(rreshti["D_KONVURDHER"]);
            dShtimDraft = Convert.ToBoolean(rreshti["D_SHTIMDRAFT"]);
            dModifikimDraft = Convert.ToBoolean(rreshti["D_MODIFIKIMDRAFT"]);
            komponente = rreshti["KOMPONEMRI"].ToString();
            int.TryParse(rreshti["IDPERDORUES"].ToString(), out idPerdoruesi);            
            niveli = 1;
            eshteGjethe = true;
            expanded = false;
            loaded = true;
            dKerko = Convert.ToBoolean(rreshti["D_KERKO"]);
            dEksporto = Convert.ToBoolean(rreshti["D_EKSPORTO"]);
            dPrinto = Convert.ToBoolean(rreshti["D_PRINTO"]);
            dArkiva = Convert.ToBoolean(rreshti["D_ARKIVA"]);
            dKonverto = Convert.ToBoolean(rreshti["D_KONVERTO"]);
            dPezullo = Convert.ToBoolean(rreshti["D_PEZULLO"]);
            dAutoKonverto = Convert.ToBoolean(rreshti["D_AUTOKONVERTO"]);
            dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok && this.dShtimDraft && this.dModifikimDraft && this.dKerko && this.dEksporto && this.dPrinto && this.dArkiva && this.dKonverto && this.dPezullo;
        }

        [Obsolete("Perdor Mbush(IDataRecord)")]

        /// <summary>
        /// mbush objektin nga nje datarow i marr nga db-ja
        /// </summary>
        /// <param name="rreshti">datarow me te dhenat e te drejtes</param>
        /// <returns>True nese te dhenat merren me sukses, False perndryshe</returns>
        internal bool mbushTeDrejten(DataRow rreshti)
        {
            try
            {

                idDrejta = Convert.ToInt32(rreshti["IDDREJTA"]);
                idPrindi = colTeDrejtaRoli.rootPrindi;
                //idPrindi = Convert.ToInt32(rreshti["IDPRINDI"]);
                idNdermarrje = Convert.ToInt32(rreshti["IDNDERMARRJE"]);
                idViti = Convert.ToInt32(rreshti["IDVITI"]);
                idModul = Convert.ToInt32(rreshti["IDMODULI"]);
                textModuli = Convert.ToString(rreshti["TEXTMODUL"]);
                idKomponente = Convert.ToInt32(rreshti["IDKOMPONENTE"]);
                pershkrimKomponente = Convert.ToString(rreshti["PERSHKRIMKOMPONENTE"]);
                idRoli = Convert.ToInt32(rreshti["IDROLI"]);
                dMod = Convert.ToBoolean(rreshti["D_MOD"]);
                dFsh = Convert.ToBoolean(rreshti["D_FSH"]);
                dAmb = Convert.ToBoolean(rreshti["D_AMB"]);
                dShtim = Convert.ToBoolean(rreshti["D_SHTIM"]);
                dGjitheDok = Convert.ToBoolean(rreshti["D_GJITHEDOK"]);
                dNdryshoCmimShitje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMSHITJE"]);
                dNdryshoCmimBlerje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMBLERJE"]);
                dNdryshoZbritjeAnalitike = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJEANALITIKE"]);
                dNdryshoZbritjeTotale = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJETOTALE"]);
                dKonvertimi = Convert.ToBoolean(rreshti["D_KONVERTIMI"]);
                dKonvertimSipasUrdherShitje = Convert.ToBoolean(rreshti["D_KONVURDHER"]);
                dShtimDraft = Convert.ToBoolean(rreshti["D_SHTIMDRAFT"]);
                dModifikimDraft = Convert.ToBoolean(rreshti["D_MODIFIKIMDRAFT"]);                
                niveli = 1;
                eshteGjethe = true;
                expanded = false;
                loaded = true;
                dKerko = Convert.ToBoolean(rreshti["D_KERKO"]);
                dEksporto = Convert.ToBoolean(rreshti["D_EKSPORTO"]);
                dPrinto = Convert.ToBoolean(rreshti["D_PRINTO"]);
                dArkiva = Convert.ToBoolean(rreshti["D_ARKIVA"]);
                dKonverto = Convert.ToBoolean(rreshti["D_KONVERTO"]);
                dPezullo = Convert.ToBoolean(rreshti["D_PEZULLO"]);
                dAutoKonverto = Convert.ToBoolean(rreshti["D_AUTOKONVERTO"]);
                dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok && this.dShtimDraft && this.dModifikimDraft && this.dKerko && this.dEksporto && this.dPrinto && this.dArkiva && this.dKonverto && this.dPezullo;
                
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        internal bool mbushTeDrejtePerTreeGride(DataRow rreshti)
        {
            try
            {
                idDrejta = Convert.ToInt32(rreshti["IDDREJTA"]);
                idPeme = Convert.ToInt32(rreshti["ID"]);
                idPrindi = Convert.ToInt32(rreshti["IDPRINDI"]);
                idNdermarrje = Convert.ToInt32(rreshti["IDNDERMARRJE"]);
                idViti = Convert.ToInt32(rreshti["IDVITI"]);
                idModul = Convert.ToInt32(rreshti["IDMODULI"]);
                textModuli = Convert.ToString(rreshti["TEXTMODUL"]);
                idKomponente = Convert.ToInt32(rreshti["IDKOMPONENTE"]);
                idRaport = Convert.ToInt32(rreshti["IDRAPORT"]);
                idNivelRegjistrimi = Convert.ToInt32(rreshti["IDNIVELREGJISTRIMI"]);
                idKategoria = Convert.ToInt32(rreshti["IDKATEGORIA"]);
                pershkrimKomponente = Convert.ToString(rreshti["PERSHKRIMKOMPONENTE"]);
                idRoli = Convert.ToInt32(rreshti["IDROLI"]);
                dMod = Convert.ToBoolean(rreshti["D_MOD"]);
                dFsh = Convert.ToBoolean(rreshti["D_FSH"]);
                dAmb = Convert.ToBoolean(rreshti["D_AMB"]);
                dShtim = Convert.ToBoolean(rreshti["D_SHTIM"]);
                dGjitheDok = Convert.ToBoolean(rreshti["D_GJITHEDOK"]);
                dNdryshoCmimShitje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMSHITJE"]);
                dNdryshoCmimBlerje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMBLERJE"]);
                dNdryshoZbritjeAnalitike = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJEANALITIKE"]);
                dNdryshoZbritjeTotale = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJETOTALE"]);
                dKonvertimi = Convert.ToBoolean(rreshti["D_KONVERTIMI"]);
                dKonvertimSipasUrdherShitje = Convert.ToBoolean(rreshti["D_KONVURDHER"]);
                dShtimDraft = Convert.ToBoolean(rreshti["D_SHTIMDRAFT"]);
                dModifikimDraft = Convert.ToBoolean(rreshti["D_MODIFIKIMDRAFT"]);
                niveli = Convert.ToInt32(rreshti["NIVELI"]);
                eshteGjethe = true;
                expanded = false;
                loaded = true;
                dKerko = Convert.ToBoolean(rreshti["D_KERKO"]);
                dEksporto = Convert.ToBoolean(rreshti["D_EKSPORTO"]);
                dPrinto = Convert.ToBoolean(rreshti["D_PRINTO"]);
                dArkiva = Convert.ToBoolean(rreshti["D_ARKIVA"]);
                dKonverto = Convert.ToBoolean(rreshti["D_KONVERTO"]);
                dPezullo = Convert.ToBoolean(rreshti["D_PEZULLO"]);
                dAutoKonverto = Convert.ToBoolean(rreshti["D_AUTOKONVERTO"]);
                dPlot = this.dMod && this.dFsh && this.dAmb && this.dShtim && this.dGjitheDok && dShtimDraft && dModifikimDraft && dKerko && dEksporto && dPrinto && dArkiva && dKonverto && dPezullo && dAutoKonverto;
                idDrejtaKoka = Convert.ToInt32(rreshti["IDDREJTAKOKA"]);
                idLayer = Convert.ToInt32(rreshti["IDLAYER"]);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}