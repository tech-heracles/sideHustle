using System;
using System.Diagnostics;
using DbCore.IMBUtils.Logging;
using NLog;
using System.Collections.Generic;

namespace DbCore
{
    [Serializable]
    public class MyException : Exception
    {
        public MyException(string message) : this(message, true) { }
        public MyException(string message, Dictionary<string, object> dic) : this(message, true)
        {

            foreach (var item in dic)
                Data[item.Key] = item.Value;
        }
        public MyException(string message, bool logPergjigje = true) : base(message)
        {
            if (logPergjigje)
                ImbLogger.Log(LogLevel.Error, new StackFrame(1), Message);
        }
        public MyException(Logger logu, Exception ex)
        {
            logu.Error(ex.Message);
        }

        public MyException(string message, Exception innerException) : base(string.Format("{0} ex:{1} ", message, innerException))
        {
            ImbLogger.Log(LogLevel.Error, new StackFrame(1), Message);
        }

        public MyException(string message, params object[] args) : base(string.Format(message, args))
        {
            ImbLogger.Log(LogLevel.Error, new StackFrame(1), Message);
        }

        /// <summary>
        /// konstruktor i cili perdoret ne rastin kur ke informacion te plote per te loguar,ne menyre te tille qe logu te jete kuptimplote,
        /// perdoret ne rastin qe je ne exceptionin fundor
        /// </summary>
        /// <param name="loger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public MyException(Logger loger, string message, params object[] args) : base(string.Format(message, args))
        {
            loger.Error(Message);
            //TODO getson hiq varesine qe kane librarite qe do e perdorin kete nga nlog
            //  ImbLogger.Log(LogLevel.Error, new StackFrame(1), Message);
        }
    }
}
