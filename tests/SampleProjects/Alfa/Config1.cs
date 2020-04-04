using Yontech.Fat;
using Yontech.Fat.Logging;

namespace Alfa
{
    public class Config1 : FatConfig
    {
        public Config1()
        {
            Browser = BrowserType.Chrome;
            RunInBackground = true;
            DriversFolder = "alfa1_drivers";
            LogLevel = LogLevel.Debug;
        }
    }
}
