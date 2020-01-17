using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yontech.Fat.ConsoleRunner
{
  public class FatTestCase
  {
    public Type Class { get; set; }
    public MethodInfo Method { get; set; }
  }

  public class FatTestClass
  {
    public Type Class { get; set; }
    public List<FatTestCase> TestCases { get; set; }
    public MethodInfo BeforeMethod { get; set; }
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
          BeforeMethod = GetBeforeMethod(type)
        };
      }
    }

    private MethodInfo GetBeforeMethod(Type type)
    {
      var allMethods = type.GetMethods();
      return allMethods.FirstOrDefault(method => method.Name == "BeforeEachTest");
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

    public IEnumerable<FatTestCase> GetTestCasesForClass(Type testClass)
    {
      var allMethods = testClass.GetMethods();
      var testCases = allMethods.Where(method => method.Name.StartsWith("Test"));

      foreach (var method in testCases)
      {
        yield return new FatTestCase()
        {
          Class = testClass,
          Method = method
        };
      }
    }
  }
}