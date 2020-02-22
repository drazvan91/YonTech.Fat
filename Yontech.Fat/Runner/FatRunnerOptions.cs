using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Interceptors;

namespace Yontech.Fat.Runner
{
    public class FatRunnerOptions
    {
        public ITestCaseFilter Filter { get; set; }
        public BrowserType Browser { get; set; }
        // public bool ScreenShotOnFailure { get; set; }
        // public string ReportFileLocation { get; set; }
        public int DelayBetweenTestCases { get; set; }
        public int DelayBetweenSteps { get; set; }
        public bool RunInBackground { get; set; }
        public string DriversFolder { get; set; }
        public bool AutomaticDriverDownload { get; set; } = true;
        public IEnumerable<FatInterceptor> Interceptors { get; set; }

        public static FatRunnerOptions Clone(FatRunnerOptions options)
        {
            return new FatRunnerOptions()
            {
                Filter = options.Filter,
                Browser = options.Browser,
                DelayBetweenSteps = options.DelayBetweenSteps,
                AutomaticDriverDownload = options.AutomaticDriverDownload,
                DelayBetweenTestCases = options.DelayBetweenTestCases,
                DriversFolder = options.DriversFolder,
                Interceptors = options.Interceptors,
                RunInBackground = options.RunInBackground
            };
        }
    }
}