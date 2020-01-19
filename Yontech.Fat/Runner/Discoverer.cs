using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yontech.Fat.Runner
{
  public static class Discoverer
  {
    public static IEnumerable<Type> GetFatPages(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      var fatPages = allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatPage));
      });
      return fatPages;
    }

    public static IEnumerable<Type> GetFatPageSections(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      var fatPages = allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatPageSection));
      });
      return fatPages;
    }

    public static IEnumerable<Type> GetFatFlows(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      var fatPages = allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatFlow));
      });
      return fatPages;
    }

    public static IEnumerable<Type> GetTestClassesForAssembly(Assembly assembly)
    {
      var allTypes = assembly.GetTypes();
      return allTypes.Where(type =>
      {
        return type.IsSubclassOf(typeof(FatTest));
      });
    }

    public static IEnumerable<MethodInfo> GetTestCasesForClass(Type testClass)
    {
      var allMethods = testClass.GetMethods();
      return allMethods.Where(method => method.Name.StartsWith("Test"));
    }
  }
}