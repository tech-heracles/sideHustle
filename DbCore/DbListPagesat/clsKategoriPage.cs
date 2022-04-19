using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using EO.Web.Internal;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kategorite e pages
    ///  (Te dhenat  merren nga tabela : T_KATEGORIPAGE)
    /// </summary>
    public class clsKategoriPage
    {
        public static string mbushjeSukses = "Kategoria u mbush me sukses";
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se kategorive te pages nga db-ja";
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        private const string gabimEkzistence = "Ekziston nje kategori page ne kete kod. Ju lutem zgjidhni nje kod tjeter!";
        #region Atribute

        private int idKategoriPage;
        private string kodi;
        private string pershkrimi;
        private decimal paga;
        private int tipi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKategoriPage()
        {
        }

        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="idkategoripage"> id e kategorise se pages</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="paga">paga  </param>
        /// <param name="total">totali </param>
        /// <param name="tipi"> tipi i pageses 1-mujore, 2-ditore,3-orare</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka bere veprimin</param>
        /// <param name="idNdermarje"> id e ndermarjes </param>
         /// <param name="idStatusDok"> id e status dok 0-draft,1-ruajtur 2-fshire</param>
        public clsKategoriPage(int idkategoripage, string kodi, string pershkrimi, decimal paga,  int tipi, int idPerdoruesi, int idNdermarje,  int idStatusDok)
        {
            this.idKategoriPage = idkategoripage;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.paga = paga;
         
            this.tipi = tipi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="kodi">kodi i kategorise</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="tipi">tipi i pages</param>
        public clsKategoriPage(string kodi, int idNderm, int tipi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();                      
            if (!mbushKategori(db.merrKagetoriPageSipasKodit(kodi, idNderm, tipi)).Status)
                    idKategoriPage = -1;
            db.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idkategoripage">id sigurime</param>
        public clsKategoriPage(int idkategoripage)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushKategori(db.merrKagetoriPage(idkategoripage));
            db.Dispose();
        }

        public clsKategoriPage(DataRow rreshti)
        {
            
            mbushKategori(rreshti);
        }
        #endregion

        #region Properties
        
        /// <summary>
        /// id e kategori page
        /// </summary>
        public int IdKategoriPage
        {
            get
            {
                return idKategoriPage;
            }
            set
            {
                idKategoriPage = value;
            }
        }
        /// <summary>
        /// kodi 
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
        /// paga 
        /// </summary>
        public decimal Paga
        {
            get
            {
                return paga;
            }
            set
            {
                paga = value;
            }
        }

        /// <summary>
        /// tipi
        /// <example> 1-mujore, 2-ditore,3-orare</example>
        /// <see cref="cs"/>
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
        /// pershkrimi
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
        #endregion

        #region Metoda Publike
        /// <summary>
        /// ruan kategori e pagese
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo kategoria </returns>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh mesazh = new clsMesazh();
            int idkategori = 0;
            if (db.ekzistonKategoriPage(kodi, idNdermarje, tipi))
                return new clsMesazh(false, gabimEkzistence);
            mesazh = db.ruajKategoriPage(out idkategori, kodi, pershkrimi,tipi, paga,  idPerdoruesi, idNdermarje, idStatusDok);
            idKategoriPage = idkategori;
            db.Dispose();
            return mesazh;
        }

     

        /// <summary>
        /// modifikon kategorine
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo kategoria</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.modifikoKategoriPage(idKategoriPage, kodi, pershkrimi, tipi, paga, idPerdoruesi, idNdermarje, idStatusDok);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin kategori ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh u_fshi = db.fshiKategori(idKategoriPage, idPerdoruesi);
            db.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin kategori nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        public void merr()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.merrKagetoriPage(idKategoriPage);
            db.Dispose();
        }

        
        /// <summary>
        /// Merr datatable kategori  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe kategorite e kesaj ndermarje sipas tipit te pages</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.ktheGjitheKategoriPageSipasNdermarrjesDheTipit(idNdermarje, tipi);
            db.Dispose();
            return dt;            
        }

        /// <summary>
        /// kontrollon nese ekziston kategoria me kete kod ne kete ndermarje per ketge tip
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="tipi">tipi</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonKategoriPage(string kodi, int idndermarje, int tipi)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.ekzistonKategoriPage(kodi, idndermarje, tipi);
            }          
        } 
        
        /// <summary>
        /// kontrollon nese ekziston kategoria me kete kod ne kete ndermarje per ketge tip
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="tipi">tipi</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonKategoriPage(string kodi, int idndermarje)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.ekzistonKategoriPage(kodi, idndermarje);
            }
        }

        /// <summary>
        /// kontrollon ka veprime me kete kategori
        /// </summary>
        
        /// <param name="idkategori">idkategori</param>
        ///   /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool kaVeprime(int idkategori,int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.kaVeprimeKategoriPage(idkategori, idndermarje);
            db.Dispose();
            return ekziston;            
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kategori me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushKategori(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDKATEGORIPAGE"].ToString(), out idKategoriPage);
                    kodi = dbDataRow["KODI"].ToString();
                    decimal.TryParse(dbDataRow["PAGA"].ToString(), out paga);
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["TIPI"].ToString(), out tipi);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsKategoriPage.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsKategoriPage.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsKategoriPage.drbosh);
        }

    
        #endregion
    }
}

