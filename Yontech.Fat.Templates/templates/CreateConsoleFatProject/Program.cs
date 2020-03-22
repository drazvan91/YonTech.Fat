using System;
using Yontech.Fat.Runner;

using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat;
using Yontech.Fat.Interceptors;
using Yontech.Fat.ConsoleRunner;
using CreateConsoleFatProject.TestCases;

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
