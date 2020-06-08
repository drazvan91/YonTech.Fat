using Yontech.Fat;
using Yontech.Fat.Configuration;

namespace CreateFatProjectWithSamples
{
    public class Config : FatConfig
    {
        public Config()
        {
            LogLevel = Yontech.Fat.Logging.LogLevel.Info;

            AddChrome();

            BrowserConfig.RunInBackground = true;
        }
    }
}
