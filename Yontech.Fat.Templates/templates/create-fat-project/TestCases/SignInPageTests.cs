using Yontech.Fat;
using FatFramework.Sample.Flows;
using FatFramework.Sample.Pages;
using FatFramework.Sample.PageSections;
using FatFramework.Sample.Data;

namespace FatFramework.Sample.TestCases
{
    public class SignInPageTests : FatTest
    {
        SignInPage signInPage { get; set; }
        HeaderSection headerSection { get; set; }
        SignInFlows signInFlows { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate("https://angular.realworld.io/");
            headerSection.SignInLink.Click();
            headerSection.LogoLink.Click();
        }

        public void Tdest1()
        {
            headerSection.SignInLink.Click();

            signInFlows.Login(Users.WrongUser);

            signInPage.ErrorMessage.ShouldContainText("email or password is invalid");

            signInPage.EmailTextInput.ClearText();
            signInPage.EmailTextInput.TypeKeys("another wrong email");
            signInPage.SignInButton.Click();

            signInPage.ErrorMessage.ShouldContainText("email or password is invalid");
        }

        public void Tdest_signin_successful()
        {
            headerSection.SignInLink.Click();
            signInFlows.Login(Users.Drazvan91);

            headerSection.UserNameLink.ShouldHaveText(Users.Drazvan91.UserName);
        }

        public void Test_domn_domn_sananltam()
        {

        }
    }
}