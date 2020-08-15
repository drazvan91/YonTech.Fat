using Yontech.Fat;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using System;
using Yontech.Fat.DataSources;
using RealWorld.Angular.Tests.Data;
using System.Diagnostics;
using Yontech.Fat.Labels;
using RealWorld.Angular.Tests.Flows;

namespace RealWorld.Angular.Tests.TestCases.FatFeatures
{
    public class KeyboardKeysTests : FatTest
    {
        HomePage homePage { get; set; }
        HeaderSection headerSection { get; set; }
        SignInFlows signInFlows { get; set; }

        SettingsPage settingsPage { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
        }

        public void Test_simulate_slow_connetion()
        {
            headerSection.SignInLink.Click();

            signInFlows.Login(Users.Drazvan91);
            headerSection.SettingsLink.Click();

            settingsPage.ImageUrlTextBox.TypeKeys("before");
            settingsPage.ImageUrlTextBox.TypeKeys(KeyboardKeys.Shift + "tt");
            settingsPage.ImageUrlTextBox.TypeKeys("after");

            settingsPage.ImageUrlTextBox.InnerTextShouldBe("beforeTTafter");
        }
    }
}
