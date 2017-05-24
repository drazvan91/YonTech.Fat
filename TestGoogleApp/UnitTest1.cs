using System;
using System.Threading;
using Xunit;
using Yontech.Fat;
using Yontech.Fat.Selenium;
using Yontech.Fat.WebControls;

namespace TestGoogleApp
{
    public class UnitTest1: BaseAccountConnectTest
    {
        private readonly SearchPageHandler searchPage;

        public UnitTest1(AccountTestContext testContext) : base(testContext)
        {
             Browser.Navigate("http://google.ro");
             this.searchPage = new SearchPageHandler(Browser);
        }

        [Fact]
        public void Test1()
        {
            searchPage.SearchTextBox.SendKeys("dotnet");
            Thread.Sleep(5000);
        }
    }

    class SearchPageHandler : BasePageHandler
    {
        public SearchPageHandler(IWebBrowser webBrowser) : base(webBrowser)
        {
        }

        public ITextBoxControl SearchTextBox
        {
            get
            {
                return ControlFinder.TextBox("#lst-ib");
            }
        }
    }
   
    public class AccountTestContext: TestContext
    {
        public AccountTestContext()
        {
            var browserFactory = new SeleniumWebBrowserFactory();
            base.Browser = browserFactory.Create(BrowserType.Chrome);
        }
    }

    [CollectionDefinition("AccountConnect")]
    public class TestContextCollection : ICollectionFixture<AccountTestContext>
    {
    }

    [Collection("AccountConnect")]
    public class BaseAccountConnectTest
    {
        protected IWebBrowser Browser
        {
            get
            {
                return _testContext.Browser;
            }
        }

        private readonly TestContext _testContext;

        public BaseAccountConnectTest(AccountTestContext testContext)
        {
            _testContext = testContext;
        }
    }
}
