using Yontech.Fat;

namespace Alfa
{
    public class Config1 : FatConfig
    {
        public Config1()
        {
            Browser = BrowserType.Chrome;
            DriversFolder = "alfa1_drivers";
        }
    }
}
