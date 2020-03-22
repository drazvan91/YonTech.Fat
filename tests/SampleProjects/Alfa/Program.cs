
using Yontech.Fat.ConsoleRunner;

namespace Alfa
{
    class Program
    {
        static int Main(string[] args)
        {
            FatConsoleRunner runner = new FatConsoleRunner();
            return runner.Run().Failed;
        }
    }
}
