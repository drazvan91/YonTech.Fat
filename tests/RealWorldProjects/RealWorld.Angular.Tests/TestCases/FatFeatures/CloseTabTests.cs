using Yontech.Fat;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using RealWorld.Angular.Tests.Data;
using RealWorld.Angular.Tests.Flows;

namespace RealWorld.Angular.Tests.TestCases.FatFeatures
{
    public class CloseTabTests : FatTest
    {
        HomePage homePage { get; set; }
        HeaderSection headerSection { get; set; }
        SignInFlows signInFlows { get; set; }

        SettingsPage settingsPage { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
        }

        public void Test_open_new_tab_then_left_open()
        {
            headerSection.SignInLink.Click();

            WebBrowser.OpenNewTab("https://google.com");
            WebBrowser.OpenNewTab("https://facebook.com");

            WebBrowser.Tab(0).Focus();
            signInFlows.Login(Users.Drazvan91);
            headerSection.SettingsLink.Click();
            settingsPage.LogoutButton.Click();
        }

        public void Test_open_then_close_tab()
        {
            headerSection.SignInLink.Click();
            var currentTab = WebBrowser.CurrentTab;
            var temporaryTab = WebBrowser.OpenNewTab(Urls.HOME_PAGE).Focus();

            headerSection.SignInLink.Click();
            signInFlows.Login(Users.Drazvan91);
            headerSection.SettingsLink.Click();
            settingsPage.ImageUrlTextBox.TypeKeys("hello there from tab 2");
            settingsPage.LogoutButton.Click();

            currentTab.Focus();

            headerSection.SignInLink.Click();
            signInFlows.Login(Users.Drazvan91);
            headerSection.SettingsLink.Click();

            WebBrowser.Tab(1).Focus();
            WebBrowser.Navigate("https://facebook.com");

            currentTab.Focus();
            temporaryTab.Close();

            settingsPage.ImageUrlTextBox.TypeKeys("hello there from tab 1");
            settingsPage.LogoutButton.Click();
        }
    }
}
