using System;
using Yontech.Fat.Runner;

using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat;
using Yontech.Fat.Interceptors;
using RealWorld.Angular.Tests.Interceptors;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using RealWorld.Angular.Tests.TestCases;
using Yontech.Fat.ConsoleRunner;

namespace RealWorld.Angular.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new FatConsoleRunner();
            runner.Run();
        }
    }
}
