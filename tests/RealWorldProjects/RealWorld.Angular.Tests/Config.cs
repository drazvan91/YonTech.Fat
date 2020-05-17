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
            AddChrome(new ChromeFatConfig()
            {
                DriversFolder = "chrome_driver",
            });

            AddFirefox(new FirefoxFatConfig()
            {
                DriversFolder = "firefox_driver",
            });

            BrowserConfig.RunInBackground = false;
            BrowserConfig.AutomaticDriverDownload = true;

            LogLevel = LogLevel.Debug;
        }
    }
}
