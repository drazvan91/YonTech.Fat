using System.Diagnostics;

namespace Yontech.Fat.Utils
{
    public static class AssemblyVersionUtils
    {
        public static string GetFatVersion()
        {
            System.Reflection.Assembly assembly = typeof(AssemblyVersionUtils).Assembly;
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}
