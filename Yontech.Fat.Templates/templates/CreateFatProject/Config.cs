using Yontech.Fat;
using Yontech.Fat.Configuration;

namespace CreateFatProject
{
    public class Config : FatConfig
    {
        public Config()
        {
            AddChrome();

            BrowserConfig.RunInBackground = true;
        }
    }
}
