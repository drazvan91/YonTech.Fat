using System;
using System.Collections.Generic;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Logging
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        private readonly LogLevel _defaultLogLevel;
        private readonly Dictionary<string, LogLevel> _logLevelConfigs;
        public ConsoleLoggerFactory(LogLevel logLevel, Dictionary<string, LogLevel> logLevelConfigs)
        {
            _defaultLogLevel = logLevel;
            _logLevelConfigs = logLevelConfigs;
        }

        public ILogger Create<T>(T forObject)
        {
            var componentName = forObject.GetType().FullName.Replace("Yontech.", "").Replace("YonTech.", "");
            var logLevel = _defaultLogLevel;

            if (this._logLevelConfigs.TryGetValue(componentName, out LogLevel value))
            {
                logLevel = value;
            }

            return new ConsoleLogger(componentName, logLevel);
        }
    }
}
