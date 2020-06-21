using Yontech.Fat.Configuration;

namespace Alfa
{
    public class Config3 : FatConfig
    {
        public Config3()
        {
            AddChrome(new ChromeFatConfig());
            AddFirefox();

            BrowserConfig.AutomaticDriverDownload = true;
            BrowserConfig.RunInBackground = true;
        }
    }
}
