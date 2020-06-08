using Yontech.Fat;
using Yontech.Fat.Configuration;
using Yontech.Fat.Logging;

namespace CreateConsoleFatProject
{
    public class Config : FatConfig
    {
        public Config()
        {
            LogLevel = LogLevel.Debug;
            AddChrome(new ChromeFatConfig()
            {
                DriversFolder = "drivers-folder1",
            });

            AddFirefox();

            BrowserConfig.RunInBackground = true;
        }
    }
}
