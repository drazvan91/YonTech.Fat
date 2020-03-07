using System;
using System.Threading;
using Yontech.Fat.Logging;

namespace Yontech.Fat
{
    public class BaseFatDiscoverable
    {
        internal protected IWebBrowser WebBrowser { get; internal set; }
        internal LogsSink LogsSink { get; set; }
        internal ILogger Logger { get; set; }

        protected void LogInfo(string format, params object[] args)
        {
            LogsSink.Add(Log.INFO, format, args);
            Logger.Info(format, args);
        }

        protected void LogError(string format, params object[] args)
        {
            LogsSink.Add(Log.ERROR, format, args);
            Logger.Error(format, args);
        }

        protected void LogWarning(string format, params object[] args)
        {
            LogsSink.Add(Log.WARNING, format, args);
            Logger.Warning(format, args);
        }

        protected void LogDebug(string format, params object[] args)
        {
            LogsSink.Add(Log.DEBUG, format, args);
            Logger.Debug(format, args);
        }

        protected void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        protected void WaitForTrue(Func<bool> condition)
        {
            Waiters.Waiter.WaitForConditionToBeTrue(condition, WebBrowser.Configuration.DefaultTimeout);
        }
    }
}
