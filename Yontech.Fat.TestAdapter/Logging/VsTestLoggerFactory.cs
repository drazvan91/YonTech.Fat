using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Yontech.Fat.Logging;

namespace Yontech.Fat.TestAdapter.Logging
{
    internal class VsTestLoggerFactory : ILoggerFactory
    {
        public LogLevel LogLevel { get; set; }
        public Dictionary<string, LogLevel> LogLevelConfig { get; set; } = new Dictionary<string, LogLevel>();
        private readonly IMessageLogger _vsLogger;
        public VsTestLoggerFactory(IMessageLogger vsLogger)
        {
            _vsLogger = vsLogger;
        }

        public ILogger Create<T>(T forObject)
        {
            var componentName = forObject.GetType().FullName.Replace("Yontech.", "").Replace("YonTech.", "");
            var logLevel = LogLevel;

            if (this.LogLevelConfig.TryGetValue(componentName, out LogLevel value))
            {
                logLevel = value;
            }

            return new VsTestLogger(_vsLogger, componentName, logLevel);
        }
    }
}
