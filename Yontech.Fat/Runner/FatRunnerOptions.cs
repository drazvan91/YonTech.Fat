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

    public class FatRunOptions
    {
        public List<Assembly> Assemblies { get; set; }
        public BrowserType Browser { get; set; }
        // public bool ScreenShotOnFailure { get; set; }
        // public string ReportFileLocation { get; set; }
        public int DelayBetweenTestCases { get; set; }
        public int DelayBetweenSteps { get; set; }
        public bool RunInBackground { get; set; }

        public IEnumerable<FatInterceptor> Interceptors { get; set; }
    }

}