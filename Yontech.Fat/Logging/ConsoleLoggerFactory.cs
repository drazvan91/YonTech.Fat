using System.Collections.Generic;

namespace Yontech.Fat.Logging
{
    public class ConsoleLoggerFactory : BaseLoggerFactory
    {
        public ConsoleLoggerFactory() : base()
        {
        }

        public ConsoleLoggerFactory(LogLevel logLevel, Dictionary<string, LogLevel> logLevelConfigs)
            : base(logLevel, logLevelConfigs) { }

        protected override ILogger CreateLoggerInstance(string componentName, LogLevel logLevel)
        {
            return new ConsoleLogger(componentName, logLevel);
        }
    }
}
