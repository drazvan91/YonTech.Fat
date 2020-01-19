using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.BusyConditions;

namespace Yontech.Fat.ConsoleRunner
{

  public class FatRunOptions
  {
    public List<Assembly> Assemblies { get; set; }
    public BrowserType Browser { get; set; }
    public bool ScreenShotOnFailure { get; set; }
    public string ReportFileLocation { get; set; }
    public int WaitAfterEachTestCase { get; set; }
    public int DelayBetweenInstructions { get; set; }
  }

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

      try
      {
        foreach (var assembly in options.Assemblies)
        {
          var results = this.ExecuteAssembly(options, assembly);
          Console.WriteLine("Execution summary:");
          foreach (var result in results)
          {
            if (result.Success)
            {
              Console.WriteLine("Success in {1}ms: {0}", result.ShortName, result.TimeEllapsed, result.Success);
            }
            else
            {
              Console.WriteLine("ERROR in {1}ms: {0}", result.ShortName, result.TimeEllapsed, result.Success);
            }
          }
        }
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

    private IEnumerable<TestCaseRunSummary> ExecuteAssembly(FatRunOptions options, Assembly assembly)
    {
      var serviceCollection = new ServiceCollection();

      var types = _discoverer.GetTestClassesForAssembly(assembly);

      foreach (var type in types)
      {
        serviceCollection.AddTransient(type.Class, type.Class);

      }

      var fatPages = _discoverer.GetFatPages(assembly);
      foreach (var page in fatPages)
      {
        serviceCollection.AddSingleton(page);
      }
      var fatPageSections = _discoverer.GetFatPageSections(assembly);
      foreach (var pageSections in fatPageSections)
      {
        serviceCollection.AddSingleton(pageSections);
      }
      var fatFlows = _discoverer.GetFatFlows(assembly);
      foreach (var flow in fatFlows)
      {
        serviceCollection.AddSingleton(flow);
      }

      var serviceProvider = serviceCollection.BuildServiceProvider();

      var results = new List<TestCaseRunSummary>();
      foreach (var type in types)
      {
        results.AddRange(this.ExecuteTestClass(options, type, serviceProvider));
      }

      return results;
    }

    private IEnumerable<TestCaseRunSummary> ExecuteTestClass(FatRunOptions options, FatTestClass testClass, ServiceProvider serviceProvider)
    {
      var testClassInstance = GetPropertyInjectedService(testClass.Class, serviceProvider) as FatTest;
      testClassInstance.Browser = this._webBrowser;

      testClassInstance.BeforeAllTestCases();

      foreach (var testCase in testClass.TestCases)
      {
        yield return ExecuteTestCase(testClassInstance, testClass, testCase, serviceProvider);
        Thread.Sleep(options.WaitAfterEachTestCase);
      }

      testClassInstance.AfterAllTestCases();
    }

    private TestCaseRunSummary ExecuteTestCase(FatTest testClassInstance, FatTestClass testClass, FatTestCase testCase, ServiceProvider serviceProvider)
    {
      var watch = Stopwatch.StartNew();

      testClassInstance.BeforeEachTestCase();
      _webBrowser.WaitForIdle();

      testCase.Method.Invoke(testClassInstance, new object[0]);
      _webBrowser.WaitForIdle();

      testClassInstance.AfterEachTestCase();
      _webBrowser.WaitForIdle();

      watch.Stop();

      return new TestCaseRunSummary()
      {
        ShortName = testCase.Method.Name,
        LongName = testCase.Class.Name + " " + testCase.Method.Name,
        TimeEllapsed = watch.ElapsedMilliseconds
      };
    }

    private object GetPropertyInjectedService(Type fatPageType, ServiceProvider serviceProvider)
    {
      return GetPropertyInjectedService(fatPageType, serviceProvider, new HashSet<string>());
    }

    private object GetPropertyInjectedService(Type fatPageType, ServiceProvider serviceProvider, HashSet<string> injectionContext)
    {
      injectionContext.Add(fatPageType.FullName);

      var fatPageInstance = serviceProvider.GetService(fatPageType);
      var properties = fatPageType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
      var fatPageProperties = properties.Where(prop =>
      {
        return prop.PropertyType.IsSubclassOf(typeof(FatPage)) || prop.PropertyType.IsSubclassOf(typeof(FatPageSection)) || prop.PropertyType.IsSubclassOf(typeof(FatFlow));
      });

      foreach (var prop in fatPageProperties)
      {

        if (prop.GetValue(fatPageInstance) == null)
        {
          object fatPageProp = null;
          if (injectionContext.Contains(prop.PropertyType.FullName))
          {

            fatPageProp = serviceProvider.GetService(prop.PropertyType);
          }
          else
          {
            fatPageProp = GetPropertyInjectedService(prop.PropertyType, serviceProvider, injectionContext);
          }
          prop.SetValue(fatPageInstance, fatPageProp);
        }
      }

      injectionContext.Remove(fatPageType.FullName);

      var sectionPage = fatPageInstance as FatPageSection;
      if (sectionPage != null)
      {
        sectionPage.ControlFinder = _webBrowser.ControlFinder;
      }

      var page = fatPageInstance as FatPage;
      if (page != null)
      {
        page._ = _webBrowser.ControlFinder;
      }

      return fatPageInstance;
    }
  }
}