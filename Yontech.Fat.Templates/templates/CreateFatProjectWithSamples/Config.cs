using Yontech.Fat;

namespace CreateFatProjectWithSamples
{
    public class Config : FatConfig
    {
        public Config()
        {
            Browser = BrowserType.Chrome;
            RunInBackground = true;
            LogLevel = Yontech.Fat.Logging.LogLevel.Info;
        }
    }
}
