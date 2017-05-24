using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    webDriver = new ChromeDriver(@"C:\techboard\YonTech.Fat\seleniumdrivers");
                    
                    break;
                default:
                    throw new NotSupportedException();
            }

            SeleniumWebBrowser browser = new SeleniumWebBrowser(webDriver, browserType);
            return browser;
        }
    }
}
