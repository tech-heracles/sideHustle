using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Types;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje komponente page
    ///  (Te dhenat  merren nga tabela : T_KOMPONENTEPAGE)
    /// </summary>
    /// 

    public class clsKomponentePage
    {
        public static string mbushjeSukses = "Komponentja u mbush me sukses";
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se komponentes nga db-ja";
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        private static NLog.Logger logu = NLog.LogManager.GetCurrentClassLogger();
        #region Atribute

        private int idKomponentePage;
        private string kodi;
        private string pershkrimi;
        private int njesi;
        private string kodParam;
        private string emerParam;
        private int njesiParam;
        private string formula;
        private int idLlogDebi;
        private int idLlogKredi;
        private bool aktivizimi;
        private int tipi;
        private bool lloji;
        private int model;
        private DateTime data;
        private string llogDebi;
        private string llogKredi;
        private string grupKomponente;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private bool aplikoPageMuaji;
        private bool aplikoDiteMuaji;
        private bool shfaqDefault;
        private bool lejoModVlere;
        private int idGrupKomponente;
        private bool llogaritGjithmone;
        private int idGrupNivel1;
        private int idGrupNivel2;
        private int idRenditje;
        private string Shenime;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKomponentePage()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idKomponentePage">id e komponentes</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="njesi">njesi tabele, numer, formule</param>
        /// <param name="kodParam">kodi i parametrit shtese</param>
        /// <param name="emerParam"> emri i parametrit shtese</param>
        /// <param name="formula">formula e llogaritjes se komponentes</param>
        /// <param name="idLlogDebi"> id e llogarise debi</param>
        /// <param name="idLlogKredi"> id e llogarise kredi</param>
        /// <param name="aktivizimi">aktive apo inaktive</param>
        /// <param name="tipi"> tipi  pagese, ndalese , llogaritese</param>
        /// <param name="lloji"> lloji komponente e pages apo listpagese</param>
        /// <param name="model"> modeli default, te ndermarjes qe nuk fshihen, te krijuara nga perdoruesi</param>
        /// <param name="data"> data e aktivizimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsKomponentePage(int idKomponentePage, string kodi, string pershkrimi, int njesi, string kodParam, string emerParam, string formula, int idLlogDebi, int idLlogKredi, bool aktivizimi, int tipi, bool lloji, int model, DateTime data, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, int njesiParam, bool aplikopagemuaji, bool aplikoditemuaji, bool shfaqdefault, int idgrupkomponente, bool lejomodvlere, bool llogaritGjithmone, int idGrupNivel1, int idGrupNivel2, int idrenditje,string Shenime)
        {
            IdKomponentePage = idKomponentePage;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.njesi = njesi;
            this.kodParam = kodParam;
            this.emerParam = emerParam;
            this.formula = formula;
            this.idLlogDebi = idLlogDebi;
            this.idLlogKredi = idLlogKredi;
            this.aktivizimi = aktivizimi;
            this.tipi = tipi;
            this.lloji = lloji;
            this.model = model;
            this.data = data;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idKonfig;
            this.idStatusDok = idStatusDok;
            this.njesiParam = njesiParam;
            this.aplikoDiteMuaji = aplikoditemuaji;
            this.aplikoPageMuaji = aplikopagemuaji;
            this.shfaqDefault = shfaqdefault;
            this.lejoModVlere = lejomodvlere;
            this.idGrupKomponente = idgrupkomponente;
            this.llogaritGjithmone = llogaritGjithmone;
            this.idGrupNivel1 = idGrupNivel1;
            this.idGrupNivel2 = idGrupNivel2;
            this.idRenditje = idrenditje;
            this.Shenime = Shenime;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i komponentes</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsKomponentePage(string kodi, int idNderm)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                if (!db.ktheKomponentePageSipasKodit(kodi, idNderm, this))
                    idKomponentePage = -1;
            }
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="kodi">kodi i komponentes</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="data"> data</param>
        public clsKomponentePage(string kodi, int idNderm, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKomponentePageSipasKoditDheDates(kodi, idNderm, data, this);

            }
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idKomponente">id e komponentes</param>
        public clsKomponentePage(int idKomponente)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKomponentePage(idKomponente, this);
            }
        }
        public static clsKomponentePage MerrSipasIdNgaStaticCache(int idKomponente)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return StaticCache.GetKomponentePage(idKomponente, db);
            }
        }

        public clsKomponentePage(DataRow rreshti)
        {
            
           // mbushKomponente(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// kthen /vendos apliko ditet e muajit kur ka ndodhur
        /// </summary>
        public bool AplikoDiteMuaji
        {
            get
            {
                return aplikoDiteMuaji;
            }
            set
            {
                aplikoDiteMuaji = value;
            }
        }
        /// <summary>
        /// kthen/vendos aplikon pagen e muajit kur ka ndodhur
        /// </summary>
        public bool AplikoPageMuaji
        {
            get
            {
                return aplikoPageMuaji;
            }
            set
            {
                aplikoPageMuaji = value;
            }
        }

        /// <summary>
        /// kthen vendos id e grupit te komponentes
        /// </summary>
        public int IdGrupKomponente
        {
            get
            {
                return idGrupKomponente;
            }
            set
            {
                idGrupKomponente = value;
            }
        }
        /// <summary>
        /// grupimet e nivelit te pare sipas vodafonit
        /// </summary>
        public int IdGrupNivel1
        {
            get
            {
                return idGrupNivel1;
            }
            set
            {
                idGrupNivel1 = value;
            }
        }
        //grupimet e nivelit te dyte sipas vodafonit
        public int IdGrupNivel2
        {
            get
            {
                return idGrupNivel2;
            }
            set
            {
                idGrupNivel2 = value;
            }
        }
        /// <summary>
        /// id e komponentes te pages
        /// </summary>
        public int IdKomponentePage
        {
            get
            {
                return
                    idKomponentePage;
            }
            set
            {
                idKomponentePage = value;
            }
        }
        public int IdRenditje
        {
            get
            {
                return idRenditje;
            }
            set
            {
                idRenditje = value;
            }
        }
        /// <summary>
        /// kodi i komponentes
        /// </summary>
        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }
        /// <summary>
        /// kthen vendos nqs vlera e kesaj formule mund te modifikohet
        /// 
        /// </summary>
        public bool LejoModVlere
        {
            get
            {
                return lejoModVlere;
            }
            set
            {
                lejoModVlere = value;
            }
        }
        public bool LlogaritGjithmone
        {
            get
            {
                return llogaritGjithmone;
            }
            set
            {
                llogaritGjithmone = value;
            }
        }
        /// <summary>
        /// kthen vendos njesi e artikullit 0 nr 1 kohe
        /// </summary>
        public int ParamNjesi
        {
            get
            {
                return njesiParam;
            }
            set
            {
                njesiParam = value;
            }
        }
        /// <summary>
        /// pershkrimi i komponentes
        /// </summary>
        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }
        /// <summary>
        /// njesia e komponentes
        /// <example> 0 -nr, 1-tabele, 2-formule</example>
        /// </summary>
        public int Njesi
        {
            get
            {
                return njesi;
            }
            set
            {
                njesi = value;
            }
        }
        /// <summary>
        /// kodi i parametrit
        /// </summary>
        public string ParamKodi
        {
            get
            {
                return kodParam;
            }
            set
            {
                kodParam = value;
            }
        }
        /// <summary>
        /// emri i parametrit
        /// </summary>
        public string ParamEmri
        {
            get
            {
                return emerParam;
            }
            set
            {
                emerParam = value;
            }
        }
        /// <summary>
        /// formula e llogaritjes
        /// </summary>
        public string Formula
        {
            get
            {
                return formula;
            }
            set
            {
                formula = value;
            }
        }
        /// <summary>
        /// id e llogarise debi
        /// </summary>
        public int IdLlogDebi
        {
            get
            {
                return idLlogDebi;
            }
            set
            {
                idLlogDebi = value;
            }
        }
        /// <summary>
        /// id e llogarise kredi
        /// </summary>
        public int IdLlogKredi
        {
            get
            {
                return idLlogKredi;
            }
            set
            {
                idLlogKredi = value;
            }
        }
        /// <summary>
        /// aktive ose inaktive
        /// </summary>
        public bool Aktivizimi
        {
            get
            {
                return aktivizimi;
            }
            set
            {
                aktivizimi = value;
            }
        }
        /// <summary>
        /// kthen vendos nqs kjo komponente do dale e selektuar tek punonjesi
        /// </summary>
        public bool ShfaqDefault
        {
            get
            {
                return shfaqDefault;
            }
            set
            {
                shfaqDefault = value;
            }
        }
        /// <summary>
        /// tipi i komponentes
        /// <example> 1-pagese,2-ndalese,3 -llogaritese</example>
        /// </summary>
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
        /// lloji false-komponente page true-listpagese
        /// </summary>
        public bool Lloji
        {
            get
            {
                return lloji;
            }
            set
            {
                lloji = value;
            }
        }
        /// <summary>
        /// modeli
        /// <example> 0- modelet e sistemit qe perdoren per ndermarjet e reja 1- modelet e ndermarjes qe nuk fshihen 2- modelet e krijuar nga perdoruesi</example>
        /// </summary>
        public int Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        /// <summary>
        /// data e aktivizimit
        /// </summary>
        public DateTime Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        /// <summary>
        /// nr e llogarise debi
        /// </summary>
        public string LlogDebi
        {
            get
            {
                return llogDebi;
            }

        }
        /// <summary>
        /// nr i llogarise kredi
        /// </summary>
        public string LlogKredi
        {
            get
            {
                return llogKredi;
            }

        }
        /// <summary>
        /// id e perdoruesit qe ka kryer veprimin
        /// </summary>
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
        /// <summary>
        /// id e ndermarjes 
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }
        /// <summary>
        /// id e konfigurimit te dokumentit
        /// </summary>
        public int IdKonfig
        {
            get
            {
                return idKonfig;
            }
            set
            {
                idKonfig = value;
            }
        }
        /// <summary>
        /// id e status te dok
        /// <example> 0 draft, 1-ruajtur,2 -fshire</example>
        /// </summary>
        public int IdStatusDok
        {
            get
            {
                return idStatusDok;
            }
            set
            {
                idStatusDok = value;
            }
        }
        /// <summary>
        /// data e krijimit te kompoentes
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
        }
        /// <summary>
        /// data e modifikimi te fundit te komponentes
        /// </summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
        }

        public string shenime
        {
            get
            {
                return Shenime;
            }
            set
            {
                Shenime = value;
            }
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// kopjon vetveten
        /// </summary>
        /// <returns></returns>
        public clsKomponentePage Clone()
        {
            return new clsKomponentePage(idKomponentePage, kodi, pershkrimi, njesi, kodParam, emerParam, formula, idLlogDebi, idLlogKredi, aktivizimi, tipi, lloji, model, data, idPerdoruesi, idNdermarje, idKonfig, idStatusDok, njesiParam, aplikoPageMuaji, aplikoDiteMuaji, shfaqDefault, idGrupKomponente, lejoModVlere, llogaritGjithmone, idGrupNivel1, idGrupNivel2, idRenditje, Shenime);
        }

        /// <summary>
        /// ruan Komponente page
        /// </summary>
        /// <param name="db"> clsDatabaseListPagese per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo komponentja</returns>
        public clsMesazh ruaj(clsDatabazeListPagesa db)
        {
            clsMesazh mesazh = new clsMesazh();
            int idkomponente = 0;
            mesazh = db.ruajKomponentePage(out idkomponente, kodi, pershkrimi, njesi, kodParam, emerParam, formula, idLlogDebi, idLlogKredi, aktivizimi, tipi, lloji, model, data, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, njesiParam, aplikoPageMuaji, aplikoDiteMuaji, shfaqDefault, idGrupKomponente, lejoModVlere, llogaritGjithmone,idGrupNivel1,idGrupNivel2, idRenditje,Shenime);
            idKomponentePage = idkomponente;
            return mesazh;
        }

        /// <summary>
        /// ruan Komponente default sipas llojit
        /// fshin te vjetrat nqs eksistojne ne ate date dhe ruan te rejat
        /// </summary>
        /// <param name="data"> data e re</param>
        /// <param name="idndermarje"> ndermarja</param>
        /// <param name="idperdoruesi">perdoruesi</param>
        /// <param name="lloji">lloji</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo komponentja</returns>
        public static clsMesazh ruajDefault(DateTime data, bool lloji, int idndermarje, int idperdoruesi, int idndermnga)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa(); ;
            //db.krijoManager();
            db.beginTransaksion();
            if (db.ekzistonDateKomponentePage(data, lloji, idndermarje))
            {
                colKomponentePage col = new colKomponentePage(idndermarje, lloji, data);
                foreach (clsKomponentePage komp in col)
                {
                    komp.idPerdoruesi = idperdoruesi;
                    mesazh = komp.fshi(db);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }
                }
            }
            mesazh = db.ruajDefaultKomponentePage(lloji, data, idperdoruesi, idndermarje, idndermnga);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            mesazh = db.modifikoLegjendeListOrariSipasKomponenteveTeReja(idndermarje);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            if (mesazh.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return mesazh;
        }

        /// <summary>
        /// ruan Komponente  ne daten e re 
        /// fshin te vjetrat nqs eksistojne ne ate date dhe ruan te rejat
        /// </summary>
        /// <param name="data"> data e re</param>
        /// <param name="idndermarje"> ndermarja</param>
        /// <param name="idperdoruesi">perdoruesi</param>
        /// <param name="lloji">lloji</param>
        /// <param name="dt">data table me komponentet e reja</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo komponentja</returns>
        public clsMesazh ruajKomp(DataTable dt, DateTime data, bool lloji, int idndermarje, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            //db.krijoManager();
            db.beginTransaksion();
            if (db.ekzistonDateKomponentePage(data, lloji, idndermarje))
            {
                colKomponentePage col = new colKomponentePage(idndermarje, lloji, data);
                foreach (clsKomponentePage komp in col)
                {
                    komp.idPerdoruesi = idperdoruesi;
                    mesazh = komp.fshi(db);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                mbushKomponenteNgaDT(dr);
                this.data = data;
                idPerdoruesi = idperdoruesi;
                mesazh = ruaj(db);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
            }
            mesazh = db.modifikoLegjendeListOrariSipasKomponenteveTeReja(idndermarje);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            db.commitTransaksion();
            return mesazh;
        }

        /// <summary>
        /// modifikon Komponente page
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo komponentja</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.modifikoKomponentePage(idKomponentePage, kodi, pershkrimi, njesi, kodParam, emerParam, formula, idLlogDebi, idLlogKredi, aktivizimi, tipi, lloji, model, data, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, njesiParam, aplikoPageMuaji, aplikoDiteMuaji, shfaqDefault, idGrupKomponente, lejoModVlere, llogaritGjithmone,Shenime);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin Komponente page ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {

            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh u_fshi = fshi(db);
            db.Dispose();
            return u_fshi;
        }
        public clsMesazh fshi(clsDatabazeListPagesa db)
        {

            clsMesazh u_fshi = db.fshiKomponentePageStatus(idKomponentePage, idPerdoruesi);

            return u_fshi;
        }

        /// <summary>
        /// Merr objektin Komponente page nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        public void merr()
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKomponentePage(idKomponentePage, this);
            }
        }

        /// <summary>
        /// merr objektin komponente page sipas kodi dhe ndermarjes
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        public static void merrKomponenteSipasKodit(string kodi, int idndermarje)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKomponentePageSipasKodit(kodi, idndermarje, null);
            }
        }

      
        /// <summary>
        /// kontrollon nese ekziston komponentja me kete kod ne kete ndermarje ne kete date
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="data">data</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonKomponente(string kodi, int idndermarje, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool ekziston = db.ekzistonKomponentePage(kodi, idndermarje, data);
                return ekziston;
            }
        }

        /// <summary>
        /// kontrollon nese ekziston komponentja me kete kod ne kete ndermarje
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonKomponente(string kodi, int idndermarje, bool lloji)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool ekziston = db.ekzistonKomponentePage(kodi, idndermarje, lloji);
                return ekziston;
            }
        }
        public static bool ekzistonKomponenteParametri(string param, int idndermarje, string kodi, bool lloji)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool ekziston = db.ekzistonKomponentePageParametri(param, idndermarje, kodi, lloji);
                return ekziston;
            }
        }
        /// <summary>
        /// kontrollon nese ekziston komponentja me kete kod ne kete ndermarje
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonKomponenteNeFormule(string kodi, int idndermarje)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool ekziston = db.ekzistonKomponentePageNeFormule(kodi, idndermarje);
                return ekziston;
            }
        }
        
        public static clsKomponentePage Krijo(IDataRecord record)
        {
            clsKomponentePage kompPage = new clsKomponentePage();
            kompPage.mbushKomponente(record);
            return kompPage;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush komponenten me te dhenat nga databaza
        /// </summary>
        /// <param name="record">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal void mbushKomponente(IDataRecord record)
        {
            
            try
            {
                Converter.ParseExact(record["IDKOMPONENTEPAGE"].ToString(), out idKomponentePage, "idKomponentePage");
                kodi = record["KODI"].ToString();
                pershkrimi = record["PERSHKRIMI"].ToString();
                Converter.Parse(record["NJESI"].ToString(), out njesi, "njesi");
                kodParam = record["PARAMKodi"].ToString();
                emerParam = record["PARAMEMRI"].ToString();
                formula = record["FORMULA"].ToString();
                Converter.Parse(record["DATA"].ToString(), out data, "data");
                Converter.Parse(record["AKTIVIZIMI"].ToString(), out aktivizimi, "aktivizimi");
                Converter.Parse(record["IDLLOGDEBI"].ToString(), out idLlogDebi, "idLlogDebi");
                Converter.Parse(record["IDLLOGKREDI"].ToString(), out idLlogKredi, "idLlogKredi");
                Converter.Parse(record["PARAMNJESI"].ToString(), out njesiParam, "njesiParam");
                llogDebi = record["LLOGDEBI"].ToString();
                llogKredi = record["LLOGKREDI"].ToString();

                Converter.Parse(record["IDPERDORUESI"].ToString(), out idPerdoruesi, "idPerdoruesi");
                Converter.Parse(record["TIPI"].ToString(), out tipi, "tipi");
                Converter.Parse(record["MODEL"].ToString(), out model, "model");
                Converter.Parse(record["IDNDERMARJE"].ToString(), out idNdermarje, "idNdermarje");
                Converter.Parse(record["LLOJI"].ToString(), out lloji, "lloji");

                Converter.Parse(record["IDKONFIG"].ToString(), out idKonfig, "idKonfig");

                Converter.Parse(record["IDSTATUSDOK"].ToString(), out idStatusDok, "idStatusDok");
                Converter.Parse(record["APLIKOPAGEMUAJI"].ToString(), out aplikoPageMuaji, "aplikoPageMuaji");
                Converter.Parse(record["APLIKODITEMUAJI"].ToString(), out aplikoDiteMuaji, "aplikoDiteMuaji");
                Converter.Parse(record["SHFAQDEFAULT"].ToString(), out shfaqDefault, "shfaqDefault");
                Converter.Parse(record["LEJOMODVLERE"].ToString(), out lejoModVlere, "lejoModVlere");
                Converter.Parse(record["LLOGARITGJITHMONE"].ToString(), out llogaritGjithmone, "llogaritGjithmone");
                Converter.Parse(record["IDGRUPKOMPONENTE"].ToString(), out idGrupKomponente, "idGrupKomponente");
                Converter.Parse(record["DTKRIJIMI"].ToString(), out dtKrijimi, "dtKrijimi");
                Converter.Parse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi, "dtModifikimi");
                Converter.Parse(record["IDGRUPNIVEL1"].ToString(), out idGrupNivel1, "idGrupNivel1");
                Converter.Parse(record["IDGRUPNIVEL2"].ToString(), out idGrupNivel2, "idGrupNivel2");
                Converter.Parse(record["IDRENDITJE"].ToString(), out idRenditje, "idRenditje");
                Shenime = record["Shenime"].ToString();
                    
            }
            catch (MyWarnException warn)
            {
                logu.Warn("gabim ne mbushjen e komponenteve te pages per:{0} {1}", idKomponentePage, warn.Message);
            }
            catch (MyException myex)
            {
                throw new MyException(logu, "gabim ne mbushjen e komponenteve te pages per :{0} {1}", idKomponentePage, myex.Message);
            }


        }

        /// <summary>
        /// mbush komponenten kur te dhenat vijne nga dt e grides
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns>kthen nese mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushKomponenteNgaDT(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IdKomponentePage"].ToString(), out idKomponentePage);
                    kodi = dbDataRow["Kodi"].ToString();
                    pershkrimi = dbDataRow["Pershkrimi"].ToString();
                    int.TryParse(dbDataRow["Njesi"].ToString(), out njesi);
                    kodParam = dbDataRow["ParamKodi"].ToString();
                    emerParam = dbDataRow["ParamEmri"].ToString();
                    formula = dbDataRow["Formula"].ToString();
                    DateTime.TryParse(dbDataRow["Data"].ToString(), out data);
                    Boolean.TryParse(dbDataRow["Aktivizimi"].ToString(), out aktivizimi);
                    int.TryParse(dbDataRow["IdLlogDebi"].ToString(), out idLlogDebi);
                    int.TryParse(dbDataRow["IdLlogKredi"].ToString(), out idLlogKredi);
                    llogDebi = dbDataRow["LlogDebi"].ToString();
                    llogKredi = dbDataRow["LlogKredi"].ToString();
                    int.TryParse(dbDataRow["ParamNjesi"].ToString(), out njesiParam);
                    int.TryParse(dbDataRow["IdPerdoruesi"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["Tipi"].ToString(), out tipi);
                    int.TryParse(dbDataRow["Model"].ToString(), out model);
                    int.TryParse(dbDataRow["IdNdermarje"].ToString(), out idNdermarje);
                    bool.TryParse(dbDataRow["Lloji"].ToString(), out lloji);

                    int.TryParse(dbDataRow["IdKonfig"].ToString(), out idKonfig);
                    bool.TryParse(dbDataRow["AplikoPageMuaji"].ToString(), out aplikoPageMuaji);
                    bool.TryParse(dbDataRow["AplikoDiteMuaji"].ToString(), out aplikoDiteMuaji);
                    bool.TryParse(dbDataRow["ShfaqDefault"].ToString(), out shfaqDefault);
                    bool.TryParse(dbDataRow["LejoModVlere"].ToString(), out lejoModVlere);   
                    bool.TryParse(dbDataRow["LlogaritGjithmone"].ToString(), out llogaritGjithmone);
                    grupKomponente = dbDataRow["GrupKomponente"].ToString();
                    int.TryParse(dbDataRow["IdGrupKomponente"].ToString(), out idGrupKomponente);
                    int.TryParse(dbDataRow["IdStatusDok"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DtKrijimi"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DtModifikimi"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRow["IdGrupNivel1"].ToString(), out idGrupNivel1);
                    int.TryParse(dbDataRow["IdGrupNivel2"].ToString(), out idGrupNivel2);
                    int.TryParse(dbDataRow["IdRenditje"].ToString(), out idRenditje);
                    Shenime = dbDataRow["Shenime"].ToString();
                    return new clsMesazh(true, clsKomponentePage.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsKomponentePage.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsKomponentePage.drbosh);
        }

       

        #endregion

       
    }
}
