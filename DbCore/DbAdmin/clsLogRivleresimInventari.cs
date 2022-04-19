using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Kjo klase sherben per te ruajtur veprimet e perdoruesve qe kryhen ne ambjentin e rivleresimit te magazines 
    /// Informacioni ruhet ne tabelen T_LOGRIVLERESIMINVENTARI
    /// </summary>
    public class clsLogRivleresimInventari
    {

        #region Atributet

        //private int idLogRivleresimInventari;
        private int idPerdoruesi;
        private int idNdermarje;
        private DateTime startTime;
        private DateTime stopTime;
        private long idStartTime;
        private int nrArtikujve;
        private int nrMagazinave;
        private DateTime periudhaNga;
        private DateTime periudhaDeri;
        private string arsyeStopimi;
        private string fileLogPath;
        private TimeSpan maxTs;
        private const string logRivleresimiPath = "~/log/log.txt";


        #endregion Atributet

        #region Properties
        public TimeSpan MaxTs
        {
            get
            {
                return maxTs;
            }

            set
            {
                maxTs = value;
            }
        }

        #endregion Properties

        #region Konstruktoret

        public clsLogRivleresimInventari()
        {            
        }
        public clsLogRivleresimInventari(int idPerdoruesi, int idNdermarje)
        {
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.startTime = DateTime.Now;
            idStartTime = this.startTime.Ticks;
            this.stopTime = DateTime.MinValue;
            this.nrArtikujve = 0;
            this.nrMagazinave = 0;
            this.periudhaNga = new DateTime(1970,01,01);
            this.periudhaDeri = new DateTime(1970, 01, 01);
            this.maxTs = TimeSpan.Zero;
            this.arsyeStopimi = "";
            this.fileLogPath = System.Web.HttpContext.Current.Server.MapPath(logRivleresimiPath);
            ruajLogun();
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} shkurt START perd:{2}; nder:{3}; fillon:{4};", DateTime.Now.ToLongTimeString(), idStartTime, idPerdoruesi, idNdermarje,
startTime.ToLongDateString());

        }
        public clsLogRivleresimInventari(int idPerdoruesi, int idNdermarje, int nrArtikujve, int nrMagazinave, DateTime periudhaNga, DateTime periudhaDeri)
        {
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.startTime = DateTime.Now;
            idStartTime = this.startTime.Ticks;
            this.stopTime = DateTime.MinValue;            
            this.nrArtikujve = nrArtikujve;
            this.nrMagazinave = nrMagazinave;
            this.periudhaNga = periudhaNga;
            this.periudhaDeri = periudhaDeri;
            this.maxTs = TimeSpan.Zero;
            this.arsyeStopimi = "";
            this.fileLogPath = System.Web.HttpContext.Current.Server.MapPath(logRivleresimiPath);
            ruajLogun();
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} START perd:{2}; nder:{3}; fillon:{4}; nrArt:{5}; nrMag:{6}; nga:{7}; deri:{8}", DateTime.Now.ToLongTimeString(), idStartTime, idPerdoruesi, idNdermarje,
startTime.ToLongDateString(),
nrArtikujve, nrMagazinave, periudhaNga.Date.ToLongDateString(), periudhaDeri.Date.ToLongDateString());
        }

        #endregion Konstruktoret

        #region Metoda Publike
        public static String lastArtRivleresim(int idPerdoruesi, int idNdermarje)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.getLastArtRivleresim(idPerdoruesi, idNdermarje);
            }
        }
        /// <summary>
        /// Ruan logun e veprimeve te kryera per rivleresimin e inventarit. 
        /// Perdoruesin, ndermarrjen, nr e artikujve, nr e magazinave, periudhen e fillimit, periudhen e mbarimit, kohen e fillimit te rivleresimit
        /// </summary>
        /// <returns>Mesazhin qe jep logu ne lidhje me ruajtjen e sukseshme, ose gabimin qe ka ndodhur nese ka te tille</returns>
        private clsMesazh ruajLogun()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                clsMesazh mesazh;
                try
                {
                    mesazh = dbAdmin.ruajLogunPerRivleresiminEInventarit(this.idPerdoruesi, this.idNdermarje, this.startTime, this.idStartTime, this.nrArtikujve, this.nrMagazinave, this.periudhaNga, this.periudhaDeri);
                    if (!mesazh.Status)
                    {
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                    return new clsMesazh(true, "Ruajtja logut perfundoi me sukses!");
                }
                catch (Exception e)
                {
                    return new clsMesazh(false, e.Message);
                }
            }
        }

        /// <summary>
        /// vendos daten/oren e perfundimit te rivleresimit
        /// </summary>
        /// <param name="idStartTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        public clsMesazh logStopRivleresim(string mesazhArsyeStopimi)
        {
            return logStopRivleresim(mesazhArsyeStopimi, String.Empty, DateTime.MinValue, String.Empty);
        }
        /// <summary>
        /// vendos daten/oren e perfundimit te rivleresimit
        /// </summary>
        /// <param name="idStartTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        public clsMesazh logStopRivleresim(string mesazhArsyeStopimi, string kodArt)
        {
            return logStopRivleresim(mesazhArsyeStopimi, kodArt, DateTime.MinValue, String.Empty);
        }
        /// <summary>
        /// vendos daten/oren e perfundimit te rivleresimit
        /// </summary>
        /// <param name="idStartTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        public clsMesazh logStopRivleresim(DateTime stopTime, string mesazhArsyeStopimi, string kodArt, DateTime dita)
        {
            string exMsg = String.Empty;
            return logStopRivleresim(mesazhArsyeStopimi, kodArt, dita, exMsg);
        }
        /// <summary>
        /// vendos daten/oren e perfundimit te rivleresimit
        /// </summary>
        /// <param name="idStartTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        public clsMesazh logStopRivleresim(string mesazhArsyeStopimi, string kodArt, DateTime dita, string exMsg)
        {
            DateTime stopTime = DateTime.Now;
            string dateString = dita == DateTime.MinValue ? "" : dita.ToShortDateString();
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} Interrupted kodArt:{2}; dita:{3}; arsyeStop:{4}; exMsg:{5}; maxDelay:{6}", DateTime.Now.ToLongTimeString(), idStartTime, kodArt, dateString, mesazhArsyeStopimi, exMsg,maxTs.ToString());
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin()) { 
                return dbAdmin.modifikoStopTime(idStartTime, stopTime, mesazhArsyeStopimi+" tek artikulli me kod: "+ kodArt+" maxDelay: " + maxTs.ToString());
            }
        }

        public clsMesazh logFinishedRivleresim(string mesazhArsyeStopimi)
        {
            DateTime stopTime = DateTime.Now;
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} SUCCEEDED:{2}; perd:{3}; nder:{4}; koheZgjatja:{5}; nrArt:{6}; nrMag:{7}; nga:{8}; deri:{9}; maxDelay{10}", stopTime.ToLongTimeString(), idStartTime, stopTime.ToShortTimeString(), idPerdoruesi, idNdermarje,
                    (stopTime - startTime).ToString(), nrArtikujve == 0 ? "" : nrArtikujve.ToString(), nrMagazinave == 0 ? "" : nrMagazinave.ToString(), periudhaNga == DateTime.MinValue ? "" : periudhaNga.Date.ToLongDateString(), periudhaDeri == DateTime.MinValue ? "" : periudhaDeri.Date.ToLongDateString(), maxTs.ToString());
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.modifikoStopTime(idStartTime, stopTime, mesazhArsyeStopimi + "maxDelay: " + maxTs.ToString());
            }
        }  

        public void logRivleresim(string kodArtikulli, DateTime dita)
        {
            string kodMagazina = String.Empty;
            logRivleresim(kodArtikulli, kodMagazina, dita);
        }
        public void logRivleresim(string kodArtikulli, string kodMagazina, DateTime dita)
        {
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} - art:{2}; mag:{3}; data:{4}; maxDelay:{5}", DateTime.Now.ToLongTimeString(), idStartTime, kodArtikulli, kodMagazina,
    dita.ToLongDateString(),maxTs.ToString());
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.setLastArtRivleresim(idStartTime, kodArtikulli);
            }
        }
        public void logRetry(int retry) {
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} - Retrying rivleresimin nr:{2}; maxDelay:{3}", DateTime.Now.ToLongTimeString(), idStartTime, retry,maxTs.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retry"></param>
        /// <param name="dokuI"></param>
        public void logRetry(int retry, int dokuI, int nrError, string errortext)
        {
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} - Retrying rivleresimin nr:{2}; dokumenti i:{3}; nrError:{4}; errortext:{5}; maxDelay:{6}", DateTime.Now.ToLongTimeString(), idStartTime, retry, dokuI, nrError, errortext, maxTs.ToString());
        }
        public void logRetry(int retry, string metoda, int nrError, string errortext)
        {
            ImbLogger.LogInfoRivleresimi("{0} Rivleresimi:{1} - Retrying rivleresimin nr:{2}; metoda:{3}; nrError:{4}; errortext:{5}; maxDelay:{6}", DateTime.Now.ToLongTimeString(), idStartTime, retry, metoda, nrError, errortext, maxTs.ToString());
        }
        public void logError(int nrError, string errortext)
        {
            ImbLogger.LogErrorRivleresimi("{0} Rivleresimi:{1} - ERROR rivleresimi nrError:{2}; errortext:{3}; maxDelay:{4}", DateTime.Now.ToLongTimeString(), idStartTime, nrError, errortext, maxTs.ToString());
        }
        public void logError(string errortext)
        {
            ImbLogger.LogErrorRivleresimi("{0} Rivleresimi:{1} - ERROR GJENERAL rivleresimi errortext:{2}; maxDelay:{3}", DateTime.Now.ToLongTimeString(), idStartTime, errortext, maxTs.ToString());
        }

        #region Funksioni i vjeter qe shkruan loget e rivleresimit
        //private void writeOnLogFile(string stringToWrite) {
        //    var maxRetry = 3;
        //    for (int retry = 0; retry < maxRetry; retry++)
        //    {
        //        try
        //        {
        //            using (StreamWriter sw = new StreamWriter(fileLogPath, true))
        //            {
        //                sw.WriteLine(stringToWrite);
        //                break; // you were successfull so leave the retry loop
        //            }
        //        }
        //        catch (IOException)
        //        {
        //            if (retry < maxRetry - 1)
        //            {
        //                System.Threading.Thread.Sleep(1435); // Wait some time before retry (2 secs)
        //            }
        //            else
        //            {
        //                // handle unsuccessfull write attempts or just ignore.
        //            }
        //        }
        //    }
        //}
        #endregion

        #endregion

    }
}
