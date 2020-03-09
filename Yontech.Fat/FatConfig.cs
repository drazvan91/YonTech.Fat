using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat
{
    public class FatConfig
    {
        public ITestCaseFilter Filter { get; set; }
        public BrowserType Browser { get; set; } = BrowserType.Chrome;
        public int DelayBetweenTestCases { get; set; }
        public int DelayBetweenSteps { get; set; }
        public bool RunInBackground { get; set; }
        public bool DisablePopupBlocking { get; set; }
        public string DriversFolder { get; set; } = "drivers";
        public string RemoteDebuggerAddress { get; set; }
        public bool AutomaticDriverDownload { get; set; } = true;
        public ChromeVersion AutomaticDriverDownloadChromeVersion { get; set; } = ChromeVersion.Latest;
        public Size InitialSize { get; set; }
        public bool StartMaximized { get; set; } = false;
        public LogLevel LogLevel { get; set; } = LogLevel.Info;
        public Dictionary<string, LogLevel> LogLevelConfig { get; set; } = new Dictionary<string, LogLevel>();
        public List<FatInterceptor> Interceptors { get; set; } = new List<FatInterceptor>();
        public List<FatBusyCondition> BusyConditions { get; set; } = new List<FatBusyCondition>();

        public static FatConfig Clone(FatConfig options)
        {
            return new FatConfig()
            {
                Filter = options.Filter,
                Browser = options.Browser,
                DelayBetweenTestCases = options.DelayBetweenTestCases,
                DelayBetweenSteps = options.DelayBetweenSteps,
                RunInBackground = options.RunInBackground,
                DisablePopupBlocking = options.DisablePopupBlocking,
                DriversFolder = options.DriversFolder,
                RemoteDebuggerAddress = options.RemoteDebuggerAddress,
                AutomaticDriverDownload = options.AutomaticDriverDownload,
                AutomaticDriverDownloadChromeVersion = options.AutomaticDriverDownloadChromeVersion,
                InitialSize = options.InitialSize,
                StartMaximized = options.StartMaximized,
                LogLevel = options.LogLevel,
                LogLevelConfig = options.LogLevelConfig,
                Interceptors = options.Interceptors,
                BusyConditions = options.BusyConditions,
            };
        }

        public static FatConfig Default()
        {
            return new FatConfig()
            {
            };
        }
    }
}
