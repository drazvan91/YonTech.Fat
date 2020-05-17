using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.Filters;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat.Configuration
{
    public class FirefoxFatConfig : BaseBrowserFatConfig
    {
        public bool? RunInBackground { get; set; }
        public string DriversFolder { get; set; }
        public bool? AutomaticDriverDownload { get; set; }
        public FirefoxVersion Version { get; set; } = FirefoxVersion.Latest;
        internal override BrowserType BrowserType { get => BrowserType.Firefox; }
    }
}
