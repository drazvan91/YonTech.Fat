using System;
using System.Collections.Generic;
using System.Drawing;
using RealWorld.Angular.Tests.Interceptors;
using Yontech.Fat;
using Yontech.Fat.Logging;
using Yontech.Fat.Interceptors;

namespace RealWorld.Angular.Tests
{
    public class Config : FatConfig
    {
        public Config()
        {
            Browser = BrowserType.Chrome;
            AutomaticDriverDownloadChromeVersion = ChromeVersion.v80;
            RemoteDebuggerAddress = "localhost:9222";
            RunInBackground = false;
            LogLevel = LogLevel.Debug;
            InitialSize = new Size(900, 900);
        }
    }
}
