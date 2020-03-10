using System.Collections.Generic;

namespace Yontech.Fat.Logging
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        public LogLevel LogLevel { get; set; }
        public Dictionary<string, LogLevel> LogLevelConfig { get; set; }
        public ConsoleLoggerFactory() : this(LogLevel.Info, new Dictionary<string, LogLevel>())
        {
        }

        public ConsoleLoggerFactory(LogLevel logLevel, Dictionary<string, LogLevel> logLevelConfigs)
        {
            LogLevel = logLevel;
            LogLevelConfig = logLevelConfigs;
        }

        public ILogger Create<T>(T forObject)
        {
            var componentName = forObject.GetType().FullName.Replace("Yontech.", "").Replace("YonTech.", "");
            var logLevel = LogLevel;

            if (this.LogLevelConfig.TryGetValue(componentName, out LogLevel value))
            {
                logLevel = value;
            }

            return new ConsoleLogger(componentName, logLevel);
        }
    }
}
