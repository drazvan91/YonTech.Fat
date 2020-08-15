using Yontech.Fat.Logging;
using Yontech.Fat.Configuration;
using Yontech.Fat;
using RealWorld.Angular.Tests.Data;

namespace RealWorld.Angular.Tests
{
    public class Warmup1 : FatWarmup
    {
        protected override void Warmup()
        {
            this.WebBrowser.Navigate(Urls.HOME_PAGE);
        }
    }
}
