using System.Collections.Generic;
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
        public IEnumerable<FatInterceptor> Interceptors { get; set; }

        public static FatConfig Clone(FatConfig options)
        {
            return new FatConfig()
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

        public static FatConfig Default()
        {
            return new FatConfig()
            {
            };
        }
    }
}