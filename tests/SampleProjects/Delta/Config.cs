using Yontech.Fat.Configuration;

namespace Delta
{
    public class Config : FatConfig
    {
        public Config()
        {
            AddChrome();

            BrowserConfig.DriversFolder = "delta_drivers";
        }
    }
}
