using System;
using System.Diagnostics;
using DbCore.IMBUtils.DataBase;
using NLog;

namespace DbCore.IMBUtils.Logging
{

    /// <summary>
    /// Copyright IMB Prill 2016
    /// </summary>

    public class ImbLogger
    {
        const string LogImportiName = "importi";
        const string LogWebApi = "WebApi";
        const string LogBrmName = "brm";
        const string LogPromocioneshName = "promocionet";
        const string LogRivleresimi = "Rivleresimet";
        const string LogOTCName = "logOTC";
        const string LogTraces = "Traces";
        //shtim
        const string LogShitje = "shitje";
        const string LogBuxhetimi = "buxhetimi";
        const string LogMbylljePeriudhe = "mbylljePeriudhe";

        private static ILogger _webApiLogger = LogManager.GetLogger(LogWebApi);
        private static ILogger _importiLogger = LogManager.GetLogger(LogImportiName);
        private static ILogger _brmLogger = LogManager.GetLogger(LogBrmName);
        private static ILogger _promoLogger = LogManager.GetLogger(LogPromocioneshName);
        private static ILogger _rivleresimiLogger = LogManager.GetLogger(LogRivleresimi);
        private static ILogger _otcLogger = LogManager.GetLogger(LogOTCName);
        private static ILogger _traceLogger = LogManager.GetLogger(LogTraces);
        //shtim 
        private static ILogger _shitjeLogger = LogManager.GetLogger(LogShitje);
        private static ILogger _mbylljePeriudheLogger = LogManager.GetLogger(LogMbylljePeriudhe);
        private static ILogger _buxhetimiLogger = LogManager.GetLogger(LogBuxhetimi);

        public static ILogger MbylljePeriudheLogger
        {
            get { return _mbylljePeriudheLogger; }
            set { _mbylljePeriudheLogger = value; }
        }

        public static ILogger BuxhetimiLogger
        {
            get { return _buxhetimiLogger; }
            set { _buxhetimiLogger = value; }
        }
        static ImbLogger()
        {
        }

        private static string ServerName
        {
            get
            {
                try
                {
                    return MyConnectionsManager.GetSelectedConNameServer();
                }
                catch
                {
                    return MyConnectionsManager.ConnStringNameDefault;
                }
            }
        }

        #region te pergjithshme
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Log(LogLevel level, StackFrame stackFrame, string message)
        {


        }

        private static void Log(LogLevel level, Exception ex, string message = null)
        {

        }

        private static void Log(LogLevel level, string message)
        {
        }

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Error(Exception ex)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Error(Exception ex, string message)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Error(string message, Exception ex)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Error(string errorMesage)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Warn(Exception ex)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Warn(string warnMessage)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Info(string infoMessage)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Trace(string traceMessage)
        {
        }

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Fatal(Exception ex)
        {
        }

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void Fatal(string fatalErrorMessage)
        {
        }
        #endregion

        #region Log Traces
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogTrace(string message)
        {
        }
        #endregion

        //shtim
        #region Loget e shitjeve

        public static void LogErrorShitje(string error)
        {
            LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogShitje, error);
            errorEvent.Properties["DataBase"] = ServerName;
            _shitjeLogger.Log(errorEvent);
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogWarningShitje(string warning)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogTraceShitje(string trace)
        {
        }

        #endregion

        #region Loget e Buxhetimit

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorBuxhetimi(Exception error)
        {
        }
        public static void LogWarningBuxhetimi(string warning)
        {
        }
        private static void WarnBuxhetimi(string warning)
        {
        }
        public static void LogTraceBuxhetimi(string trace)
        {
        }
        #endregion

        #region Loget e importit

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorImporti(string error)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoImporti(string infoMessage)
        {
        }

        #endregion

        #region Loget e webapi
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorWebApi(string error, params object[] args)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoWebApi(string infoMessage, params object[] args)
        {
        }
        #endregion

        #region Loget e BRM

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorBrm(string error)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorBrm(Exception ex, string error)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorBrm(string error, params object[] args)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorBrm(Exception ex, string error, params object[] args)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoBrm(string infoMessage)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoBrm(string infoMessage, params object[] args)
        {
        }
        #endregion

        #region Loget e promocioneve
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorPromocione(string error)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorPromocione(string error, params object[] args)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoPromocione(string infoMessage)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoPromocione(string infoMessage, params object[] args)
        {
        }
        #endregion

        #region Loget e rivleresimit
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorRivleresimi(string error, params object[] args)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoRivleresimi(string infoMessage, params object[] args)
        {
        }
        #endregion

        #region Loget e OTC

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogInfoOTC(string mesazh)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorOTC(Exception ex)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogErrorOTC(string ex)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogOTC(string mesazhi, object vleraPerTuLoguar)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogOTC(string mesazhi, object parametri, object rezultati)
        {
        }

        #endregion

        #region Enter/Exit Logging
        private static void LogMethod(Action<string> loggerMethod, string msgStart, bool isInfo, params object[] args)
        {
            //StackTrace trace = new StackTrace(true);  // need `true` for getting file and line info
        }

        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogEnter(Action<string> loggerMethod, params object[] args)
        {
        }
        [Conditional("IMB_LOG")] // trupi eshte i komentuar: thirrjet (dhe argumentat) hiqen nga kompilimi
        public static void LogExit(Action<string> loggerMethod, params object[] args)
        {
        }
        #endregion
    }
}
