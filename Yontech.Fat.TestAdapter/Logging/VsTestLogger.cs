using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Yontech.Fat.Logging;

namespace Yontech.Fat.TestAdapter.Logging
{
    internal class VsTestLogger : ILogger
    {
        private readonly string _componentName;
        private readonly LogLevel _logLevel;
        private readonly IMessageLogger vsLogger;

        public VsTestLogger(IMessageLogger vsLogger, string componentName, LogLevel logLevel)
        {
            this.vsLogger = vsLogger;
            this._componentName = componentName;
            this._logLevel = logLevel;
        }

        public LogLevel LogLevel => _logLevel;

        public void Debug(string format, params object[] args)
        {
            if (_logLevel > LogLevel.Debug) return;

            SendMessage(TestMessageLevel.Informational, format, args);
        }

        public void Error(string format, params object[] args)
        {
            SendMessage(TestMessageLevel.Error, format, args);
        }

        public void Error(Exception exception, bool includeInnerExceptions = true)
        {
            var lines = StackFrameHelper.GetJoinedStackLines(exception, includeInnerExceptions);
            SendMessage(TestMessageLevel.Error, lines);
        }

        public void Info(string format, params object[] args)
        {
            if (_logLevel > LogLevel.Info) return;

            SendMessage(TestMessageLevel.Informational, format, args);
        }

        public void Warning(string format, params object[] args)
        {
            if (_logLevel > LogLevel.Warning) return;

            SendMessage(TestMessageLevel.Warning, format, args);
        }

        void SendMessage(TestMessageLevel level, string format, params object[] args)
        {
            var message = string.Format(format, args);
            vsLogger.SendMessage(level, $"[Fat] {message}");
        }
    }
}
