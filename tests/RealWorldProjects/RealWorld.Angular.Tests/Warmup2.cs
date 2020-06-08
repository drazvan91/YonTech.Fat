using Yontech.Fat.Logging;
using Yontech.Fat.Configuration;
using Yontech.Fat;

namespace RealWorld.Angular.Tests
{
    public class Warmup2 : FatWarmup
    {
        protected override void Warmup()
        {
            this.WebBrowser.Navigate("https://google.com");
        }
    }
}
