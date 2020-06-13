#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat.Configuration
{
    public class FatConfigTimeouts
    {
        public int DefaultTimeout { get; set; } = 10000;
        public int FinderTimeout { get; set; } = 5000;
        public int WarmupTimeout { get; set; } = 120000;
    }
}
