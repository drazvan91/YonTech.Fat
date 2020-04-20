using System;

namespace Yontech.Fat.TestAdapter
{
    public static class Constants
    {
        public const string ExecutorUriString = "executor://Yontech.Fat/netcoreapp";
        public static readonly Uri ExecutorUri = new Uri(Constants.ExecutorUriString);
    }
}
