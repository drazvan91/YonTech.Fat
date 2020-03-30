using Yontech.Fat;
using RealWorld.Angular.Tests.Flows;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using RealWorld.Angular.Tests.Data;

namespace RealWorld.Angular.Tests.TestCases
{
    public class SignInPageTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HeaderSection headerSection { get; set; }
        SignInFlows signInFlows { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
        }

        public void Test_error_messages()
        {
            headerSection.SignInLink.Click();

            signInFlows.Login(Users.WrongUser);

            signInPage.ErrorMessage.ShouldContainText("email or password is invalid");

            signInPage.EmailTextInput.ClearAndTypeKeys("another wrong email");
            signInPage.SignInButton.Click();

            signInPage.ErrorMessage.ShouldContainText("email or password is invalid");
        }

        public void Test_signin_successful()
        {
            headerSection.SignInLink.Click();
            signInFlows.Login(Users.Drazvan91);

            headerSection.UserNameLink.ShouldHaveText(Users.Drazvan91.UserName);

            WebBrowser.Navigate(Urls.HOME_PAGE);
            headerSection.UserNameLink.ShouldHaveText(Users.Drazvan91.UserName);

            WebBrowser.Navigate(Urls.HOME_PAGE);
        }
    }
}