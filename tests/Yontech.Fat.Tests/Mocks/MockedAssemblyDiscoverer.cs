using System;
using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests
{
    public class MockedAssemblyDiscoverer : IAssemblyDiscoverer
    {
        private readonly Assembly[] _assemblies;

        public MockedAssemblyDiscoverer(params Assembly[] aseemblies)
        {
            this._assemblies = aseemblies;
        }

        public IEnumerable<Assembly> DiscoverAssemblies()
        {
            return _assemblies;
        }
    }
}
