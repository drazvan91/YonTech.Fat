using System;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Logging
{
    internal class ConsoleLogger : ILogger
    {
        private readonly string _componentName;
        private readonly LogLevel _logLevel;
        private readonly ConsolePrinter _print;

        public ConsoleLogger(string componentName, LogLevel logLevel)
        {
            this._componentName = componentName;
            this._logLevel = logLevel;
            this._print = new ConsolePrinter();
        }

        private void PrintTag()
        {
            _print.PrintBackgroundGray("[{0}]", _componentName);
            _print.PrintNormal(" ");
        }

        public void Debug(string format, params object[] args)
        {
            if (_logLevel > LogLevel.Debug) return;

            PrintTag();
            _print.PrintGray(format, args);
            _print.Enter();
        }

        public void Info(string format, params object[] args)
        {
            if (_logLevel > LogLevel.Info) return;

            PrintTag();
            _print.PrintNormal(format, args);
            _print.Enter();
        }

        public void Warning(string format, params object[] args)
        {
            if (_logLevel > LogLevel.Warning) return;

            PrintTag();
            _print.PrintYellow(format, args);
            _print.Enter();
        }

        public void Error(string format, params object[] args)
        {
            PrintTag();
            _print.PrintRed(format, args);
            _print.Enter();
        }

        public void Error(Exception exception, bool includeInnerExceptions = true)
        {
            PrintTag();
            _print.PrintException(exception, includeInnerExceptions);
            _print.Enter();
        }
    }
}
