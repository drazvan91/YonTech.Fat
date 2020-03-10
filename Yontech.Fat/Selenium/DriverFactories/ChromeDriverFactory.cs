using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Yontech.Fat.Configuration;
using Yontech.Fat.Logging;

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
        public static IWebDriver Create(ILoggerFactory loggerFactory, string driverPath, BrowserStartOptions startOptions, bool useRemote)
        {
            var chromeOptions = useRemote ? CreateRemoteOptions(startOptions) : CreateOptions(startOptions);

            IWebDriver driver = null;
            try
            {
                driver = CreateDriver(driverPath, chromeOptions);
            }
            catch (DriverServiceNotFoundException) when (startOptions.AutomaticDriverDownload)
            {
                new ChromeDriverDownloader(loggerFactory, startOptions.ChromeVersion).Download(driverPath).Wait();

                driver = CreateDriver(driverPath, chromeOptions);
            }

            if (driver != null && useRemote == false)
            {
                // this is a hotfix because selenium --start-maximized doesn't work (see below)
                if (startOptions.StartMaximized)
                {
                    driver.Manage().Window.Maximize();
                }
            }

            return driver;
        }

        private static IWebDriver CreateDriver(string driverPath, ChromeOptions chromeOptions)
        {
            // for some reason if we use ChromeDriver instead of RemoteWebDriver there is a performance issue on 
            // initial load, we believe that is because of dotnetcore
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath);
            service.Port = 5556; // Some port value.
            service.Start();
            IWebDriver webDriver = new RemoteWebDriver(new Uri("http://127.0.0.1:5556"), chromeOptions);
            return webDriver;

            // this is how it should be done.
            // var webDriver = new ChromeDriver(driverPath, chromeOptions);
            // return webDriver;
        }
        private static ChromeOptions CreateRemoteOptions(BrowserStartOptions startOptions)
        {
            return new ChromeOptions()
            {
                DebuggerAddress = startOptions.RemoteDebuggerAddress
            };
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

            return chromeOptions;
        }
    }
}
