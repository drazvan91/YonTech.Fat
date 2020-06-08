using Yontech.Fat.Logging;
using Yontech.Fat.Configuration;

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

            BrowserConfig.RunInBackground = true;
            BrowserConfig.AutomaticDriverDownload = true;

            LogLevel = LogLevel.Debug;
        }
    }
}
