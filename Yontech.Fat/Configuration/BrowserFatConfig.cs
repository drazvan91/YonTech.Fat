using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.Filters;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat.Configuration
{
    public class BrowserFatConfig
    {
        public bool RunInBackground { get; set; }
        public bool DisablePopupBlocking { get; set; }
        public string DriversFolder { get; set; } = "drivers";
        public Size InitialSize { get; set; }
        public bool StartMaximized { get; set; } = false;
        public bool AutomaticDriverDownload { get; set; } = true;
    }
}
