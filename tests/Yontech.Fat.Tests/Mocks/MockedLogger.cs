using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Tests.Mocks
{
    public class LogEntry
    {
        public string ComponentName { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
    }

    public class MockedLoggerFactory : BaseLoggerFactory
    {
        public List<LogEntry> LogEntries { get; } = new List<LogEntry>();

        protected override ILogger CreateLoggerInstance(string componentName, LogLevel logLevel)
        {
            return new MockedLogger(componentName, logLevel, LogEntries);
        }

        public void AssertNoErrors()
        {
            Assert.DoesNotContain(LogEntries, log => log.LogLevel == LogLevel.Error);
        }

        public void AssertNoErrorOrWarning()
        {
            Assert.DoesNotContain(LogEntries, log => log.LogLevel >= LogLevel.Warning);
        }

        public void AssertContains(LogLevel level, string message)
        {

            var hasLog = LogEntries.Any(log =>
            {
                return log.LogLevel == level
                    && log.Message.Contains(message);
            });

            if (!hasLog)
            {
                var allLogs = this.GetLogs();
                Assert.True(hasLog, $"Test should have log '{message}' but instead it head: {allLogs}");
            }
        }

        public void PrintAllLogs()
        {
            foreach (var log in LogEntries)
            {
                Console.WriteLine("{0}: {1}", log.LogLevel, log.Message);
            }
        }

        private string GetLogs()
        {
            var logs = LogEntries.Select(testResult => testResult.Message);
            return string.Join(Environment.NewLine, logs);
        }
    }

    public class MockedLogger : ILogger
    {
        private readonly List<LogEntry> _logEntries;

        public MockedLogger(string componentName, LogLevel logLevel, List<LogEntry> logEntries)
        {
            this.ComponentName = componentName;
            this.LogLevel = logLevel;
            this._logEntries = logEntries;
        }

        public string ComponentName { get; private set; }
        public LogLevel LogLevel { get; set; }

        public void Debug(string format, params object[] args)
        {
            _logEntries.Add(new LogEntry()
            {
                ComponentName = ComponentName,
                LogLevel = LogLevel.Debug,
                Message = String.Format(format, args)
            });
        }

        public void Error(string format, params object[] args)
        {
            _logEntries.Add(new LogEntry()
            {
                ComponentName = ComponentName,
                LogLevel = LogLevel.Error,
                Message = String.Format(format, args)
            });
        }

        public void Error(Exception exception, bool includeInnerExceptions = true)
        {
            _logEntries.Add(new LogEntry()
            {
                ComponentName = ComponentName,
                LogLevel = LogLevel.Error,
                Message = exception.Message
            });
        }

        public void Info(string format, params object[] args)
        {
            _logEntries.Add(new LogEntry()
            {
                ComponentName = ComponentName,
                LogLevel = LogLevel.Info,
                Message = String.Format(format, args)
            });
        }

        public void Warning(string format, params object[] args)
        {
            _logEntries.Add(new LogEntry()
            {
                ComponentName = ComponentName,
                LogLevel = LogLevel.Warning,
                Message = String.Format(format, args)
            });
        }
    }
}
