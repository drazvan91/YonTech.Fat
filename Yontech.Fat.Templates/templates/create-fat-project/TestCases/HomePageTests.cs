using Yontech.Fat;
using FatFramework.Sample.Pages;
using FatFramework.Sample.PageSections;
using System;

namespace FatFramework.Sample.TestCases
{
    public class HomePageTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HomePage homePage { get; set; }
        HeaderSection headerSection { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate("https://angular.realworld.io/");
            headerSection.SignInLink.Click();
            headerSection.LogoLink.Click();
        }

        public void Test1()
        {
            headerSection.SignInLink.ShouldHaveText("Sign in");
            headerSection.LogoLink.ShouldHaveText("conduit");

            homePage.Banner.Title.ShouldContainText("conduit");
            homePage.Banner.Description.ShouldContainText("A place to share your Angular knowledge.");
        }

        public void Test2()
        {
            WebBrowser.SimulateSlowConnection(500);

            homePage.TagList.TagWithText("money").Click();
            homePage.TagList.TagWithText("butt").Click();
            homePage.TagList.TagWithText("test").Click();

            WebBrowser.SimulateFastConnection();

            homePage.TagList.TagWithText("japan").Click();
            homePage.TagList.TagWithText("cars").Click();
            homePage.TagList.TagWithText("sushi").Click();
        }

        public void Test3()
        {
            homePage.TagList.TagWithText("happiness").Click();

            homePage.ArticleList.ArticleAtPosition(0).TitleText.ShouldContainText("hadakj");
            homePage.ArticleList.ArticleAtPosition(0).Tags.ShouldContainTag("happiness");
        }
    }
}