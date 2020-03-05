using Yontech.Fat;

namespace CreateConsoleFatProject
{
    public class Config : FatConfig
    {
        public Config()
        {
            Browser = BrowserType.Chrome;
            RunInBackground = true;
        }
    }
}
