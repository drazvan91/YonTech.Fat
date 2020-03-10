using System;
using System.Collections.ObjectModel;

namespace Yontech.Fat.Logging
{
    public interface ILogger
    {
        LogLevel LogLevel { get; }
        void Debug(string format, params object[] args);
        void Info(string format, params object[] args);
        void Warning(string format, params object[] args);
        void Error(string format, params object[] args);
        void Error(Exception exception, bool includeInnerExceptions = true);
    }
}
