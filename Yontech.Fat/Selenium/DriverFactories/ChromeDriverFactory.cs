using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Yontech.Fat.Configuration;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal static class ChromeDriverFactory
    {
        /// <summary>
        /// Creates a web driver.
        /// </summary>
        /// <param name="driverPath">Path to the folder containg the web driver.</param>
        /// <param name="startOptions">Null allowed. Providing null falls back to default BrowserStartOptions.</param>
        /// <returns>An instance of Chrome Driver.</returns>
        public static RemoteWebDriver Create(string driverPath, BrowserStartOptions startOptions)
        {
            var chromeOptions = CreateOptions(startOptions);

            // In the past there was one issue with dotnetcore 1.0 on windows which created 
            // performance issues. We need to investigate, if it still happens then 
            // we should use the RemoteWebDriver instead (code below)
            // if not then we should remove this comment and code below
            // ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath);
            // service.Port = 5555; // Some port value.
            // service.Start();
            //IWebDriver webDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:5555"), chromeOptions);


            ChromeDriver driver = null;
            try
            {
                driver = CreateDriver(driverPath, chromeOptions);
            }
            catch (DriverServiceNotFoundException) when (startOptions.AutomaticDriverDownload)
            {
                new ChromeDriverDownloader().Download(driverPath).Wait();

                driver = CreateDriver(driverPath, chromeOptions);
            }

            if (driver != null)
            {
                // this is a hotfix because selenium --start-maximized doesn't work (see below)
                if (startOptions.StartMaximized)
                {
                    driver.Manage().Window.Maximize();
                }
            }



            return driver;
        }

        private static ChromeDriver CreateDriver(string driverPath, ChromeOptions chromeOptions)
        {
            var webDriver = new ChromeDriver(driverPath, chromeOptions);
            return webDriver;
        }

        private static ChromeOptions CreateOptions(BrowserStartOptions startOptions)
        {
            startOptions = startOptions ?? new BrowserStartOptions();

            var chromeOptions = new ChromeOptions();

            if (startOptions.RunHeadless)
            {
                chromeOptions.AddArguments("--headless");
            }

            if (startOptions.StartMaximized)
            {
                // this looks like it is not working, might be deprecated by selenium
                chromeOptions.AddArgument("--start-maximized");
            }

            var height = startOptions.InitialSize.Height;
            var width = startOptions.InitialSize.Width;
            if (height > 0 && height > 0)
            {
                chromeOptions.AddArgument($"--window-size={height},{width}");
            }

            if (startOptions.DisablePopupBlocking)
            {
                chromeOptions.AddArgument("--disable-popup-blocking");
            }

            // chromeOptions.AddAdditionalCapability(CapabilityType.SupportsWebStorage, 1);
            return chromeOptions;
        }
    }
}
