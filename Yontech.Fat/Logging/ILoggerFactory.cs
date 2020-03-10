using System.Collections.Generic;

namespace Yontech.Fat.Logging
{
    public interface ILoggerFactory
    {
        LogLevel LogLevel { get; set; }
        Dictionary<string, LogLevel> LogLevelConfig { get; set; }

        ILogger Create<T>(T forObject);
    }
}
