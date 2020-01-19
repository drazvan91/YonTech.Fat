using System;
using System.Threading;

namespace Yontech.Fat.ConsoleRunner
{
  public class FatContext
  {
    public BrowserType BrowserType { get; set; }
  }

  public class FatTest
  {
    internal protected FatContext Context { get; internal set; }
    internal protected IWebBrowser Browser { get; internal set; }

    public virtual void BeforeEachTestCase() { }
    public virtual void BeforeAllTestCases() { }

    public virtual void AfterEachTestCase() { }
    public virtual void AfterAllTestCases() { }


    protected void Log(string message)
    {
      Console.WriteLine(message);
    }

    protected void Wait(int milliseconds)
    {
      Thread.Sleep(milliseconds);
    }
  }
}