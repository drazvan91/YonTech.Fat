using Yontech.Fat;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using System;
using Yontech.Fat.DataSources;
using RealWorld.Angular.Tests.Data;
using System.Diagnostics;

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
        public void Test_simulate_slow_connetion(string tag)
        {
            WebBrowser.SimulateSlowConnection(5000);
            var watch = Stopwatch.StartNew();

            homePage.TagList.TagWithText(tag).Click();
            homePage.ArticleList.ArticleAtPosition(0).Tags.ShouldContainTag(tag);

            watch.Stop();
            FailIf(watch.ElapsedMilliseconds < 5000, "Simulate slow connection doesnt work. Ellapsed time: {0}", watch.ElapsedMilliseconds);
        }
    }
}