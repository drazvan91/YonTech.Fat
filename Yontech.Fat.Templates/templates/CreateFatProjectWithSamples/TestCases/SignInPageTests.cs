using Yontech.Fat;
using CreateFatProjectWithSamples.Flows;
using CreateFatProjectWithSamples.Pages;
using CreateFatProjectWithSamples.PageSections;
using CreateFatProjectWithSamples.Data;

namespace CreateFatProjectWithSamples.TestCases
{
    public class SignInPageTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HeaderSection headerSection { get; set; }
        SignInFlows signInFlows { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
            headerSection.SignInLink.Click();
            headerSection.LogoLink.Click();
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
        }
    }
}