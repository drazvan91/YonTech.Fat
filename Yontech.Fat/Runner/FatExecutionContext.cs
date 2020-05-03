using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Runner
{
    public class FatExecutionContext
    {
        public IAssemblyDiscoverer AssemblyDiscoverer { get; set; }
        public ILoggerFactory LoggerFactory { get; set; }
        public IStreamProvider StreamReaderProvider { get; set; }
        public FatConfig Config { get; set; }
    }
}
