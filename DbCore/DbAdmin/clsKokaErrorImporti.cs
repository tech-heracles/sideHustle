using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
   public class clsKokaErrorImporti
   {
        /// <summary>
        /// Kjo klase permban metodat e nevojshme per te perdorur te dhenat e tabeles T_KOKAERRORIMPORTI
        /// </summary>

        #region Atribute

        private int id;
       
        private string pershkrimi;
        private int idKategori;
        private int idNdermarje;
        private int idPerdoruesi;
        
        private DateTime oraImporti;
        private colTrupiErrorImporti colTrupi;
    

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="idKoka">id </param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="idkategori">kategoria</param>
        /// <param name="idNder">id ndermarrjes</param>
        /// <param name="idPerd">id perdoruesit</param>
        /// <param name="idStatus">id statusit dok</param>
        public clsKokaErrorImporti(int idKoka,  string pershkrimi, int idkategori, int idNder,
                                    int idPerd)
        {
            this.id = idKoka;
            this.pershkrimi = pershkrimi;
            this.idKategori = idkategori;
            this.idNdermarje = idNder;
            this.idPerdoruesi = idPerd;


            colTrupi = new  colTrupiErrorImporti ();
        }
     

        /// <summary>
        /// Konstruktori default
        /// </summary>
        public clsKokaErrorImporti()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

       

        /// <summary>
        /// Kthen/Vendos pershkrimin 
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }


        /// <summary>
        /// Kthen/Vendos  kategorine e formatit te importit
        /// </summary>
        public int IdKategori
        {
            get { return idKategori; }
            set { idKategori = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e ndermarrjes 
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos id se perdoruesit 
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }


        /// <summary>
        /// Kthen/Vendos daten e krijimit 
        /// </summary>
        public DateTime OraImporti
        {
            get { return oraImporti; }
            set { oraImporti = value; }
        }

     

        /// <summary>
        /// Kthen/Vendos collection-in me trupat 
        /// </summary>
        public colTrupiErrorImporti ColTrupi
        {
            get { return colTrupi; }
            set { colTrupi = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan formatin e importit
        /// </summary>
        /// <returns>Mesazhin qe jep info ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        public clsMesazh ruajErrorImporti()
        {
            //clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            //clsMesazh mesazh;
            //dbAdmin.beginTransaksion();
            try
            {
                ImbLogger.LogErrorImporti(ToString());
                
                StringBuilder strTrupat = new StringBuilder();
                foreach (clsTrupiErrorImporti tr in this.colTrupi)
                {
                    strTrupat.AppendLine(tr.ToString());
                }
                ImbLogger.LogErrorImporti(strTrupat.ToString());
                //Nqs ruhet me sukses edhe trupi kthejme mesazhin e suksesit
                //dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                //Ne te gjitha rastet e tjera kthejme mesazhin me pershkrimin e gabimit qe ka ndodhur                                             
            }
            catch (Exception e)
            {
                return new clsMesazh(false, e.Message);
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushErrorImporti(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                     int.TryParse(dbDataRow["IDKATEGORI"].ToString(), out idKategori);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);

                    DateTime.TryParse(dbDataRow["ORAIMPORTI"].ToString(), out oraImporti);
              
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se error importi  nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

        public override string ToString()
        {
            return $"ERORR KOKA IMPORTI ||pershkrimi :{pershkrimi}||kategoria :{idKategori}||ndermarrja :{idNdermarje}||perdoruesi:{idPerdoruesi}||";
        }

    }
}

