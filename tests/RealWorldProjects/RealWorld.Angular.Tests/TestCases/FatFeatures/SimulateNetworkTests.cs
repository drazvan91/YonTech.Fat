using Yontech.Fat;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using System;
using Yontech.Fat.DataSources;
using RealWorld.Angular.Tests.Data;
using System.Diagnostics;
using Yontech.Fat.Labels;

namespace RealWorld.Angular.Tests.TestCases.FatFeatures
{
    public class SimulateNetworkTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HomePage homePage { get; set; }
        HeaderSection headerSection { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
        }

        [InlineData("test")]
        [SkipFirefox]
        public void Test_simulate_slow_connetion(string tag)
        {
            WebBrowser.Configuration.DefaultTimeout = 20000;
            WebBrowser.Configuration.FinderTimeout = 20000;
            WebBrowser.SimulateSlowConnection(5000);
            var watch = Stopwatch.StartNew();

            homePage.TagList.TagWithText(tag).Click();
            homePage.ArticleList.ArticleAtPosition(0).Tags.ShouldContainTag(tag);

            watch.Stop();
            FailIf(watch.ElapsedMilliseconds < 5000, "Simulate slow connection doesnt work. Ellapsed time: {0}", watch.ElapsedMilliseconds);
        }
    }
}
