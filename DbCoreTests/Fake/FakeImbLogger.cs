using DbCore.IMBUtils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.ComponentModel;

namespace DbCoreTests.Fake
{
    public class FakeImbLogger : ILogger
    {
        public LogFactory Factory
        {
            get
            {
                return null;
            }
        }

        public bool IsDebugEnabled
        {
            get
            {
                return true; 
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsTraceEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "";
            }
        }

        public event EventHandler<EventArgs> LoggerReconfigured;

        public void Debug([Localizable(false)] string message)
        {
            
        }

        public void Debug(LogMessageGenerator messageFunc)
        {
            
        }

        public void Debug(object value)
        {
            
        }

        public void Debug(string message, byte argument)
        {
            
        }

        public void Debug(string message, char argument)
        {
            
        }

        public void Debug(string message, bool argument)
        {
            
        }

        public void Debug(string message, int argument)
        {
            
        }

        public void Debug(string message, decimal argument)
        {
            
        }

        public void Debug(string message, ulong argument)
        {
            
        }

        public void Debug(string message, uint argument)
        {
            
        }

        public void Debug(string message, sbyte argument)
        {
            
        }

        public void Debug(string message, object argument)
        {
            
        }

        public void Debug([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Debug([Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Debug(Exception exception, [Localizable(false)] string message)
        {
            
        }

        public void Debug(string message, double argument)
        {
            
        }

        public void Debug(string message, float argument)
        {
            
        }

        public void Debug(string message, long argument)
        {
            
        }

        public void Debug(string message, string argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, object value)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, byte argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, uint argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, bool argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, object argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
        {
            
        }

        public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, string argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, double argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, float argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, char argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, long argument)
        {
            
        }

        public void Debug(IFormatProvider formatProvider, string message, int argument)
        {
            
        }

        public void Debug(string message, object arg1, object arg2)
        {
            
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Debug(string message, object arg1, object arg2, object arg3)
        {
            
        }

        public void Debug<T>(T value)
        {
            
        }

        public void Debug<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            
        }

        public void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void DebugException([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Error([Localizable(false)] string message)
        {
            
        }

        public void Error(LogMessageGenerator messageFunc)
        {
            
        }

        public void Error(object value)
        {
            
        }

        public void Error(string message, char argument)
        {
            
        }

        public void Error(string message, string argument)
        {
            
        }

        public void Error(string message, long argument)
        {
            
        }

        public void Error(string message, double argument)
        {
            
        }

        public void Error(string message, object argument)
        {
            
        }

        public void Error(string message, uint argument)
        {
            
        }

        public void Error(Exception exception, [Localizable(false)] string message)
        {
            
        }

        public void Error(string message, ulong argument)
        {
            
        }

        public void Error(string message, sbyte argument)
        {
            
        }

        public void Error(string message, decimal argument)
        {
            
        }

        public void Error(string message, float argument)
        {
            
        }

        public void Error(string message, int argument)
        {
            
        }

        public void Error(string message, byte argument)
        {
            
        }

        public void Error(string message, bool argument)
        {
            
        }

        public void Error([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Error([Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Error(IFormatProvider formatProvider, object value)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, char argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, int argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, uint argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, string argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, object argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, bool argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, decimal argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, double argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, float argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, long argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, byte argument)
        {
            
        }

        public void Error(IFormatProvider formatProvider, string message, ulong argument)
        {
            
        }

        public void Error(string message, object arg1, object arg2)
        {
            
        }

        public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Error(string message, object arg1, object arg2, object arg3)
        {
            
        }

        public void Error<T>(T value)
        {
            
        }

        public void Error<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            
        }

        public void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void ErrorException([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Fatal(LogMessageGenerator messageFunc)
        {
            
        }

        public void Fatal([Localizable(false)] string message)
        {
            
        }

        public void Fatal(object value)
        {
            
        }

        public void Fatal(string message, int argument)
        {
            
        }

        public void Fatal(string message, float argument)
        {
            
        }

        public void Fatal(string message, double argument)
        {
            
        }

        public void Fatal(string message, decimal argument)
        {
            
        }

        public void Fatal(string message, sbyte argument)
        {
            
        }

        public void Fatal(Exception exception, [Localizable(false)] string message)
        {
            
        }

        public void Fatal([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Fatal([Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Fatal(string message, uint argument)
        {
            
        }

        public void Fatal(string message, ulong argument)
        {
            
        }

        public void Fatal(string message, object argument)
        {
            
        }

        public void Fatal(string message, long argument)
        {
            
        }

        public void Fatal(string message, byte argument)
        {
            
        }

        public void Fatal(string message, string argument)
        {
            
        }

        public void Fatal(string message, char argument)
        {
            
        }

        public void Fatal(string message, bool argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, object value)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, int argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, string argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, bool argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
        {
            
        }

        public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, char argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, object argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, double argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, float argument)
        {
            
        }

        public void Fatal(IFormatProvider formatProvider, string message, long argument)
        {
            
        }

        public void Fatal(string message, object arg1, object arg2)
        {
            
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Fatal(string message, object arg1, object arg2, object arg3)
        {
            
        }

        public void Fatal<T>(T value)
        {
            
        }

        public void Fatal<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void FatalException([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Info(LogMessageGenerator messageFunc)
        {
            
        }

        public void Info([Localizable(false)] string message)
        {
            
        }

        public void Info(object value)
        {
            
        }

        public void Info(string message, object argument)
        {
            
        }

        public void Info(Exception exception, [Localizable(false)] string message)
        {
            
        }

        public void Info([Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Info([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Info(string message, uint argument)
        {
            
        }

        public void Info(string message, ulong argument)
        {
            
        }

        public void Info(string message, sbyte argument)
        {
            
        }

        public void Info(string message, string argument)
        {
            
        }

        public void Info(string message, int argument)
        {
            
        }

        public void Info(string message, long argument)
        {
            
        }

        public void Info(string message, double argument)
        {
            
        }

        public void Info(string message, decimal argument)
        {
            
        }

        public void Info(string message, float argument)
        {
            
        }

        public void Info(string message, char argument)
        {
            
        }

        public void Info(string message, byte argument)
        {
            
        }

        public void Info(string message, bool argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, object value)
        {
            
        }

        public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, bool argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, ulong argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, uint argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, object argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, decimal argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, double argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, float argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, long argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, int argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, string argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, byte argument)
        {
            
        }

        public void Info(IFormatProvider formatProvider, string message, char argument)
        {
            
        }

        public void Info(string message, object arg1, object arg2)
        {
            
        }

        public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Info(string message, object arg1, object arg2, object arg3)
        {
            
        }

        public void Info<T>(T value)
        {
            
        }

        public void Info<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            
        }

        public void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void InfoException([Localizable(false)] string message, Exception exception)
        {
            
        }

        public bool IsEnabled(LogLevel level)
        {
            return true;
        }

        public void Log(LogEventInfo logEvent)
        {
            
        }

        public void Log(Type wrapperType, LogEventInfo logEvent)
        {
            
        }

        public void Log(LogLevel level, [Localizable(false)] string message)
        {
            
        }

        public void Log(LogLevel level, LogMessageGenerator messageFunc)
        {
            
        }

        public void Log(LogLevel level, object value)
        {
            
        }

        public void Log(LogLevel level, string message, object argument)
        {
            
        }

        public void Log(LogLevel level, string message, sbyte argument)
        {
            
        }

        public void Log(LogLevel level, string message, byte argument)
        {
            
        }

        public void Log(LogLevel level, string message, uint argument)
        {
            
        }

        public void Log(LogLevel level, string message, ulong argument)
        {
            
        }

        public void Log(LogLevel level, string message, bool argument)
        {
            
        }

        public void Log(LogLevel level, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Log(LogLevel level, [Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Log(LogLevel level, string message, string argument)
        {
            
        }

        public void Log(LogLevel level, string message, int argument)
        {
            
        }

        public void Log(LogLevel level, string message, long argument)
        {
            
        }

        public void Log(LogLevel level, string message, float argument)
        {
            
        }

        public void Log(LogLevel level, string message, char argument)
        {
            
        }

        public void Log(LogLevel level, string message, double argument)
        {
            
        }

        public void Log(LogLevel level, string message, decimal argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, object value)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
        {
            
        }

        public void Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
        {
            
        }

        public void Log(LogLevel level, string message, object arg1, object arg2)
        {
            
        }

        public void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Log(LogLevel level, string message, object arg1, object arg2, object arg3)
        {
            
        }

        public void Log<T>(LogLevel level, T value)
        {
            
        }

        public void Log<TArgument>(LogLevel level, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Log<T>(LogLevel level, IFormatProvider formatProvider, T value)
        {
            
        }

        public void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void LogErrorBuxhetimi(string error)
        {
        }

        public void LogException(LogLevel level, [Localizable(false)] string message, Exception exception)
        {
            
        }

        public void LogInfoBuxhetimi(string info)
        {
        }

        public void LogWarningBuxhetimi(string warning)
        {
        }

        public void Swallow(Task task)
        {
            
        }

        public void Swallow(Action action)
        {
            
        }

        public T Swallow<T>(Func<T> func)
        {
            return default(T);
        }

        public T Swallow<T>(Func<T> func, T fallback)
        {
            return default(T);
        }

        public Task SwallowAsync(Func<Task> asyncAction)
        {
            return null;
        }

        public Task SwallowAsync(Task task)
        {
            return null;
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            return null;
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback)
        {
            return null;
        }

        public void Trace([Localizable(false)] string message)
        {
            
        }

        public void Trace(LogMessageGenerator messageFunc)
        {
            
        }

        public void Trace(object value)
        {
            
        }

        public void Trace(string message, char argument)
        {
            
        }

        public void Trace(string message, byte argument)
        {
            
        }

        public void Trace(string message, string argument)
        {
            
        }

        public void Trace(Exception exception, [Localizable(false)] string message)
        {
            
        }

        public void Trace(string message, int argument)
        {
            
        }

        public void Trace([Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Trace([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Trace(string message, long argument)
        {
            
        }

        public void Trace(string message, float argument)
        {
            
        }

        public void Trace(string message, double argument)
        {
            
        }

        public void Trace(string message, decimal argument)
        {
            
        }

        public void Trace(string message, object argument)
        {
            
        }

        public void Trace(string message, sbyte argument)
        {
            
        }

        public void Trace(string message, uint argument)
        {
            
        }

        public void Trace(string message, ulong argument)
        {
            
        }

        public void Trace(string message, bool argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, object value)
        {
            
        }

        public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, char argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, string argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, long argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, double argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, object argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, uint argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, float argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, int argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, byte argument)
        {
            
        }

        public void Trace(IFormatProvider formatProvider, string message, bool argument)
        {
            
        }

        public void Trace(string message, object arg1, object arg2)
        {
            
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Trace(string message, object arg1, object arg2, object arg3)
        {
            
        }

        public void Trace<T>(T value)
        {
            
        }

        public void Trace<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Trace<T>(IFormatProvider formatProvider, T value)
        {
            
        }

        public void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void TraceException([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Warn(LogMessageGenerator messageFunc)
        {
            
        }

        public void Warn([Localizable(false)] string message)
        {
            
        }

        public void Warn(object value)
        {
            
        }

        public void Warn(string message, long argument)
        {
            
        }

        public void Warn(string message, char argument)
        {
            
        }

        public void Warn(Exception exception, [Localizable(false)] string message)
        {
            
        }

        public void Warn(string message, float argument)
        {
            
        }

        public void Warn(string message, double argument)
        {
            
        }

        public void Warn([Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Warn([Localizable(false)] string message, Exception exception)
        {
            
        }

        public void Warn(string message, int argument)
        {
            
        }

        public void Warn(string message, decimal argument)
        {
            
        }

        public void Warn(string message, byte argument)
        {
            
        }

        public void Warn(string message, object argument)
        {
            
        }

        public void Warn(string message, bool argument)
        {
            
        }

        public void Warn(string message, sbyte argument)
        {
            
        }

        public void Warn(string message, uint argument)
        {
            
        }

        public void Warn(string message, string argument)
        {
            
        }

        public void Warn(string message, ulong argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, object value)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, long argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, float argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, decimal argument)
        {
            
        }

        public void Warn(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, object argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, uint argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, ulong argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, double argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, byte argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, string argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, int argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, bool argument)
        {
            
        }

        public void Warn(IFormatProvider formatProvider, string message, char argument)
        {
            
        }

        public void Warn(string message, object arg1, object arg2)
        {
            
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            
        }

        public void Warn(string message, object arg1, object arg2, object arg3)
        {
            
        }

        public void Warn<T>(T value)
        {
            
        }

        public void Warn<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Warn<T>(IFormatProvider formatProvider, T value)
        {
            
        }

        public void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            
        }

        public void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            
        }

        public void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            
        }

        public void WarnException([Localizable(false)] string message, Exception exception)
        {
            
        }
    }
}
