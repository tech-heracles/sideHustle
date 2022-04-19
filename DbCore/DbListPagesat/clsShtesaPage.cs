using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using EO.Web.Internal;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne shtesat e pages
    ///  (Te dhenat  merren nga tabela : T_SHTESAPAGE)
    /// </summary>
    public class clsShtesaPage
    {
        public static string mbushjeSukses = "Shtesaa u mbush me sukses";
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se shtesave te pages nga db-ja";
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        private const string gabimEkzistimi = "ekziston nje shtesa page ne kete kod. Ju lutem zgjidhni nje kod tjeter!";
        #region Atribute

        private int idShtesaPage;
        private string kodi;
        private string klasa;
        private decimal vlera;
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
        public clsShtesaPage()
        {
        }

        /// <summary>
        /// kontruktori me parametra
        /// </summary>
        /// <param name="idshtesapage"> id e shtesase se pages</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="klasa">klasa</param>
        /// <param name="vlera">vlera  </param>
        /// <param name="tipi"> tipi i pageses 1-funksion perqindje, 2-funksion vlere,3-pozicioni, 4-kualifikimi, 5-veshtiresia, 6-vjeteria</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka bere veprimin</param>
        /// <param name="idNdermarje"> id e ndermarjes </param>
        /// <param name="idStatusDok"> id e status dok 0-draft,1-ruajtur 2-fshire</param>
        public clsShtesaPage(int idshtesapage, string kodi, string klasa, decimal vlera, int tipi, int idPerdoruesi, int idNdermarje, int idStatusDok)
        {
            this.idShtesaPage = idshtesapage;
            this.kodi = kodi;
            this.klasa = klasa;
            this.vlera = vlera;

            this.tipi = tipi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="kodi">kodi i shtesase</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="tipi">tipi i pages</param>
        public clsShtesaPage(string kodi, int idNderm, int tipi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (!mbushShtesa(db.merrShtesaPageSipasKodit(kodi, idNderm, tipi)).Status)
                    idShtesaPage = -1;
            db.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idshtesapage">id sigurime</param>
        public clsShtesaPage(int idshtesapage)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushShtesa(db.merrShtesaPage(idshtesapage));
            db.Dispose();
        }

        public clsShtesaPage(DataRow rreshti)
        {
            
            mbushShtesa(rreshti);
        }
        #endregion

        #region Properties

        /// <summary>
        /// id e shtesa page
        /// </summary>
        public int IdShtesaPage
        {
            get
            {
                return idShtesaPage;
            }
            set
            {
                idShtesaPage = value;
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
        /// vlera 
        /// </summary>
        public decimal Vlera
        {
            get
            {
                return vlera;
            }
            set
            {
                vlera = value;
            }
        }

        /// <summary>
        /// tipi
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
        /// klasa
        /// </summary>
        public string Klasa
        {
            get
            {
                return klasa;
            }
            set
            {
                klasa = value;
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
        /// ruan shtesa e pagese
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo shtesaa </returns>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh mesazh = new clsMesazh();
            int idshtesa = 0;
            if (db.ekzistonShtesaPage(kodi, idNdermarje, tipi))
                return new clsMesazh(false, gabimEkzistimi);
            mesazh = db.ruajShtesaPage(out idshtesa, kodi, klasa, tipi, vlera, idPerdoruesi, idNdermarje, idStatusDok);
            idShtesaPage = idshtesa;
            db.Dispose();
            return mesazh;
        }



        /// <summary>
        /// modifikon shtesane
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo shtesaa</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.modifikoShtesaPage(idShtesaPage, kodi, klasa, tipi, vlera, idPerdoruesi, idNdermarje, idStatusDok);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh u_fshi = db.fshiShtesa(idShtesaPage, idPerdoruesi);
            db.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin shtesa nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        public void merr()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.merrShtesaPage(idShtesaPage);
            db.Dispose();
        }


        /// <summary>
        /// Merr datatable shtesa  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe shtesate e kesaj ndermarje sipas tipit te pages</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.ktheGjitheShtesaPageSipasNdermarrjesDheTipit(idNdermarje, tipi);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// kontrollon nese ekziston shtesaa me kete kod ne kete ndermarje per ketge tip
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="tipi">tipi</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonShtesaPage(string kodi, int idndermarje, int tipi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.ekzistonShtesaPage(kodi, idndermarje, tipi);
            db.Dispose();
            return ekziston;            
        }
        /// <summary>
        /// kontrollon ka veprime me kete shtesa
        /// </summary>

        /// <param name="idshtesa">idshtesa</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool kaVeprime(int idshtesa, int tipi,int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool kaVeprime = db.kaVeprimeShtesaPage(idshtesa, tipi, idndermarje);
            db.Dispose();
            return kaVeprime;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush shtesa me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushShtesa(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDSHTESAPAGE"].ToString(), out idShtesaPage);
                    kodi = dbDataRow["KODI"].ToString();
                    decimal.TryParse(dbDataRow["VLERA"].ToString(), out vlera);
                    klasa = dbDataRow["KLASA"].ToString();
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["TIPI"].ToString(), out tipi);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsShtesaPage.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsShtesaPage.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsShtesaPage.drbosh);
        }


        #endregion
    }
}

