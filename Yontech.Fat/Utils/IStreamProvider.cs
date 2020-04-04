using System.IO;
using System.Reflection;

namespace Yontech.Fat.Utils
{
    public interface IStreamProvider
    {
        Stream GetStream(string filename, Assembly relativeToAssembly);
        TextReader GetTextReader(string filename, Assembly relativeToAssembly);
        string GetFileLocation(string filename, Assembly relativeToAssembly);
    }
}
