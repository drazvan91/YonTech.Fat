using Yontech.Fat;
using Yontech.Fat.Logging;

namespace Alfa
{
    public class Config1 : FatConfig
    {
        public Config1()
        {
            AddChrome(new ChromeFatConfig()
            {
                RunInBackground = true,
            });

            BrowserConfig.DriversFolder = "alfa1_drivers";

            LogLevel = LogLevel.Debug;
        }
    }
}
