using Yontech.Fat.Configuration;

namespace Alfa
{
    public class Config2 : FatConfig
    {
        public Config2()
        {
            AddChrome(new ChromeFatConfig());
        }
    }
}
