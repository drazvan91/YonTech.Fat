﻿using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.Filters;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat
{
    public class FatConfigTimeouts
    {
        public int DefaultTimeout { get; set; } = 5000;
        public int FinderTimeout { get; set; } = 1000;
    }

    public class DefaultFatConfig : FatConfig
    {
    }

    public abstract class FatConfig
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

        public FatConfigTimeouts Timeouts { get; set; } = new FatConfigTimeouts();

        internal void Log(ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.Create(this);
            logger.Info("Configuration file being used: {0}", this.GetType().FullName);
            logger.Info("Log level: {0}", logger.LogLevel);

            logger.Debug($@"
    Browser: {Browser}, 
    RemoteDebuggerAddress: {RemoteDebuggerAddress}
    DriversFolder: {DriversFolder}
    AutomaticDriverDownload: {AutomaticDriverDownload}
    DelayBetweenTestCases: {DelayBetweenTestCases}
    DelayBetweenSteps: {DelayBetweenSteps}
            ");
        }
    }
}
