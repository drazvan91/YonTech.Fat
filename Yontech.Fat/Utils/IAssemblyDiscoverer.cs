using System.Collections.Generic;
using System.Reflection;

namespace Yontech.Fat.Utils
{
    public interface IAssemblyDiscoverer
    {
        IEnumerable<Assembly> DiscoverAssemblies();
    }
}
