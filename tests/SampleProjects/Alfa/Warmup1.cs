using Yontech.Fat;

namespace Alfa
{
    public class Warmup1 : FatWarmup
    {
        protected override void Warmup()
        {
            LogInfo("Warming up Alfa project for browser {0}", base.WebBrowser.BrowserType);

            WebBrowser.Navigate("https://google.com");

            LogInfo("Alfa project is warmed up");
        }
    }
}
