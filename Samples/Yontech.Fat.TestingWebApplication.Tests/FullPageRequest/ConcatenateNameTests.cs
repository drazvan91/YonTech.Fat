using System;
using Xunit;
using Yontech.Fat.Selenium;
using Yontech.Fat.TestingWebApplication.Tests.Config;
using Yontech.Fat.TestingWebApplication.Tests.FullPageRequest.Pages;
using Yontech.Fat.TestingWebApplication.Tests.Runtime;

namespace Yontech.Fat.TestingWebApplication.Tests.FullPageRequestTest
{
    public class ConcatenateNameTests: BaseFatTest
    {
        private readonly FullPageRequestDemoPage fullPageRequestDemoPage;

        public ConcatenateNameTests(BaseTestContext context):base(context)
        {
            base.Browser.Navigate(PageUrls.FullPageRequestPage);

            this.fullPageRequestDemoPage = new FullPageRequestDemoPage(base.Browser);
        }

        [Fact]
        public void ConcatenateName_MissingFirstName()
        {
            fullPageRequestDemoPage.ConcatenateButton.Click();
            fullPageRequestDemoPage.ConcatenatedNameResultSection.InnerTextShouldBe("Missing first name");
        }

        [Fact]
        public void ConcatenateName_MissingLastName()
        {
            fullPageRequestDemoPage.FirstNameTextBox.SendKeys("Some value");
            fullPageRequestDemoPage.ConcatenateButton.Click();
            fullPageRequestDemoPage.ConcatenatedNameResultSection.InnerTextShouldBe("Missing last name");
        }


        [Fact]
        public void ConcatenateName_ConcatenationWorks()
        {
            fullPageRequestDemoPage.FirstNameTextBox.SendKeys("John");
            fullPageRequestDemoPage.LastNameTextBox.SendKeys("Doe");
            fullPageRequestDemoPage.ConcatenateButton.Click();
            fullPageRequestDemoPage.ConcatenatedNameResultSection.InnerTextShouldBe("John Doe");
        }
    }
}
