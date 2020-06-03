using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Yontech.Fat.Logging
{
    internal class LogsSink
    {
        private List<Log> _logs = new List<Log>();
        private Stopwatch _stopWatch = Stopwatch.StartNew();

        public void Reset()
        {
            this._stopWatch.Restart();
            this._logs.Clear();
        }

        public void Add(int? browserId, string category, string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            var escapedMessage = EscapeInvalidHexChars(message);

            this._logs.Add(new Log(browserId, category, escapedMessage, _stopWatch.Elapsed));
        }

        public IEnumerable<Log> GetLogs()
        {
            return _logs.AsReadOnly();
        }

        /// copied from Xunit.Sdk.TestOutputHelper
        private static string EscapeInvalidHexChars(string s)
        {
            var builder = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if (ch == '\0')
                {
                    builder.Append("\\0");
                }
                else if (ch < 32 && !char.IsWhiteSpace(ch))
                {
                    // C0 control char
                    builder.AppendFormat(@"\x{0}", (+ch).ToString("x2"));
                }
                else if (char.IsSurrogatePair(s, i))
                {
                    // For valid surrogates, append like normal
                    builder.Append(ch);
                    builder.Append(s[++i]);
                }
                else if (char.IsSurrogate(ch) || ch == '\uFFFE' || ch == '\uFFFF')
                {
                    // Check for stray surrogates/other invalid chars
                    builder.AppendFormat(@"\x{0}", (+ch).ToString("x4"));
                }
                else
                {
                    builder.Append(ch); // Append the char like normal
                }
            }

            return builder.ToString();
        }
    }
}
