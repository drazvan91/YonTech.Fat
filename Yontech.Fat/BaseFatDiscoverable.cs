using System;
using System.Threading;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;

namespace Yontech.Fat
{
    public class BaseFatDiscoverable
    {
        internal protected IWebBrowser WebBrowser { get; internal set; }
        internal LogsSink LogsSink { get; set; }
        internal ILogger Logger { get; set; }
        internal bool SinkableLogs { get; set; } = true;

        protected void LogInfo(string format, params object[] args)
        {
            AddSinkLog(Log.INFO, format, args);
            Logger.Info(format, args);
        }

        protected void LogError(string format, params object[] args)
        {
            AddSinkLog(Log.ERROR, format, args);
            Logger.Error(format, args);
        }

        protected void LogWarning(string format, params object[] args)
        {
            AddSinkLog(Log.WARNING, format, args);
            Logger.Warning(format, args);
        }

        protected void LogDebug(string format, params object[] args)
        {
            AddSinkLog(Log.DEBUG, format, args);
            Logger.Debug(format, args);
        }

        internal void AddSinkLog(string logLevel, string format, params object[] args)
        {
            if (SinkableLogs)
            {
                LogsSink.Add(logLevel, format, args);
            }
        }

        protected void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        protected void Fail(string messageFormat, params object[] args)
        {
            throw new FatAssertException(messageFormat, args);
        }
        protected void FailIf(bool condition, string messageFormat, params object[] args)
        {
            if (condition)
            {
                throw new FatAssertException(messageFormat, args);
            }
        }

        protected void FailIf(Func<bool> condition, string messageFormat, params object[] args)
        {
            if (condition())
            {
                throw new FatAssertException(messageFormat, args);
            }
        }

        protected void WaitForTrue(Func<bool> condition)
        {
            Waiters.Waiter.WaitForConditionToBeTrue(condition, WebBrowser.Configuration.DefaultTimeout);
        }
    }
}
