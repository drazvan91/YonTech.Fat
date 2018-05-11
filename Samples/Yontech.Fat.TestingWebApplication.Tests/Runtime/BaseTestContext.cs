using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yontech.Fat.Selenium;

namespace Yontech.Fat.TestingWebApplication.Tests.Runtime
{
    
    public class BaseTestContext:TestContext
    {
        public BaseTestContext()
        {
            var browserFactory = new SeleniumWebBrowserFactory();
            base.Browser = browserFactory.Create(BrowserType.Chrome);
        }
    }
}
