using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yontech.Fat.Utils
{
    public class AssemblyDiscoverer : IAssemblyDiscoverer
    {
        public IEnumerable<Assembly> DiscoverAssemblies()
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
