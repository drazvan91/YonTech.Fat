using System;
using System.Collections.Generic;

namespace Yontech.Fat.Runner.Results
{
  public class TestClassRunResult
  {
    public Type Class { get; set; }
    public string Name { get; set; }
    public List<TestCaseRunResult> TestCases { get; set; }
  }
}