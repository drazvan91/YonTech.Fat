using System;
using System.Threading;

namespace Yontech.Fat
{
  public class FatTest
  {
    internal protected IWebBrowser WebBrowser { get; internal set; }

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