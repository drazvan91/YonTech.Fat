using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Yontech.Fat.Logging
{
    public class LogsSink
    {

        private List<Log> _logs = new List<Log>();
        private Stopwatch stopWatch = Stopwatch.StartNew();


        public void Reset()
        {
            this.stopWatch.Restart();
            this._logs.Clear();
        }

        public void Add(string category, string message)
        {
            this._logs.Add(new Log(category, message, stopWatch.Elapsed));
        }

        public IEnumerable<Log> GetLogs()
        {
            return _logs.AsReadOnly();
        }
    }
}