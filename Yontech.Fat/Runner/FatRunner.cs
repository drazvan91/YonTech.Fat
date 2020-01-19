using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Runner.Results;

namespace Yontech.Fat.Runner
{

  public class FatRunner
  {
    private readonly FatTestDiscoverer _discoverer = new FatTestDiscoverer();
    private IWebBrowser _webBrowser;

    public void Run(FatRunOptions options)
    {

      var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory();
      this._webBrowser = factory.Create(Yontech.Fat.BrowserType.Chrome);
      this._webBrowser.Configuration.BusyConditions.Add(new DocumentReadyBusyCondition());
      this._webBrowser.Configuration.BusyConditions.Add(new PendingRequestsBusyCondition());
      this._webBrowser.Configuration.BusyConditions.Add(new InstructionDelayTimeBusyCondition(options.DelayBetweenInstructions));


      var iocService = new IocService(options.Assemblies, this._webBrowser);
      var strategy = RunStrategyFactory.Create(options);

      try
      {
        foreach (var collection in strategy)
        {
          this.ExecuteTestCollection(collection, options, iocService);
        }

        PrintResults(strategy);
      }
      catch (Exception ex)
      {
        Console.WriteLine("ERRRORRRRRRR");
        Console.WriteLine((ex.InnerException ?? ex).StackTrace);
      }
      finally
      {
        this._webBrowser.Close();
        this._webBrowser = null;
      }
    }

    private void PrintResults(List<TestCollectionRunResult> strategy)
    {
      Console.WriteLine("Execution summary:");
      foreach (var collection in strategy)
      {
        Console.WriteLine("- {0}", collection.Name);

        foreach (var testClass in collection.TestClasses)
        {
          Console.WriteLine("    - {0}", testClass.Name);

          foreach (var testCase in testClass.TestCases)
          {
            Console.WriteLine("        {0} {1} {2}", testCase.Result, testCase.Duration, testCase.ShortName);
          }
        }
      }
    }

    private void ExecuteTestCollection(TestCollectionRunResult collection, FatRunOptions options, IocService iocService)
    {
      foreach (var testClass in collection.TestClasses)
      {
        ExecuteTestClass(testClass, options, iocService);
      }
    }

    // private IEnumerable<TestCaseRunSummary> ExecuteAssembly(FatRunOptions options, Assembly assembly, IocService iocService)
    // {
    //   var results = new List<TestCaseRunSummary>();
    //   foreach (var type in types)
    //   {
    //     results.AddRange(this.ExecuteTestClass(options, type, serviceProvider));
    //   }

    //   return results;
    // }

    private void ExecuteTestClass(TestClassRunResult testClass, FatRunOptions options, IocService iocService)
    {
      var fatTest = iocService.GetService<FatTest>(testClass.Class) as FatTest;

      fatTest.BeforeAllTestCases();

      foreach (var testCase in testClass.TestCases)
      {
        ExecuteTestCase(fatTest, testCase, iocService);
        Thread.Sleep(options.WaitAfterEachTestCase);
      }

      fatTest.AfterAllTestCases();
    }

    private void ExecuteTestCase(FatTest testClassInstance, TestCaseRunResult testCase, IocService iocService)
    {
      var watch = Stopwatch.StartNew();

      testClassInstance.BeforeEachTestCase();
      _webBrowser.WaitForIdle();

      testCase.Method.Invoke(testClassInstance, new object[0]);
      _webBrowser.WaitForIdle();

      testClassInstance.AfterEachTestCase();
      _webBrowser.WaitForIdle();

      watch.Stop();

      testCase.Duration = watch.ElapsedMilliseconds;
      testCase.Result = TestCaseRunResult.ResultType.Success;
    }


  }
}