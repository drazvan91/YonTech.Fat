using Yontech.Fat;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using System;
using Yontech.Fat.DataSources;
using RealWorld.Angular.Tests.Data;

namespace RealWorld.Angular.Tests.TestCases
{
    public class HomePageTagsTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HomePage homePage { get; set; }
        HeaderSection headerSection { get; set; }

        public override void BeforeAllTestCases()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
        }

        [InlineData("money")]
        [InlineData("butt")]
        [InlineData("test")]
        public void Test_clicking_tags_at_slow_speed(string tag)
        {
            WebBrowser.SimulateSlowConnection(1000);
            LogInfo("Run test with tag={0}", tag);

            homePage.TagList.TagWithText(tag).Click();
            homePage.ArticleList.ArticleAtPosition(0).Tags.ShouldContainTag(tag);
        }

        [CsvFileData("./files/tags.csv")]
        public void Test_clicking_tags_at_high_speed(string tagName, string tagId)
        {
            LogInfo("Run test with tag={0}", tagName);
            WebBrowser.SimulateFastConnection();

            homePage.TagList.TagWithText(tagName).Click();
            homePage.ArticleList.ArticleAtPosition(1).Tags.ShouldContainTag(tagName);
        }
    }
}