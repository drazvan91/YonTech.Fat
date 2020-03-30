using Yontech.Fat;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using System;
using RealWorld.Angular.Tests.Data;

namespace RealWorld.Angular.Tests.TestCases
{
    public class HomePageTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HomePage homePage { get; set; }
        HeaderSection headerSection { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
            headerSection.SignInLink.Click();
            headerSection.LogoLink.Click();
        }

        public void Test_header_elements_are_present()
        {
            headerSection.SignInLink.ShouldHaveText("Sign in");
            headerSection.LogoLink.ShouldHaveText("conduit");

            homePage.Banner.Title.ShouldContainText("conduit");
            homePage.Banner.Description.ShouldContainText("A place to share your Angular knowledge.");
        }
    }
}