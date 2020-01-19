using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yontech.Fat.Runner
{
  public class DiscoveredTestCase
  {
    public Type Class { get; set; }
    public MethodInfo Method { get; set; }
  }

  public class FatTestClass
  {
    public Type Class { get; set; }
    public List<DiscoveredTestCase> TestCases { get; set; }
  }

  public class FatTestDiscoverer
  {
    public IEnumerable<FatTestClass> GetTestClassesForAssembly(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      var allTestClasses = allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatTest));
      });

      foreach (var type in allTestClasses)
      {
        yield return new FatTestClass()
        {
          Class = type,
          TestCases = GetTestCasesForClass(type).ToList(),
        };
      }
    }

    public IEnumerable<Type> GetFatPages(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      var fatPages = allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatPage));
      });
      return fatPages;
    }

    public IEnumerable<Type> GetFatPageSections(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      var fatPages = allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatPageSection));
      });
      return fatPages;
    }

    public IEnumerable<Type> GetFatFlows(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      var fatPages = allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatFlow));
      });
      return fatPages;
    }

    public IEnumerable<DiscoveredTestCase> GetTestCasesForClass(Type testClass)
    {
      var allMethods = testClass.GetMethods();
      var testCases = allMethods.Where(method => method.Name.StartsWith("Test"));

      foreach (var method in testCases)
      {
        yield return new DiscoveredTestCase()
        {
          Class = testClass,
          Method = method
        };
      }
    }
  }
}