using System;
using Yontech.Fat;
using Yontech.Fat.Runner;

using System.Collections.Generic;
using System.Reflection;

namespace CreateFatProjectWithSamples
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
                }
            };

            FatRunner runner = new FatRunner();
            runner.Run(options);
        }
    }
}