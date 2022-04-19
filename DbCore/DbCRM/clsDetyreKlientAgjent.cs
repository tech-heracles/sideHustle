
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbCRM
{
    /// <summary>
    /// klasa per kryerjes e veprimeve me tabelen T_CRM_KOKAKLIENT_ANKETA_AGJENT
    /// </summary>
    public class clsDetyreKlientAgjent
    {

        #region Atribute
        private int idKlientDetyreAgjent;
        private int idDetyreKlient;
        private int idPerdoruesi;
        private DateTime dtKrijimi;
        private string shenime;
        private DateTime dtVeprimi;
        private int idNdermarrje;

        private bool status;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKlientDetyreAgjent
        {
            get { return idKlientDetyreAgjent; }
            set { idKlientDetyreAgjent = value; }
        }
        /// <summary>
        /// Kthen/Vendos id e klientit.
        /// </summary>
        public int IdDetyreKlient
        {
            get { return idDetyreKlient; }
            set { idDetyreKlient = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe eshte agjenti
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos fushen shenime te kokes
        /// </summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }
        /// <summary>
        /// Kthen/Vendos DtVeprimi
        /// </summary>
        public DateTime DtVeprimi
        {
            get { return dtVeprimi; }
            set { dtVeprimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
    

        #endregion

        #region Konstruktoret

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idKokaKlientAnkete"></param>
        /// <param name="idKlientAnkete"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="dtKrijimi"></param>
        /// <param name="shenime"></param>
        /// <param name="dtVeprimi"></param>
        /// <param name="idNdermarrje"></param>
        public clsDetyreKlientAgjent(int idKokaKlientAnkete, int idKlientAnkete, int idPerdoruesi, DateTime dtKrijimi, string shenime, DateTime dtVeprimi, int idNdermarrje, bool status)
        {
            this.idKlientDetyreAgjent = idKokaKlientAnkete;
            this.idDetyreKlient = idKlientAnkete;
            this.idPerdoruesi = idPerdoruesi;
            this.dtKrijimi = dtKrijimi;
            this.shenime = shenime;
            this.dtVeprimi = dtVeprimi;
            this.idNdermarrje = idNdermarrje;
            this.status = status;
        }

 

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsDetyreKlientAgjent()
        {
        }

        #endregion
        #region Metoda Publike

        /// <summary>
        /// Ruan objektin DetyreKlientAgjent ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbCRM.clsDatabaseCRM.ruajKlientAnketa"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>

        public clsMesazh ruaj()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_ruajt = ruaj(data);
            data.Dispose();
            return u_ruajt;
        }

        public clsMesazh ruaj(clsDatabaseCRM data)
        {

            clsMesazh u_ruajt = data.ruajDetyreKlientAgjent(out this.idKlientDetyreAgjent, this.idDetyreKlient, this.idPerdoruesi, this.shenime, this.dtVeprimi, this.idNdermarrje);

            return u_ruajt;
        }

        #endregion
        #region Metoda Internal

        /// <summary>
        /// mbushja e DetyreKlientAgjent nga databaza
        /// </summary>
        /// <param name="dbDataRowKlientAnketa">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushDetyreKlientAgjent(DataRow dbDataRowDetyreKlientAgjent)
        {
            if (dbDataRowDetyreKlientAgjent != null)
            {
                try
                {
                    int.TryParse(dbDataRowDetyreKlientAgjent["IDKLIENTDETYREAGJENT"].ToString(), out idKlientDetyreAgjent);
                    int.TryParse(dbDataRowDetyreKlientAgjent["IDDETYREKLIENT"].ToString(), out idDetyreKlient);
                    int.TryParse(dbDataRowDetyreKlientAgjent["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowDetyreKlientAgjent["DTKRIJIMI"].ToString(), out dtKrijimi);
                    shenime = dbDataRowDetyreKlientAgjent["SHENIME"].ToString();
                    DateTime.TryParse(dbDataRowDetyreKlientAgjent["DTVEPRIMI"].ToString(), out dtVeprimi);
                    int.TryParse(dbDataRowDetyreKlientAgjent["IDNDERMARJE"].ToString(), out idNdermarrje);
                    status =Convert.ToBoolean(dbDataRowDetyreKlientAgjent["STATUS"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhjes klient detyre agjent nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}