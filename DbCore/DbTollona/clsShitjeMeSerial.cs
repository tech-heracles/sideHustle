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
    public class clsShitjeMeSerial
    {

        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Shitja u mbush me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se shitjes nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merret asnje e dhene
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e ritese
        /// </summary>
        private int id;
        /// <summary>
        /// seriali
        /// </summary>
        private string seriali;
        /// <summary>
        /// id e artikullit
        /// </summary>
        private int idArtikulli;

        /// <summary>
        /// id e trupit te shitjes ne alphaweb
        /// </summary>
        private int idTrupiShitje;


        /// <summary>
        /// sasia e artikullit
        /// </summary>
        private double sasia;
        /// <summary>
        /// cmimi i artikullit
        /// </summary>
        private double cmimi;
        /// <summary>
        /// data e fillimit i vlefshme per tollonat
        /// </summary>
        private DateTime dtFillimi;
        /// <summary>
        /// data e mbarimit te vlefshmerise per tollonat
        /// </summary>
        private DateTime dtMbarrimi;
        /// <summary>
        /// id e statusit 
        /// </summary>
        private int idStatusDok;
        /// <summary>
        /// data e krijimi 
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
        public clsShitjeMeSerial()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idartikulli">id e burimit</param>

        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsShitjeMeSerial(int id, string seriali, int idartikulli, int idtrupishitje, double sasi, double cmim, int idStatusDok, DateTime dtfillimi, DateTime dtmbarimi)
        {
            this.id = id;
            this.seriali = seriali;
            this.idArtikulli = idartikulli;
            idTrupiShitje = idtrupishitje;
            this.sasia = sasi;
            this.cmimi = cmim;
            this.dtFillimi = dtfillimi;
            this.dtMbarrimi = dtmbarimi;
            this.idStatusDok = idStatusDok;
        }



        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i burimit</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsShitjeMeSerial(int idartikulli, string seriali)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                if (!mbushShitje(db.ktheShitjeMeSerialSipasSerialitDheArtikullit(idartikulli, seriali)).Status)
                    id = -1;
            }
        }
        public clsShitjeMeSerial(int idartikulli, string seriali, clsDatabazeTollona db)
        {
            if (!mbushShitje(db.ktheShitjeMeSerialSipasSerialitDheArtikullit(idartikulli, seriali)).Status)
                id = -1;

        }

        public clsShitjeMeSerial(DataRow rreshti)
        {
            
            mbushShitje(rreshti);
        }

        ///// <summary>
        ///// konstruktor me 1 parameter
        ///// </summary>
        ///// <param name="idburim">id e burimit</param>
        //public clsShitjeMeSerial(int idburim)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    mbushArtikull(db.ktheBurim(idburim));
        //    db.Dispose();
        //}
        //public clsShitjeMeSerial(int idburim, clsDatabazeTollona db)
        //{
        //    mbushArtikull(db.ktheBurim(idburim));

        //}
        #endregion

        #region Properties
        /// <summary>
        /// id ritese
        /// </summary>
        public int Id
        {
            get
            {
                return
                    id;
            }
            set
            {
                id = value;
            }
        }
        /// <summary>
        /// seriali
        /// </summary>
        public string Seriali
        {
            get
            {
                return seriali;
            }
            set
            {
                seriali = value;
            }
        }
        /// <summary>
        /// id e artikullit
        /// </summary>
        public int IdArtikulli
        {
            get
            {
                return idArtikulli;
            }
            set
            {
                idArtikulli = value;
            }
        }

        /// <summary>
        /// id i trupit te shitjes tek webinf 
        /// </summary>
        public int IdTrupiShitje
        {
            get { return idTrupiShitje; }
            set { idTrupiShitje = value; }
        }


        /// <summary>
        /// data e fillimit i vlefshme per tollonat
        /// </summary>
        public DateTime DtFillimi
        {
            get
            {
                return dtFillimi;
            }
            set
            {
                dtFillimi = value;
            }
        }
        /// <summary>
        /// data e mbarimit te vlefshmerise per tollonat
        /// </summary>
        public DateTime DtMbarrimi
        {
            get
            {
                return dtMbarrimi;
            }
            set
            {
                dtMbarrimi = value;
            }
        }
        /// <summary>
        /// cmimi i artikullit
        /// </summary>
        public double Cmimi
        {
            get
            {
                return cmimi;
            }
            set
            {
                cmimi = value;
            }
        }
        /// <summary>
        /// sasia e artikullit
        /// </summary>
        public double Sasia
        {
            get
            {
                return sasia;
            }
            set
            {
                sasia = value;
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
            int id = 0;
            mesazh = db.ruajShitjeMeSerial(out id, seriali,sasia,cmimi, idTrupiShitje,dtFillimi,dtMbarrimi, idArtikulli,  idStatusDok);
            this.id = id;
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
        public static bool kaTollonaShitja(int idshitjekoka)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.kaTollonaShitja(idshitjekoka);
            }
        }
        public static bool ekzistonSeriali(string seriali)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.ekzistonSeriali(seriali);
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushShitje(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    seriali = dbDataRow["SERIALI"].ToString();
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out  idArtikulli);
                    double.TryParse(dbDataRow["SASIA"].ToString(), out  sasia);
                    double.TryParse(dbDataRow["CMIMI"].ToString(), out  cmimi);
                    int.TryParse(dbDataRow["IDTRUPISHITJE"].ToString(), out idTrupiShitje);
                    DateTime.TryParse(dbDataRow["DTFILLIMI"].ToString(), out dtFillimi);
                    DateTime.TryParse(dbDataRow["DTMBARRIMI"].ToString(), out dtMbarrimi);
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
