using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yontech.Fat.TestingWebApplication.Tests.Config;
using Yontech.Fat.TestingWebApplication.Tests.FullPageRequest.Pages;
using Yontech.Fat.TestingWebApplication.Tests.Runtime;

namespace Yontech.Fat.TestingWebApplication.Tests.FullPageRequest
{
    public class LongRequestTests : BaseFatTest
    {
        private readonly FullPageRequestDemoPage fullPageRequestDemoPage;

        public LongRequestTests(BaseTestContext context):base(context)
        {
            base.Browser.Navigate(PageUrls.FullPageRequestPage);

            this.fullPageRequestDemoPage = new FullPageRequestDemoPage(base.Browser);
        }

        [Fact]
        public void LongTimeRequest_WaitForOneSecond()
        {
            fullPageRequestDemoPage.InfoMessageSection.ShouldNotBeVisible();

            fullPageRequestDemoPage.OneSecondRequestButton.Click();

            fullPageRequestDemoPage.InfoMessageSection.ShouldContainText("Request lasted 1000 miliseconds");
        }

        [Fact]
        public void LongTimeRequest_WaitForFiveSeconds()
        {
            fullPageRequestDemoPage.InfoMessageSection.ShouldNotBeVisible();

            fullPageRequestDemoPage.FiveSecondsRequestButton.Click();

            fullPageRequestDemoPage.InfoMessageSection.ShouldContainText("Request lasted 5000 miliseconds");
        }

        [Fact]
        public void LongTimeRequest_WaitForTenSeconds()
        {
            fullPageRequestDemoPage.InfoMessageSection.ShouldNotBeVisible();

            fullPageRequestDemoPage.TenSecondsRequestButton.Click();

            fullPageRequestDemoPage.InfoMessageSection.ShouldContainText("Request lasted 10000 miliseconds");
        }
    }
}
