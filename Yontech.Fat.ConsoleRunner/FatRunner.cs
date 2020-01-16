using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Yontech.Fat.ConsoleRunner
{

  public enum BrowserType
  {
    Chrome,
    IE11
  }
  public class FatRunOptions
  {
    public List<Assembly> Assemblies { get; set; }
    public BrowserType Browser { get; set; }
    public bool ScreenShotOnFailure { get; set; }
    public string ReportFileLocation { get; set; }
  }

  public class FatRunner
  {
    private readonly FatTestDiscoverer _discoverer = new FatTestDiscoverer();
    private IWebBrowser _webBrowser;

    public void Run(FatRunOptions options)
    {

      var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory();
      this._webBrowser = factory.Create(Yontech.Fat.BrowserType.Chrome);

      foreach (var assembly in options.Assemblies)
      {
        var results = this.ExecuteAssembly(assembly);
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

      this._webBrowser.Close();
    }

    private IEnumerable<TestCaseRunSummary> ExecuteAssembly(Assembly assembly)
    {
      var serviceCollection = new ServiceCollection();

      var types = _discoverer.GetTestClassesForAssembly(assembly);

      foreach (var type in types)
      {
        serviceCollection.AddTransient(type.Class, type.Class);

      }

      var fatPages = _discoverer.GetFatPages(assembly);
      foreach (var fat in fatPages)
      {
        serviceCollection.AddSingleton(fat);
      }

      var serviceProvider = serviceCollection.BuildServiceProvider();

      var results = new List<TestCaseRunSummary>();
      foreach (var type in types)
      {
        results.AddRange(this.ExecuteTestClass(type, serviceProvider));
      }

      return results;
    }

    private IEnumerable<TestCaseRunSummary> ExecuteTestClass(FatTestClass testClass, ServiceProvider serviceProvider)
    {
      var testClassInstance = GetPropertyInjectedService(testClass.Class, serviceProvider) as FatTest;
      testClassInstance.Browser = this._webBrowser;

      foreach (var testCase in testClass.TestCases)
      {
        yield return ExecuteTestCase(testClassInstance, testClass, testCase, serviceProvider);

      }
    }

    private TestCaseRunSummary ExecuteTestCase(object testClassInstance, FatTestClass testClass, FatTestCase testCase, ServiceProvider serviceProvider)
    {
      var watch = Stopwatch.StartNew();
      if (testClass.BeforeMethod != null)
      {
        testClass.BeforeMethod.Invoke(testClassInstance, new object[0]);
      }

      testCase.Method.Invoke(testClassInstance, new object[0]);

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
        return prop.PropertyType.IsSubclassOf(typeof(FatPage));
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

      return fatPageInstance;
    }
  }
}