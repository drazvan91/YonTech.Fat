using Yontech.Fat;

namespace Beta
{
    public class Warmup2 : FatWarmup
    {
        protected override void Warmup()
        {
            LogInfo("Warming up Alfa project");

            WebBrowser.Navigate("https://google.com");

            LogInfo("Alfa project is warmed up");
        }
    }
}
