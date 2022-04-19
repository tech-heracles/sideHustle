using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Klasa qe perdoret per te mbajtur loget e People Finder
    /// </summary>
    public class clsLoguPeopleFinder
    {
        #region Atribute

        private int idLogu;
        private string ipKlient;
        private DateTime kohaLogut;
        private string pershkrimiLogut;
        private string kodPershkrimi;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLogu
        {
            get { return idLogu; }
            set { idLogu = value; }
        }

        /// <summary>
        /// Kthen/Vendos IP e klientit qe ka bere kerkesen.
        /// </summary>
        public string IpKlient
        {
            get { return ipKlient; }
            set { ipKlient = value; }
        }

        /// <summary>
        /// Kthen/Vendos kohen kur ka ndodhur logu.
        /// </summary>
        public DateTime KohaLogut
        {
            get { return kohaLogut; }
            set { kohaLogut = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e logut.
        /// </summary>
        public string PershkrimiLogut
        {
            get { return pershkrimiLogut; }
            set { pershkrimiLogut = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e pershkrimit te logut.
        /// </summary>
        public string KodPershkrimi
        {
            get { return kodPershkrimi; }
            set { kodPershkrimi = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori bosh i logeve per People Finder
        /// </summary>
        public clsLoguPeopleFinder()
        {
        }

        /// <summary>
        /// Konstruktori i plote i klases
        /// </summary>
        /// <param name="ipKlient">(int) Ip e klientit qe ka bere kerkesen</param>
        /// <param name="kohaLogut">(string) Koha kur eshte kapur logu</param>
        /// <param name="pershkrimiLogut">(string) Pershkrimi i logut</param>
        /// <param name="kodPershkrimi">(string) Kodi i pershkrimit te logut</param>
        public clsLoguPeopleFinder(string ipKlient, DateTime kohaLogut, string pershkrimiLogut, string kodPershkrimi)
        {
            this.ipKlient = ipKlient;
            this.kohaLogut = kohaLogut;
            this.pershkrimiLogut = pershkrimiLogut;
            this.kodPershkrimi = kodPershkrimi;
        }

        /// <summary>
        /// Konstruktori me parameter rreshtin e kthyer nga DB-ja per te mbushur klasen
        /// </summary>
        /// <param name="rreshti">(DataRow) Rreshti i kthyer nga DB-ja</param>
        public clsLoguPeopleFinder(DataRow rreshti)
        {
            mbushLogun(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Mbush atributet e klases nga rreshti i kthyer nga DB-ja.
        /// </summary>
        /// <param name="rreshti">(DataRow) Rreshti i kthyer nga DB-ja.</param>
        /// <returns>Kthen True nese mbushja kryhet me sukses dhe False ne te kundert.</returns>
        internal bool mbushLogun(DataRow rreshti)
        {
            try
            {
                this.idLogu = Convert.ToInt32(rreshti["ID_LOG"]);
                this.ipKlient = Convert.ToString(rreshti["IP_KLIENT"]);
                DateTime.TryParse(rreshti["KOHA"].ToString(), out  kohaLogut);
                this.pershkrimiLogut = Convert.ToString(rreshti["ARYSEJA"]);
                this.kodPershkrimi = Convert.ToString(rreshti["KOD_PERSHKRIMI"]);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Metoda Public 

        public clsMesazh shtoLogError()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh = data.ruajLogPeopleFinder(ipKlient, kohaLogut, pershkrimiLogut, kodPershkrimi);
            data.Dispose();
            return mesazh;
        }

        public static bool bllokoLogimPeopleFinder()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            int nrErr = data.ktheErrorLogimiNgaPeopleFinder();
            data.Dispose();
            if (nrErr < 10)
                return false;
            return true;
        }

        #endregion
    }
}
