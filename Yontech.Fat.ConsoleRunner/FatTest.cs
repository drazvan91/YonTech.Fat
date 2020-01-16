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
    public FatContext Context { get; internal set; }
    public IWebBrowser Browser { get; internal set; }

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