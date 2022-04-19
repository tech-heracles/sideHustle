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
    public class clsKokaKlientAnketaAgjent
    {

        #region Atribute
        private int idKokaKlientAnkete;
        private int idKlientAnkete;
        private int idPerdoruesi;
        private DateTime dtKrijimi;
        private string shenime;
        private DateTime dtVeprimi;
        private int idNdermarrje;
        private string koordinata;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKokaKlientAnkete
        {
            get { return idKokaKlientAnkete; }
            set { idKokaKlientAnkete = value; }
        }
        /// <summary>
        /// Kthen/Vendos id e klientitanketes.
        /// </summary>
        public int IdKlientAnkete
        {
            get { return idKlientAnkete; }
            set { idKlientAnkete = value; }
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

        public string Koordinata
        {
            get { return koordinata; }
            set { koordinata = value; }
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
        public clsKokaKlientAnketaAgjent(int idKokaKlientAnkete, int idKlientAnkete, int idPerdoruesi, DateTime dtKrijimi, string shenime, DateTime dtVeprimi, int idNdermarrje, string koordinata)
        {
            this.idKokaKlientAnkete = idKokaKlientAnkete;
            this.idKlientAnkete = idKlientAnkete;
            this.idPerdoruesi = idPerdoruesi;
            this.dtKrijimi = dtKrijimi;
            this.shenime = shenime;
            this.dtVeprimi = dtVeprimi;
            this.idNdermarrje = idNdermarrje;
            this.koordinata = koordinata;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="idKokaKlientAnkete"></param>
        /// <param name="idndermarje"></param>
        public clsKokaKlientAnketaAgjent(int idKokaKlientAnkete, int idndermarje)
        {
            clsDatabaseCRM dbKokaKlientAnketaAgjent = new clsDatabaseCRM();
            mbushKokaKlientAnketaAgjent(dbKokaKlientAnketaAgjent.merrKokaKlientAnketaAgjent(idKokaKlientAnkete, idndermarje));
            dbKokaKlientAnketaAgjent.Dispose();
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKokaKlientAnketaAgjent()
        {
        }

        #endregion
        #region Metoda Publike

        /// <summary>
        /// Ruan objektin KokaKlientAnketaAgjent ne tabelen perkatese ne databaze.Therret funksionin
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
            int id;
            clsMesazh u_ruajt = data.ruajKokaKlientAnketaAgjent(out id, this.IdKlientAnkete, this.idPerdoruesi, this.shenime, this.dtVeprimi, this.idNdermarrje);
            this.idKlientAnkete = id;
            return u_ruajt;
        }

        #endregion
        #region Metoda Internal

        /// <summary>
        /// mbushja e KokaKlientAnketaAgjent nga databaza
        /// </summary>
        /// <param name="dbDataRowKlientAnketa">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushKokaKlientAnketaAgjent(DataRow dbDataRowKokaKlientAnketaAgjent)
        {
            if (dbDataRowKokaKlientAnketaAgjent != null)
            {
                try
                {
                    int.TryParse(dbDataRowKokaKlientAnketaAgjent["IDKOKAKLIENTANKETE"].ToString(), out idKokaKlientAnkete);
                    int.TryParse(dbDataRowKokaKlientAnketaAgjent["IDKLIENTANKETE"].ToString(), out idKlientAnkete);
                    int.TryParse(dbDataRowKokaKlientAnketaAgjent["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowKokaKlientAnketaAgjent["DTKRIJIMI"].ToString(), out dtKrijimi);
                    shenime = dbDataRowKokaKlientAnketaAgjent["SHENIME"].ToString();
                    DateTime.TryParse(dbDataRowKokaKlientAnketaAgjent["DTVEPRIMI"].ToString(), out dtVeprimi);
                    int.TryParse(dbDataRowKokaKlientAnketaAgjent["IDNDERMARJE"].ToString(), out idNdermarrje);
                    koordinata = dbDataRowKokaKlientAnketaAgjent["KOORDINATA"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhjes klient ankete agjent nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}