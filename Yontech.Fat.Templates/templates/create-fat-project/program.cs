using System;
using Yontech.Fat.Runner;

using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat;

namespace FatFramework.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new FatRunOptions()
            {
                Browser = BrowserType.Chrome,
                Assemblies = new List<Assembly>(){
                    typeof(Program).Assembly
                },
                ScreenShotOnFailure = true,
                WaitAfterEachTestCase = 1,
                // DelayBetweenInstructions = 2000,
                ReportFileLocation = "",
                RunHeadless = false
            };

            FatRunner runner = new FatRunner();
            runner.Run(options);
        }
    }
}