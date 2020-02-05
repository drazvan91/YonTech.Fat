using Yontech.Fat;
using CreateFatProjectWithSamples.Pages;
using CreateFatProjectWithSamples.PageSections;
using CreateFatProjectWithSamples.Data;
using Yontech.Fat.DataSources;

namespace CreateFatProjectWithSamples.TestCases
{
    public class HomePageTagsTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HomePage homePage { get; set; }
        HeaderSection headerSection { get; set; }

        public override void BeforeAllTestCases()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
            headerSection.SignInLink.Click();
            headerSection.LogoLink.Click();
        }

        [InlineData("money")]
        [InlineData("butt")]
        [InlineData("test")]
        public void Test_clicking_tags_at_slow_speed(string tag)
        {
            WebBrowser.SimulateSlowConnection(500);

            homePage.TagList.TagWithText(tag).Click();
            homePage.ArticleList.ArticleAtPosition(0).Tags.ShouldContainTag(tag);
        }

        [CsvFileData("./files/tags.csv")]
        public void Test_clicking_tags_at_high_speed(string tagName)
        {
            Log(tagName);
            WebBrowser.SimulateFastConnection();

            homePage.TagList.TagWithText(tagName).Click();
            homePage.ArticleList.ArticleAtPosition(0).Tags.ShouldContainTag(tagName);
        }
    }
}