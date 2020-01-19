using System.Collections.Generic;
using System.Reflection;

namespace Yontech.Fat.Runner.Results
{
  public class TestCollectionRunResult
  {
    public Assembly Assembly { get; set; }
    public string Name { get; set; }
    public List<TestClassRunResult> TestClasses { get; set; }
  }
}