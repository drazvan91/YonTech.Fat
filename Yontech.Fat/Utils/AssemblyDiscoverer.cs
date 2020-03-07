using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yontech.Fat.Utils
{
    internal static class AssemblyDiscoverer
    {
        public static IEnumerable<Assembly> DiscoverAssemblies()
        {
            var refAssembyNames = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies();
            foreach (var asslembyNames in refAssembyNames)
            {
                Assembly.Load(asslembyNames);
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies;
        }
    }
}
