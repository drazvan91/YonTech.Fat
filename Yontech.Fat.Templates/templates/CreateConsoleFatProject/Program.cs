using Yontech.Fat.ConsoleRunner;

namespace CreateConsoleFatProject
{
    class Program
    {
        static int Main(string[] args)
        {
            FatConsoleRunner runner = new FatConsoleRunner(new Config());
            var results = runner.Run();

            // or a specific TestClass
            // var results = runner.Run<HomePageTests>();

            return results.Failed;
        }
    }
}
