using System;
using System.IO;
using System.Threading;
using System.Web.Hosting;

namespace AlphaWebCommon.Logging
{
    public class clsLogError
    {
        #region Atributet
        private string fileLogPath;
        private string error;
        #endregion Atributet
        public clsLogError(string path, string errorMsg)
        {
            fileLogPath = path;
            error = errorMsg;
            shkruajErrorNeLogFile();
        }


        public void shkruajErrorNeLogFile()
        {
            var maxRetry = 3;
            for (int retry = 0; retry < maxRetry; retry++)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(HostingEnvironment.MapPath(fileLogPath), true))
                    {
                        Log(sw);
                        break; // you were successfull so leave the retry loop
                    }
                }
                catch (IOException)
                {
                    if (retry < maxRetry - 1)
                    {
                        Thread.Sleep(1435); // Wait some time before retry (2 secs)
                    }
                    else
                    {
                        // handle unsuccessfull write attempts or just ignore.
                    }
                }
            }
        }
        public void Log(StreamWriter sw)
        {
            try
            {
                sw.Write("\r\nLog Entry : ");
                sw.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                sw.WriteLine("  :{0}", error);
                sw.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
            }
        }
    }
}