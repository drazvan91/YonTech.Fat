using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using OpenQA.Selenium.IE;

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
            string driversPath = Path.Combine(Environment.CurrentDirectory, "Drivers");

            switch (browserType)
            {
                case BrowserType.Chrome:
                    webDriver = new ChromeDriver(driversPath);
                    break;
                case BrowserType.InternetExplorer:
                    webDriver = new InternetExplorerDriver(driversPath);
                    break;
                default:
                    throw new NotSupportedException();
            }

            SeleniumWebBrowser browser = new SeleniumWebBrowser(webDriver, browserType);
            return browser;
        }
    }
}
