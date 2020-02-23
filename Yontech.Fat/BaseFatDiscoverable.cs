using System;
using System.Threading;
using Yontech.Fat.Logging;

namespace Yontech.Fat
{
    public class BaseFatDiscoverable
    {
        internal protected IWebBrowser WebBrowser { get; internal set; }
        internal LogsSink LogsSink { get; set; }

        protected void LogInfo(string format, params object[] args)
        {
            LogsSink.Add(Log.INFO, format, args);
        }

        protected void LogError(string format, params object[] args)
        {
            LogsSink.Add(Log.ERROR, format, args);
        }

        protected void LogWarning(string format, params object[] args)
        {
            LogsSink.Add(Log.WARNING, format, args);
        }

        protected void LogDebug(string format, params object[] args)
        {
            LogsSink.Add(Log.DEBUG, format, args);
        }

        protected void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}