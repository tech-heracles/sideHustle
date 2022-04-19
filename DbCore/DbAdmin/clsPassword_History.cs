using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DbCore.DbAdmin;

namespace PlatinumWeb
{
    
    /// <summary>
    /// Kjo klase sherben per objektet qe perfaqesojne konfigurimin e politikave te fjalekalimit. Konfigurimi eshte ne nivel licence.
    /// Te dhenat merren nga tabela T_PASSWORD_HISTORY
    /// </summary>
    public class clsPassword_History
    {

        #region Atributet

        private int passHistoryID;
        private int perdoruesID;
        private string password;
        private DateTime dateKrijimi;
        private int idPerdoruesiLoguar;
      
        #endregion

        #region Properties

        /// <summary>
        /// Kthen id-ne qe gjenerohet automatikisht
        /// </summary>
        public int PassHistoryID
        {
            get { return passHistoryID; }
           // set { passHistoryID = value; }
        }

        /// <summary>
        /// Kthen/Vendos id-ne e perdoruesit
        /// </summary>
        public int PerdoruesID
        {
            get { return perdoruesID; }
            set { perdoruesID = value; }
        }

        /// <summary>
        /// Kthen/Vendos password-in e perdoruesit
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit
        /// </summary>
        public DateTime DateKrijimi
        {
            get { return dateKrijimi; }
            set { dateKrijimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e perdoruesit i cili ben veprimin
        /// ne rastin kur behet resetimi i passwordit nga linku ne faqen e loginit, fusha idPerdoruesiLoguar ne tabelen T_PASSWORD_HISTORY do kete id e perdoruesit sipas username te vendosur per resetimin e pass
        /// </summary>
        public int IdPerdoruesiLoguar
        {
            get { return idPerdoruesiLoguar; }
            set { idPerdoruesiLoguar = value; }
        }
        #endregion

        #region Konstruktoret
        public clsPassword_History() { }

        public clsPassword_History(int idPerdorues, string password) {
        
             using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
               mbushPassHistory(data.merPassHistorySipasPassDhePerd(idPerdorues, password));
            }
        }

        //public clsPassword_History(string password, int nr)
        //{
        //    using (clsDatabaseAdmin data = new clsDatabaseAdmin())
        //    {
        //        data.passwordIPerdorur(password, nr);
        //    }

        //}


        #endregion

        #region Metoda publike

        public static bool shtoPassNeHistorik(int idPerdoruesi, String Pass, int nrPassNeHistorik, int idPerdoruesiLoguar, clsDatabaseAdmin db)
        {

            return db.shtoPasswordNeHistorik(idPerdoruesi, Pass, nrPassNeHistorik, idPerdoruesiLoguar);
            
        }
        public static bool shtoPassNeHistorikPunonjes(int idPunonjes, String Pass, int nrPassNeHistorik, int idPerdoruesiLoguar, clsDatabaseAdmin db)
        {

            return db.shtoPasswordNeHistorikPunonjes(idPunonjes, Pass, nrPassNeHistorik, idPerdoruesiLoguar);
            
        }
        
      
        #endregion

        #region metoda internal

        internal bool mbushPassHistory(DataRow dbDataRowKonfigurimeFjalekalimi)
        {
            if (dbDataRowKonfigurimeFjalekalimi != null)
            {
                try
                {
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["PASSHISTORYID"].ToString(), out passHistoryID);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["PERDORUESID"].ToString(), out perdoruesID);
                    password = Convert.ToString(dbDataRowKonfigurimeFjalekalimi["PASSWORD"]);
                    if (!(dbDataRowKonfigurimeFjalekalimi["DATEKRIJIMI"] is System.DBNull))
                        dateKrijimi = Convert.ToDateTime(dbDataRowKonfigurimeFjalekalimi["DATEKRIJIMI"]);
                    if (!(dbDataRowKonfigurimeFjalekalimi["IDPERDORUESILOGUAR"] is System.DBNull))
                        idPerdoruesiLoguar = Convert.ToInt32(dbDataRowKonfigurimeFjalekalimi["IDPERDORUESILOGUAR"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new DbCore.MyException("ERROR: Gabim casti gjate marrjes se password history nga databaza!");
                }
                catch (Exception)
                {
                    throw new DbCore.MyException("ERROR: Gabim gjate marrjes se password history nga databaza!");
                }
            }
            else
                return false;
        }
        #endregion
    }
}