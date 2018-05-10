using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Yontech.Fat.Selenium
{
    public class SeleniumWebBrowserFactory : IWebBrowserFactory
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
                    webDriver = CreateChromeWebDriver(driversPath);
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

        private IWebDriver CreateChromeWebDriver(string driversPath)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driversPath);
            service.Port = 5555; // Some port value.
            service.Start();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");

            IWebDriver webDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:5555"), chromeOptions);
            return webDriver;
        }
    }
}
