using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.Filters;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat
{
    public abstract class BaseBrowserFatConfig
    {
        internal abstract BrowserType BrowserType { get; }
    }

    public class BrowserFatConfig
    {
        public bool RunInBackground { get; set; }
        public bool DisablePopupBlocking { get; set; }
        public string DriversFolder { get; set; } = "drivers";
        public Size InitialSize { get; set; }
        public bool StartMaximized { get; set; } = false;
        public bool AutomaticDriverDownload { get; set; } = true;
    }

    public class FirefoxFatConfig : BaseBrowserFatConfig
    {
        public bool? RunInBackground { get; set; }
        public string DriversFolder { get; set; }
        public bool? AutomaticDriverDownload { get; set; }
        public FirefoxVersion Version { get; set; } = FirefoxVersion.Latest;
        internal override BrowserType BrowserType { get => BrowserType.Firefox; }
    }

    public class BaseChromeFatConfig : BaseBrowserFatConfig
    {
        public bool? RunInBackground { get; set; }
        public string DriversFolder { get; set; }
        public bool? AutomaticDriverDownload { get; set; }
        public bool? DisablePopupBlocking { get; set; }
        public ChromeVersion Version { get; set; } = ChromeVersion.Latest;
        internal override BrowserType BrowserType { get => BrowserType.Chrome; }
    }

    public class ChromeFatConfig : BaseChromeFatConfig
    {
        public Size? InitialSize { get; set; }
        public bool? StartMaximized { get; set; }
    }

    public class RemoteChromeFatConfig : BaseChromeFatConfig
    {
        public string RemoteDebuggerAddress { get; set; }
    }

    public class FatConfigTimeouts
    {
        public int DefaultTimeout { get; set; } = 5000;
        public int FinderTimeout { get; set; } = 1000;
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

        public void AddChrome(ChromeFatConfig chromeConfig)
        {
            this.Browsers.Add(chromeConfig);
        }

        public void AddFirefox()
        {
            this.AddFirefox(new FirefoxFatConfig());
        }

        public void AddFirefox(FirefoxFatConfig firefoxConfig)
        {
            this.Browsers.Add(firefoxConfig);
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
