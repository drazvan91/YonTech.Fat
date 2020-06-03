using System;

namespace Yontech.Fat.Logging
{
    public class Log
    {
        public const string INFO = "INFO";
        public const string WARNING = "WARNING";
        public const string ERROR = "ERROR";
        public const string DEBUG = "DEBUG";

        public string Category { get; }
        public string Message { get; }
        public int? BrowserId { get; set; }
        public TimeSpan TimeSpan { get; }

        public Log(int? browserId, string category, string message, TimeSpan timespan)
        {
            this.Category = category;
            this.Message = message;
            this.TimeSpan = timespan;
            this.BrowserId = browserId;
        }
    }
}
