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
            string message = String.Format(format, args);
            LogsSink.Add(Log.INFO, message);
        }

        protected void LogError(string format, params object[] args)
        {
            string message = String.Format(format, args);
            LogsSink.Add(Log.ERROR, message);
        }

        protected void LogWarning(string format, params object[] args)
        {
            string message = String.Format(format, args);
            LogsSink.Add(Log.WARNING, message);
        }

        protected void LogDebug(string format, params object[] args)
        {
            string message = String.Format(format, args);
            LogsSink.Add(Log.DEBUG, message);
        }

        protected void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}