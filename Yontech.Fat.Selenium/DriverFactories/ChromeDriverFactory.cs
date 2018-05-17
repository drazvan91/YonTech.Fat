using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Yontech.Fat.Configuration;

namespace Yontech.Fat.Selenium.DriverFactories
{
    public static class ChromeDriverFactory
    {
        public static IWebDriver Create(string driverPath, BrowserStartOptions startOptions)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath);
            service.Port = 5555; // Some port value.
            service.Start();

            var chromeOptions = CreateOptions(startOptions);

            IWebDriver webDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:5555"), chromeOptions);
            return webDriver;
        }

        private static ChromeOptions CreateOptions(BrowserStartOptions startOptions)
        {
            var chromeOptions = new ChromeOptions();
            if (startOptions == null)
            {
                return chromeOptions;
            }

            if (startOptions.RunHeadless)
            {
                chromeOptions.AddArguments("--headless");
            }

            if(startOptions.StartMaximized)
            {
                chromeOptions.AddArgument("--start-maximized");
            }

            return chromeOptions;
        }
    }
}
