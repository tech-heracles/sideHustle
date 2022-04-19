using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbKontabiliteti;

namespace DbCore.DbTollona
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje artikull per tollona
    ///  (Te dhenat  merren nga tabela : T_Artikulli tek dbtollonat)
    /// </summary>
    public class clsArtikullTolloni
    {

        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Artikulli u mbush me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se artikullit nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merret asnje e dhene
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e artikullit
        /// </summary>
        private int idArtikulli;
        /// <summary>
        /// kodi
        /// </summary>
        private string kodi;
        /// <summary>
        /// emertimi
        /// </summary>
        private string pershkrimi;

        /// <summary>
        /// id e artikullit ne alphaweb
        /// </summary>
        private int idArtWebinf;
        /// <summary>
        /// aktiv
        /// </summary>
        private bool aktiv;

        /// <summary>
        /// id e statusit te burimit
        /// </summary>
        private int idStatusDok;
        /// <summary>
        /// data e krijimi te burimit
        /// </summary>
        private DateTime dtKrijimi;
        /// <summary>
        /// data e fundit e modifikimit
        /// </summary>
        private DateTime dtModifikimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsArtikullTolloni()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idartikulli">id e burimit</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="kostoPlan">kostoja e planifikuar</param>
        /// <param name="idartwebinf"> id e llogarise </param>
        /// <param name="aktiv">aktive apo inaktive</param>
        /// <param name="tipi"> tipi  makineri,mjet apo punonjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsArtikullTolloni(int idartikulli, string kodi, string pershkrimi, int idartwebinf, bool aktiv, int idStatusDok)
        {
            idArtikulli = idartikulli;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;

            idArtWebinf = idartwebinf;
            this.aktiv = aktiv;

            this.idStatusDok = idStatusDok;
        }

        public clsArtikullTolloni(DataRow rreshti)
        {
            
            mbushArtikull(rreshti);
        }



        ///// <summary>
        ///// konstruktor me 2 parametra
        ///// </summary>
        ///// <param name="kodi">kodi i burimit</param>
        ///// <param name="idNderm">id e ndermarrjes</param>
        //public clsArtikullTolloni(string kodi, int idNderm)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    if (!mbushArtikull(db.ktheBurimSipasKodit(kodi, idNderm)).Status)
        //        idArtikulli = -1;
        //    db.Dispose();
        //}
        //public clsArtikullTolloni(string kodi, int idNderm, clsDatabazeTollona db)
        //{
        //    if (!mbushArtikull(db.ktheBurimSipasKodit(kodi, idNderm)).Status)
        //        idArtikulli = -1;

        //}

        ///// <summary>
        ///// konstruktor me 1 parameter
        ///// </summary>
        ///// <param name="idburim">id e burimit</param>
        //public clsArtikullTolloni(int idburim)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    mbushArtikull(db.ktheBurim(idburim));
        //    db.Dispose();
        //}
        //public clsArtikullTolloni(int idburim, clsDatabazeTollona db)
        //{
        //    mbushArtikull(db.ktheBurim(idburim));

        //}
        #endregion

        #region Properties
        /// <summary>
        /// id e artikullit
        /// </summary>
        public int IdArtikulli
        {
            get
            {
                return
                    idArtikulli;
            }
            set
            {
                idArtikulli = value;
            }
        }
        /// <summary>
        /// kodi i artikullit
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
        /// emertimi
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
        /// id e artikullit tek webinf 
        /// </summary>
        public int IdArtWebinf
        {
            get { return idArtWebinf; }
            set { idArtWebinf = value; }
        }
        /// <summary>
        /// aktive ose inaktive
        /// </summary>
        public bool Aktiv
        {
            get
            {
                return aktiv;
            }
            set
            {
                aktiv = value;
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
        /// data e krijimit te artikullit
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
        }
        /// <summary>
        /// data e modifikimi te fundit te artikullit
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



        public clsMesazh ruaj()
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return ruaj(db);                
            }
        }

        /// <summary>
        /// ruan burimin
        /// </summary>
        /// <param name="db"> clsDatabazeTollona per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo burimi</returns>
        public clsMesazh ruaj(clsDatabazeTollona db)
        {
            clsMesazh mesazh = new clsMesazh();
            //if (db == null)
            //    db = new clsDatabazeTollona();
            int idart = 0;
            mesazh = db.ruajArtTollon(out idart, kodi, pershkrimi, idArtWebinf, aktiv,  idStatusDok,"",1);
            idArtikulli = idart;
            return mesazh;
        }

        ///// <summary>
        ///// modifikon burimin
        ///// </summary>
        ///// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo burimi</returns>   
        //public clsMesazh modifiko()
        //{
        //    clsMesazh mesazh = new clsMesazh();
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    mesazh = db.modifikoBurim(idArtikulli, kodi, pershkrimi, tipi, kostoPlan, idArtWebinf, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
        //    db.Dispose();
        //    return mesazh;
        //}
        //public clsMesazh modifiko(clsDatabazeTollona db)
        //{
        //    clsMesazh mesazh = new clsMesazh();

        //    mesazh = db.modifikoBurim(idArtikulli, kodi, pershkrimi, tipi, kostoPlan, idArtWebinf, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);

        //    return mesazh;
        //}

        //public clsMesazh fshi()
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    clsMesazh mesazh = fshi(db);
        //    db.Dispose();
        //    return mesazh;
        //}

        ///// <summary>
        ///// Fshin objektin burimin ne tabelen perkatese ne databaze
        ///// </summary>
        ///// <param name="db">db nqs ben pjese ne nje transaksion</param>
        ///// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        //public clsMesazh fshi(clsDatabazeTollona db)
        //{
        //    //if (db == null)
        //    //    db = new clsDatabazeTollona();
        //    clsMesazh u_fshi = db.fshiBurimStatus(idArtikulli, idPerdoruesi);
        //    return u_fshi;
        //}

        ///// <summary>
        ///// Merr objektin burim nga tabela perkatese ne databaze.
        ///// </summary>
        //public void merr()
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    db.ktheBurim(idArtikulli);
        //    db.Dispose();
        //}

        ///// <summary>
        ///// merr objektin burimin sipas kodi dhe ndermarjes
        ///// </summary>
        ///// <param name="kodi">kodi </param>
        ///// <param name="idndermarje">ndermarja</param>
        //public static void merrBurimSipasKodit(string kodi, int idndermarje)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    db.ktheBurimSipasKodit(kodi, idndermarje);
        //    db.Dispose();
        //}

        ///// <summary>
        ///// Merr datatable burim  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        ///// </summary>
        ///// <returns > nje datatable me te gjithe burimet te kesaj ndermarje</returns>
        //public DataTable merriTeGjithe()
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    DataTable dt = db.ktheGjitheBurimetSipasNdermarjes(IdNdermarje);
        //    db.Dispose();
        //    return dt;
        //}

        ///// <summary>
        ///// kontrollon nese ekziston burimi me kete kod ne kete ndermarje 
        ///// </summary>
        ///// <param name="kodi">kodi</param>
        ///// <param name="idndermarje">idndermarje</param>
        ///// <returns> true ose false</returns>
        //public static bool ekzistonBurim(string kodi, int idndermarje)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    bool ekziston = db.ekzistonBurim(kodi, idndermarje);
        //    db.Dispose();
        //    return ekziston;
        //}

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushArtikull(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out idArtikulli);
                    kodi = dbDataRow["KODARTIKULLI"].ToString();
                    pershkrimi = dbDataRow["PERSHKRIMARTIKULLI"].ToString();
                    Boolean.TryParse(dbDataRow["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRow["IDARTWEBINF"].ToString(), out idArtWebinf);
                   
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsArtikullTolloni.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsArtikullTolloni.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsArtikullTolloni.drbosh);
        }

        #endregion
    }
}
