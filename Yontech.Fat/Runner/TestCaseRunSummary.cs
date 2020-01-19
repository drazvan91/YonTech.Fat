using System;

namespace Yontech.Fat.Runner
{
  public class TestCaseRunSummary
  {
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public string FullName { get; set; }
    public long TimeEllapsed { get; set; }
    public Exception Error { get; set; }
    public bool Success { get { return Error == null; } }
  }
}