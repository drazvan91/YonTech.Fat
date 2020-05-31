using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.Filters;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat.Configuration
{
    public class BaseChromeFatConfig : BaseBrowserFatConfig
    {
        public bool? RunInBackground { get; set; }
        public string DriversFolder { get; set; }
        public bool? AutomaticDriverDownload { get; set; }
        public bool? DisablePopupBlocking { get; set; }
        public ChromeVersion Version { get; set; } = ChromeVersion.Latest;
        internal override BrowserType BrowserType { get => BrowserType.Chrome; }
    }

    public class ChromeFatConfig : BaseChromeFatConfig
    {
        public Size? InitialSize { get; set; }
        public bool? StartMaximized { get; set; }
    }

    public class RemoteChromeFatConfig : BaseChromeFatConfig
    {
        public RemoteChromeFatConfig(string host, int port)
        {
            this.RemoteDebuggerAddress = host + ":" + port;
        }

        public string RemoteDebuggerAddress { get; }
    }
}
