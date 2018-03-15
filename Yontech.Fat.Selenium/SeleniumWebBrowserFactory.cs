using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Yontech.Fat.Selenium
{
    public class SeleniumWebBrowserFactory:IWebBrowserFactory
    {
        public SeleniumWebBrowserFactory()
        {
        }

        public IWebBrowser Create(BrowserType browserType)
        {
            IWebDriver webDriver = null;

            switch (browserType)
            {
                case BrowserType.Chrome:
                    webDriver = new ChromeDriver(Path.Combine(Environment.CurrentDirectory, "Drivers"));
                    
                    break;
                default:
                    throw new NotSupportedException();
            }

            SeleniumWebBrowser browser = new SeleniumWebBrowser(webDriver, browserType);
            return browser;
        }
    }
}
