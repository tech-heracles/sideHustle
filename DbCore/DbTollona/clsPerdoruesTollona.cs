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
    public class clsPerdoruesTolloni
    {

        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Perdoruesi u mbush me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se perdoruesit nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merret asnje e dhene
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdoruesi;
        /// <summary>
        /// emer
        /// </summary>
        private string emer;
        /// <summary>
        ///mbiemer
        /// </summary>
        private string mbiemer;

        /// <summary>
        /// username i perdoruesit
        /// </summary>
        private string username;
        /// <summary>
        /// passwordi i perdoruesit
        /// </summary>
        private string password;
        /// <summary>
        /// eshte admin apo jo
        /// </summary>
        private bool admin;
        /// <summary>
        /// password i perkoheshem
        /// </summary>
        private bool passPerkohshem;

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


        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsPerdoruesTolloni()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idperdoruesi">id e burimit</param>
        /// <param name="emri"> kodi</param>
        /// <param name="mbiemeri"> pershkrimi</param>
        /// <param name="kostoPlan">kostoja e planifikuar</param>
        /// <param name="idartwebinf"> id e llogarise </param>
        /// <param name="aktiv">aktive apo inaktive</param>
        /// <param name="tipi"> tipi  makineri,mjet apo punonjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsPerdoruesTolloni(int idperdoruesi, string emri, string mbiemeri, string username, string password, bool admin, bool passiperkoheshm, bool aktiv, int idStatusDok)
        {
            idPerdoruesi = idperdoruesi;
            this.emer = emri;
            this.mbiemer = mbiemeri;
            this.username = username;
            this.password = password;
            this.admin = admin;
            this.passPerkohshem = passiperkoheshm;
            this.aktiv = aktiv;

            this.idStatusDok = idStatusDok;
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
        /// id e perdoruesit
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return
                    idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }
        /// <summary>
        /// eshte admin apo jo
        /// </summary>
        public bool Admin
        {
            get
            {
                return admin;
            }
            set
            {
                admin = value;
            }
        }
        /// <summary>
        /// emer
        /// </summary>
        public string Emer
        {
            get
            {
                return emer;
            }
            set
            {
                emer = value;
            }
        }
        /// <summary>
        /// password i perkoheshem
        /// </summary>
        public bool PassPerkohshem
        {
            get
            {
                return passPerkohshem;
            }
            set
            {
                passPerkohshem = value;
            }
        }
        /// <summary>
        /// passwordi i perdoruesit
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        /// <summary>
        /// mbiemer
        /// </summary>
        public string Mbiemer
        {
            get
            {
                return mbiemer;
            }
            set
            {
                mbiemer = value;
            }
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
        /// <summary>
        /// username i perdoruesit
        /// </summary>
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
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
            int idper = 0;
            mesazh = db.ruajPerdorues(out idper, emer, mbiemer, username, password,admin, passPerkohshem, aktiv, idStatusDok);
            idPerdoruesi = idper;
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

        /// <summary>
        /// kontrollon nese ekziston burimi me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonPerdorues(string username)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.ekzistonPerdorues(username);
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushPerdorues(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    emer = dbDataRow["EMER"].ToString();
                    mbiemer = dbDataRow["MBIEMER"].ToString();
                    username = dbDataRow["USERNAME"].ToString();
                    password = dbDataRow["PASSWORD"].ToString();
                    Boolean.TryParse(dbDataRow["AKTIV"].ToString(), out aktiv);
                    Boolean.TryParse(dbDataRow["ADMIN"].ToString(), out admin);
                    Boolean.TryParse(dbDataRow["PASSPERKOHSHEM"].ToString(), out passPerkohshem);
       

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
