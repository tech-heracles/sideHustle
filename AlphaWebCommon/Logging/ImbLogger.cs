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
        public static void Log(LogLevel level, StackFrame stackFrame, string message)
        {
            //var method = stackFrame.GetMethod();

            //var eventLogInfo = new LogEventInfo(level, $"{method.DeclaringType?.FullName}.{method.Name}", message);
            //eventLogInfo.Properties["DataBase"] = ServerName;

            //var logger = LogManager.GetLogger($"{method.DeclaringType?.FullName}.{method.Name}");
            //logger.Log(eventLogInfo);
        }

        private static void Log(LogLevel level, Exception ex, string message = null)
        {
            //var customMessage = string.IsNullOrEmpty(message) ? ex.Message : message;
            //message = $"errorMessage : {customMessage} ex : {ex.ToString()}";

            //Log(level, new StackFrame(2), message);
        }

        private static void Log(LogLevel level, string message)
        {
            //Log(level, new StackFrame(2), message);
        }

        public static void Error(Exception ex)
        {
            //Log(LogLevel.Error, ex);
        }
        public static void Error(Exception ex, string message)
        {
            //Log(LogLevel.Error, ex, message);
        }
        public static void Error(string message, Exception ex)
        {
            //Log(LogLevel.Error, ex, message);
        }
        public static void Error(string errorMesage)
        {
            //Log(LogLevel.Error, errorMesage);
        }
        public static void Warn(Exception ex)
        {
            //Log(LogLevel.Warn, ex);
        }
        public static void Warn(string warnMessage)
        {
            //Log(LogLevel.Warn, warnMessage);
        }
        public static void Info(string infoMessage)
        {
            //Log(LogLevel.Info, infoMessage);
        }
        public static void Trace(string traceMessage)
        {
            //Log(LogLevel.Trace, traceMessage);
        }

        public static void Fatal(Exception ex)
        {
            //Log(LogLevel.Fatal, ex);
        }

        public static void Fatal(string fatalErrorMessage)
        {
            //Log(LogLevel.Fatal, fatalErrorMessage);
        }
        #endregion

        #region Log Traces
        public static void LogTrace(string message)
        {
            //LogEventInfo traceEvent = new LogEventInfo(LogLevel.Trace, LogTraces, message);
            //traceEvent.Properties["DataBase"] = ServerName;
            //_traceLogger.Log(traceEvent);
        }
        #endregion

        //shtim
        #region Loget e shitjeve

        public static void LogErrorShitje(string error)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogShitje, error);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_shitjeLogger.Log(errorEvent);
        }
        public static void LogWarningShitje(string warning)
        {
            //LogEventInfo warnEvent = new LogEventInfo(LogLevel.Warn, LogShitje, warning);
            //warnEvent.Properties["DataBase"] = ServerName;
            //_shitjeLogger.Log(warnEvent);
        }
        public static void LogTraceShitje(string trace)
        {
            //LogEventInfo traceEvent = new LogEventInfo(LogLevel.Trace, LogShitje, trace);
            //traceEvent.Properties["DataBase"] = ServerName;
            //_shitjeLogger.Log(traceEvent);
        }

        #endregion

        #region Loget e Buxhetimit

        public static void LogErrorBuxhetimi(Exception error)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogBuxhetimi, error.Message);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_buxhetimiLogger.Log(errorEvent);
        }
        public static void LogWarningBuxhetimi(string warning)
        {
            //LogMethod(WarnBuxhetimi, string.Empty, false, warning);
        }
        private static void WarnBuxhetimi(string warning)
        {
            //LogEventInfo warningEvent = new LogEventInfo(LogLevel.Warn, LogBuxhetimi, warning);
            //warningEvent.Properties["DataBase"] = ServerName;
            //_buxhetimiLogger.Log(warningEvent);
        }
        public static void LogTraceBuxhetimi(string trace)
        {
            //LogEventInfo traceEvent = new LogEventInfo(LogLevel.Trace, LogBuxhetimi, trace);
            //traceEvent.Properties["DataBase"] = ServerName;
            //_buxhetimiLogger.Log(traceEvent);
        }
        #endregion

        #region Loget e importit

        public static void LogErrorImporti(string error)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Info, LogImportiName, error);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_importiLogger.Log(errorEvent);
        }
        public static void LogInfoImporti(string infoMessage)
        {
            //LogEventInfo intoEvent = new LogEventInfo(LogLevel.Info, LogImportiName, infoMessage);
            //intoEvent.Properties["DataBase"] = ServerName;
            //_importiLogger.Log(intoEvent);
        }

        #endregion

        #region Loget e webapi
        public static void LogErrorWebApi(string error, params object[] args)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Info, LogWebApi, CultureInfo.InvariantCulture, error, args);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_webApiLogger.Log(errorEvent);
        }
        public static void LogInfoWebApi(string infoMessage, params object[] args)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogWebApi, CultureInfo.InvariantCulture, infoMessage, args);
            //infoEvent.Properties["DataBase"] = ServerName;
            //_webApiLogger.Log(infoEvent);
        }
        #endregion

        #region Loget e BRM

        public static void LogErrorBrm(string error)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogBrmName, error);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_brmLogger.Log(errorEvent);
        }
        public static void LogErrorBrm(Exception ex, string error)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogBrmName, error + " " + ex.Message);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_brmLogger.Log(errorEvent);
        }
        public static void LogErrorBrm(string error, params object[] args)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogBrmName, CultureInfo.InvariantCulture, error, args);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_brmLogger.Log(errorEvent);
        }
        public static void LogErrorBrm(Exception ex, string error, params object[] args)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogBrmName, CultureInfo.InvariantCulture, error + " " + ex.Message, args);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_brmLogger.Log(errorEvent);
        }
        public static void LogInfoBrm(string infoMessage)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogBrmName, infoMessage);
            //infoEvent.Properties["DataBase"] = ServerName;
            //_brmLogger.Log(infoEvent);
        }
        public static void LogInfoBrm(string infoMessage, params object[] args)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogBrmName, CultureInfo.InvariantCulture, infoMessage, args);
            //infoEvent.Properties["DataBase"] = ServerName;
            //_brmLogger.Log(infoEvent);
        }
        #endregion

        #region Loget e promocioneve
        public static void LogErrorPromocione(string error)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogPromocioneshName, error);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_promoLogger.Log(errorEvent);
        }
        public static void LogErrorPromocione(string error, params object[] args)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogPromocioneshName, CultureInfo.InvariantCulture, error, args);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_promoLogger.Log(errorEvent);
        }
        public static void LogInfoPromocione(string infoMessage)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogPromocioneshName, infoMessage);
            //infoEvent.Properties["DataBase"] = ServerName;
            //_promoLogger.Log(infoEvent);
        }
        public static void LogInfoPromocione(string infoMessage, params object[] args)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogPromocioneshName, CultureInfo.InvariantCulture, infoMessage, args);
            //infoEvent.Properties["DataBase"] = ServerName;
            //_promoLogger.Log(infoEvent);
        }
        #endregion

        #region Loget e rivleresimit
        public static void LogErrorRivleresimi(string error, params object[] args)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogRivleresimi, CultureInfo.InvariantCulture, error, args);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_rivleresimiLogger.Log(errorEvent);
        }
        public static void LogInfoRivleresimi(string infoMessage, params object[] args)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogRivleresimi, CultureInfo.InvariantCulture, infoMessage, args);
            //infoEvent.Properties["DataBase"] = ServerName;
            //_rivleresimiLogger.Log(infoEvent);
        }
        #endregion

        #region Loget e OTC

        public static void LogInfoOTC(string mesazh)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogOTCName, mesazh);
            //infoEvent.Properties["DataBase"] = ServerName;
            //_otcLogger.Log(infoEvent);
        }
        public static void LogErrorOTC(Exception ex)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogOTCName, ex.Message);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_otcLogger.Log(errorEvent);
        }
        public static void LogErrorOTC(string ex)
        {
            //LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, LogOTCName, ex);
            //errorEvent.Properties["DataBase"] = ServerName;
            //_otcLogger.Log(errorEvent);
        }
        public static void LogOTC(string mesazhi, object vleraPerTuLoguar)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogOTCName, $"{mesazhi} {JsonConvert.SerializeObject(vleraPerTuLoguar)}");
            //infoEvent.Properties["DataBase"] = ServerName;
            //_otcLogger.Log(infoEvent);
        }
        public static void LogOTC(string mesazhi, object parametri, object rezultati)
        {
            //LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, LogOTCName, $"{mesazhi} parametri: {JsonConvert.SerializeObject(parametri)} rezultati :{JsonConvert.SerializeObject(rezultati)}");
            //infoEvent.Properties["DataBase"] = ServerName;
            //_otcLogger.Log(infoEvent);
        }

        #endregion

        #region Enter/Exit Logging
        private static void LogMethod(Action<string> loggerMethod, string msgStart, bool isInfo, params object[] args)
        {
            //StackTrace trace = new StackTrace(true);  // need `true` for getting file and line info
            //if (trace.FrameCount > 2)
            //{
            //    string ns = trace.GetFrame(2).GetMethod().DeclaringType.Namespace;
            //    string typeName = trace.GetFrame(2).GetMethod().DeclaringType.Name;
            //    string args_string = Newtonsoft.Json.JsonConvert.SerializeObject(args);
            //    string meParametra = (!isInfo && args.Length == 0) ? "" : $"me parametra: ";
            //    loggerMethod($"{msgStart} {ns}.{typeName}.{trace.GetFrame(2).GetMethod().Name} {meParametra}{args_string}");
            //}
        }

        public static void LogEnter(Action<string> loggerMethod, params object[] args)
        {
            //LogMethod(loggerMethod, ">> Filloi metoda", true, args);
        }
        public static void LogExit(Action<string> loggerMethod, params object[] args)
        {
            //LogMethod(loggerMethod, "Perfundoi metoda", true, args);
        }
        #endregion
    }
}
