using System;

namespace Yontech.Fat.Logging
{
    public interface ILogger
    {
        void Debug(string debug, params object[] args);
        void Info(string format, params object[] args);
        void Warning(string format, params object[] args);
        void Error(string format, params object[] args);
        void Error(Exception exception, bool includeInnerExceptions = true);
    }
}
