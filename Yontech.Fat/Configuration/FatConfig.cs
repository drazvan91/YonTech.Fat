using System.Collections.Generic;
using Yontech.Fat.Filters;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat.Configuration
{
    public abstract class BaseBrowserFatConfig
    {
        internal int BrowserId { get; set; }
        internal abstract BrowserType BrowserType { get; }
    }

    public class FatConfig
    {
        public ITestCaseFilter Filter { get; set; }
        public int DelayBetweenTestCases { get; set; }
        public int DelayBetweenSteps { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Info;
        public Dictionary<string, LogLevel> LogLevelConfig { get; set; } = new Dictionary<string, LogLevel>();
        public List<FatInterceptor> Interceptors { get; set; } = new List<FatInterceptor>();
        public List<FatBusyCondition> BusyConditions { get; set; } = new List<FatBusyCondition>();

        public FatConfigTimeouts Timeouts { get; set; } = new FatConfigTimeouts();

        public BrowserFatConfig BrowserConfig { get; } = new BrowserFatConfig();

        public void AddChrome()
        {
            this.AddChrome(new ChromeFatConfig());
        }

        public void AddChromeRemote(int port)
        {
            this.AddBrowserWithId(new RemoteChromeFatConfig("localhost", port));
        }

        public void AddChromeRemote(string host, int port)
        {
            this.AddBrowserWithId(new RemoteChromeFatConfig(host, port));
        }

        public void AddChrome(ChromeFatConfig chromeConfig)
        {
            this.AddBrowserWithId(chromeConfig);
        }

        public void AddFirefox()
        {
            this.AddFirefox(new FirefoxFatConfig());
        }

        public void AddFirefox(FirefoxFatConfig firefoxConfig)
        {
            this.AddBrowserWithId(firefoxConfig);
        }

        private void AddBrowserWithId(BaseBrowserFatConfig config)
        {
            config.BrowserId = this.Browsers.Count;
            this.Browsers.Add(config);
        }

        internal List<BaseBrowserFatConfig> Browsers { get; } = new List<BaseBrowserFatConfig>();

        internal void Log(ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.Create(this);
            logger.Info("Configuration file being used: {0}", this.GetType().FullName);
            logger.Info("Log level: {0}", logger.LogLevel);

            logger.Debug($@"
    Browsers: {Browsers.Count}, 
    DelayBetweenTestCases: {DelayBetweenTestCases}
    DelayBetweenSteps: {DelayBetweenSteps}
            ");
        }
    }
}
