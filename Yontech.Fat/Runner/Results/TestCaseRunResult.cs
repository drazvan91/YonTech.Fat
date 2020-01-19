using System.Reflection;

namespace Yontech.Fat.Runner.Results
{
  public class TestCaseRunResult
  {
    public MethodInfo Method { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public ResultType Result { get; set; }
    public long Duration { get; set; }
    public string Logs { get; set; }
    public string ErrorMessage { get; set; }

    public enum ResultType
    {
      NotStarted,
      Skipped,
      Success,
      Error
    }
  }
}