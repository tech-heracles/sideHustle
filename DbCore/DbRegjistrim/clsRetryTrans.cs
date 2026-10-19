using DbCore.IMBUtils.Logging;
using System;

namespace DbCore.DbRegjistrim
{
    internal class clsRetryTrans
    {
        private string maxRetryEnded;
        // Me pare 500 tentativa me pritje 1, 2, 3 ... 500 sekonda (deri ~35 ore ne nje kerkese), dhe pritja llogaritej
        // gjithmone nga 500 edhe kur thirresi jepte nje numer tjeter (p.sh. 15 per raportet: pritja e pare 486 s). Thread-et
        // qe flinin keshtu grumbulloheshin nen ngarkese derisa IIS nuk pergjigjej me.
        private const int maxRetry = 20;
        private const int pritjaMaksimaleMs = 5000;
        private readonly int maxTentativa;
        private int retry;

        public clsRetryTrans(string emerAmbjenti)
        {
            maxRetryEnded = $": Tentativat per {emerAmbjenti} mbaruan ju lutem riprovoni me vone";
            this.retry = maxRetry;
            this.maxTentativa = maxRetry;
        }

        public clsRetryTrans(string emerAmbjenti, int maxRetryParam)
        {
            maxRetryEnded = $": Tentativat per {emerAmbjenti} mbaruan ju lutem riprovoni me vone";
            this.retry = maxRetryParam;
            this.maxTentativa = maxRetryParam;
        }

        /// <summary>Pritja para tentatives se radhes: 1, 2, ... deri 5 sekonda, plus deri 0,5 s rastesisht (qe dy transaksione
        /// ne deadlock te mos riprovojne ne te njejtin cast).</summary>
        private static void Prit(int tentativa)
        {
            int ms = Math.Min(Math.Max(tentativa, 1) * 1000, pritjaMaksimaleMs) + new Random(Guid.NewGuid().GetHashCode()).Next(500);
            System.Threading.Thread.Sleep(ms);
        }

        internal bool isRetrying()
        {
            return this.retry > 0;
        }

        internal void stopRetrying()
        {
            this.retry = 0;
        }

        internal bool checkRetry(System.Data.SqlClient.SqlException ex, DbAdmin.clsLogRivleresimInventari log, int indexRreshti)
        {
            if (ex.Number == 1205 || ex.Number == 121 || ex.Number == 1236)
            {
                int retryNumber = maxTentativa - retry + 1;
                Prit(retryNumber);
                log.logRetry(retryNumber, indexRreshti, ex.Number, ex.Message);
                if (--retry <= 0)
                {
                    log.logError(maxTentativa + maxRetryEnded);
                    throw new MyException(maxTentativa + maxRetryEnded);
                }
                return true;
            }
            return false;
        }

        internal bool checkRetry(System.Data.SqlClient.SqlException ex, string objekti, int maxRetryParam, string mesazhi)
        {
            if (ex.Number == 1205 || ex.Number == 121 || ex.Number == 1236 || ex.Number == -2)
            {
                int retryNumber = maxTentativa - retry + 1;
                Prit(retryNumber);
                ImbLogger.Info($"{DateTime.Now.ToLongTimeString()} - Retrying {objekti} nr:{retry}; errortext:{ex.Message}");
                if (--retry <= 0)
                {
                    ImbLogger.Error(maxRetryParam + maxRetryEnded + "- Message: " + ex.Message);
                    throw new MyException(maxTentativa + maxRetryEnded);
                }
                return true;
            }
            return false;
        }

        internal bool checkRetry(Exception ex,string objekti, int maxRetryParam, string mesazhi)
        {
            if (--retry <= 0)
            {
                ImbLogger.Error(maxRetryParam + maxRetryEnded + "- Message: " + ex.Message);
                throw new MyException(mesazhi);
            }
            int retryNumber = maxRetryParam - retry + 1;
            Prit(retryNumber);
            ImbLogger.Info($"{DateTime.Now.ToLongTimeString()} - Retrying {objekti} nr:{retry}; errortext:{ex.Message}");
            return true;
        }
    }
}
