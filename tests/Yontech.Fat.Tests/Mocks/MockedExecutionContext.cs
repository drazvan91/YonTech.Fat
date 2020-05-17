using System.Reflection;
using Yontech.Fat.Configuration;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.Mocks
{
    public class MockedExecutionContext : FatExecutionContext
    {
        public MockedLoggerFactory MockedLoggerFactory { get; }
        public MockedAssemblyDiscoverer MockedAssemblyDiscoverer { get; }
        public MockedExecutionContext(params Assembly[] assemblies) :
            this(null, assemblies)
        {
        }

        public MockedExecutionContext(FatConfig config, params Assembly[] assemblies)
        {
            this.LoggerFactory = this.MockedLoggerFactory = new MockedLoggerFactory();
            this.AssemblyDiscoverer = this.MockedAssemblyDiscoverer = new MockedAssemblyDiscoverer(assemblies);
            this.StreamReaderProvider = new StreamProvider(this.LoggerFactory);
            this.Config = config;
        }
    }
}
