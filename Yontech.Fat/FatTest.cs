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


        protected void Log(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        protected void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}