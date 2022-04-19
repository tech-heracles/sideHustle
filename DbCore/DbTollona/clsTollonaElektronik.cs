using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbKontabiliteti;

namespace DbCore.DbTollona
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje tollon leter
    ///  (Te dhenat  merren nga tabela : t_tollonaelektronik tek dbtollonat)
    /// </summary>
    public class clsTollonaElektronik
    {

        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Tollonat leter u mbush me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se tollonave nga db-ja";
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
        /// fillim seriali
        /// </summary>
        private string kodArtikulli;
       
        /// <summary>
        /// id e artikullit
        /// </summary>
        private int idArtikulli;

        /// <summary>
        /// id e trupit te shitjes ne alphaweb
        /// </summary>
        private int idTrupiShitje;
        /// <summary>
        /// kodi i klientit
        /// </summary>
        private string kodKlienti;
        /// <summary>
        /// pershkrimi i klientit
        /// </summary>
        private string pershkrimKlienti;
        /// <summary>
        /// totali i litrave
        /// </summary>
        private double totaliLitra;
        /// <summary>
        /// statusi i mare nga equitmatic apo jo
        /// </summary>

        private bool status;
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
        public clsTollonaElektronik()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idartikulli">id e burimit</param>

        /// 
        public clsTollonaElektronik(int id, string kodartikulli,  string kodKlienti, string pershkrimKlienti, int idartikulli, int idtrupishitje, double totallitra, bool status)
        {
            this.id = id;
            this.kodArtikulli = kodartikulli;
            this.kodKlienti = kodKlienti;
            this.pershkrimKlienti = pershkrimKlienti;
            this.idArtikulli = idartikulli;
            idTrupiShitje = idtrupishitje;
            this.totaliLitra = totallitra;
            this.status = status;


        }

        public clsTollonaElektronik(DataRow rreshti)
        {
            
            mbushShitje(rreshti);
        }



        ///// <summary>
        ///// konstruktor me 2 parametra
        ///// </summary>
        ///// <param name="kodi">kodi i burimit</param>
        ///// <param name="idNderm">id e ndermarrjes</param>
        //public clsTollonaLeter(int idartikulli, string seriali)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //    if (!mbushShitje(db.ktheShitjeMeSerialSipasSerialitDheArtikullit(idartikulli, seriali)).Status)
        //        id = -1;
        //    db.Dispose();
        //}
        //public clsTollonaLeter(int idartikulli, string seriali, clsDatabazeTollona db)
        //{
        //    if (!mbushShitje(db.ktheShitjeMeSerialSipasSerialitDheArtikullit(idartikulli, seriali)).Status)
        //        id = -1;

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
        /// kodi i klientit
        /// </summary>
        public string KodKlienti
        {
            get
            {
                return kodKlienti;
            }
            set
            {
                kodKlienti = value;
            }
        }
      
        /// <summary>
        /// pershkrim klienti
        /// </summary>
        public string PershkrimKlienti
        {
            get
            {
                return pershkrimKlienti;
            }
            set
            {
                pershkrimKlienti = value;
            }
        }
        /// <summary>
        /// artikulli
        /// </summary>
        public string KodArtikulli
        {
            get
            {
                return kodArtikulli;
            }
            set
            {
                kodArtikulli = value;
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
        /// sasia e artikullit
        /// </summary>
        public double TotaliLitra
        {
            get
            {
                return totaliLitra;
            }
            set
            {
                totaliLitra = value;
            }
        }
        /// <summary>
        /// statusi i mare nga Equitmatic apo jo
        /// </summary>
        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
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
            int id = 0;
            mesazh = db.ruajTollonaElektronik(out id, kodArtikulli, totaliLitra,  idTrupiShitje, kodKlienti, pershkrimKlienti, idArtikulli, status);
            this.id = id;
            return mesazh;
        }



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
                return db.kaTollonaShitjaKastratiElektronik(idshitjekoka);                
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
                    kodArtikulli = dbDataRow["KODARTIKULLI"].ToString();
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out  idArtikulli);
                    double.TryParse(dbDataRow["TOTALILITRA"].ToString(), out  totaliLitra);
                    int.TryParse(dbDataRow["IDTRUPISHITJE"].ToString(), out idTrupiShitje);
                    kodKlienti = dbDataRow["KodKlienti"].ToString();
                    pershkrimKlienti = dbDataRow["PershkrimKlienti"].ToString();
                    bool.TryParse(dbDataRow["Status"].ToString(), out status);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsTollonaElektronik.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsTollonaElektronik.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsTollonaElektronik.drbosh);
        }

        #endregion
    }
}
