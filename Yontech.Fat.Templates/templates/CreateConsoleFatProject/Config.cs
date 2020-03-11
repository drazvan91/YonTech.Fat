using Yontech.Fat;
using Yontech.Fat.Logging;

namespace CreateConsoleFatProject
{
    public class Config : FatConfig
    {
        public Config()
        {
            Browser = BrowserType.Chrome;
            LogLevel = LogLevel.Debug;
            RunInBackground = true;
        }
    }
}
