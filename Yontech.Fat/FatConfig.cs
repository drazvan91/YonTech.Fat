using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Interceptors;
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
        public string DriversFolder { get; set; } = "drivers";
        public bool AutomaticDriverDownload { get; set; } = true;
        public Size InitialSize { get; set; }
        public bool StartMaximized { get; set; } = false;
        public List<FatInterceptor> Interceptors { get; set; } = new List<FatInterceptor>();
        public List<IBusyCondition> BusyConditions { get; set; } = new List<IBusyCondition>();

        public static FatConfig Clone(FatConfig options)
        {
            return new FatConfig()
            {
                Filter = options.Filter,
                Browser = options.Browser,
                DelayBetweenTestCases = options.DelayBetweenTestCases,
                DelayBetweenSteps = options.DelayBetweenSteps,
                RunInBackground = options.RunInBackground,
                DriversFolder = options.DriversFolder,
                AutomaticDriverDownload = options.AutomaticDriverDownload,
                InitialSize = options.InitialSize,
                StartMaximized = options.StartMaximized,
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
