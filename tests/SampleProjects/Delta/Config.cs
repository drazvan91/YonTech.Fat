using Yontech.Fat;

namespace Delta
{
    public class Config : FatConfig
    {
        public Config()
        {
            Browser = BrowserType.Chrome;
            DriversFolder = "delta_drivers";
        }
    }
}
