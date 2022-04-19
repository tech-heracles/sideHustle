using DbCore.IMBUtils.Logging;
using System;

namespace DbCore.DbRegjistrim
{
    internal class clsRetryTrans
    {
        private string maxRetryEnded;
        private const int maxRetry = 500;
        private int retry;

        public clsRetryTrans(string emerAmbjenti)
        {
            maxRetryEnded = $": Tentativat per {emerAmbjenti} mbaruan ju lutem riprovoni me vone";
            this.retry = maxRetry;            
        }

        public clsRetryTrans(string emerAmbjenti, int maxRetryParam)
        {
            maxRetryEnded = $": Tentativat per {emerAmbjenti} mbaruan ju lutem riprovoni me vone";
            this.retry = maxRetryParam;
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
                int retryNumber = maxRetry - retry + 1;
                Random random = new Random();
                int timeToWait = (random.Next(1) + retryNumber) * 1000; //in milisec
                System.Threading.Thread.Sleep(timeToWait);
                log.logRetry(retryNumber, indexRreshti, ex.Number, ex.Message);
                if (--retry <= 0)
                {
                    log.logError(maxRetry + maxRetryEnded);
                    throw new MyException(maxRetry + maxRetryEnded);
                }
                return true;
            }
            return false;
        }

        internal bool checkRetry(System.Data.SqlClient.SqlException ex, string objekti, int maxRetryParam, string mesazhi)
        {
            if (ex.Number == 1205 || ex.Number == 121 || ex.Number == 1236 || ex.Number == -2)
            {
                int retryNumber = maxRetry - retry + 1;
                Random random = new Random();
                int timeToWait = (random.Next(1) + retryNumber) * 1000; //in milisec
                System.Threading.Thread.Sleep(timeToWait);
                ImbLogger.Info($"{DateTime.Now.ToLongTimeString()} - Retrying {objekti} nr:{retry}; errortext:{ex.Message}; maxDelay:{timeToWait.ToString()}");
                if (--retry <= 0)
                {
                    ImbLogger.Error(maxRetryParam + maxRetryEnded + "- Message: " + ex.Message);
                    throw new MyException(maxRetry + maxRetryEnded);
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
            Random random = new Random();
            int timeToWait = (random.Next(1) + retryNumber) * 1000; //in milisec
            System.Threading.Thread.Sleep(timeToWait);
            ImbLogger.Info($"{DateTime.Now.ToLongTimeString()} - Retrying {objekti} nr:{retry}; errortext:{ex.Message}; maxDelay:{timeToWait.ToString()}");
            return true;
        }
    }
}