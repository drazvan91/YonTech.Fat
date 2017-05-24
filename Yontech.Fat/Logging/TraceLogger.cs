using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Yontech.Fat.Logging
{
    public class TraceLogger:ILogger
    {
        public static void Write(string messageFormat, params string[] args)
        {
            Trace.WriteLine(string.Format(messageFormat, args));
        }

        void ILogger.Write(string message, params string[] args)
        {
            Write(message, args);
        }
    }
}
