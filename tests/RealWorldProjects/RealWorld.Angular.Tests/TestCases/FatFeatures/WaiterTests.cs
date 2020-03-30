using Yontech.Fat;
using RealWorld.Angular.Tests.Flows;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using RealWorld.Angular.Tests.Data;
using System.Diagnostics;

namespace RealWorld.Angular.Tests.TestCases.FatFeatures
{
    public class WaiterTests : FatTest
    {
        private Stopwatch _watch;

        public override void BeforeAllTestCases()
        {
            this._watch = Stopwatch.StartNew();
        }

        public override void AfterAllTestCases()
        {
            this._watch.Stop();

            FailIf(_watch.ElapsedMilliseconds < 2500, "Wait(time) is not working. Time ellapsed: {0}", _watch.ElapsedMilliseconds);
        }

        public void Test_1()
        {
            Wait(500);
        }

        public void Test_2()
        {
            var watch = Stopwatch.StartNew();
            Wait(1000);
            watch.Stop();

            FailIf(watch.ElapsedMilliseconds < 1000, "Wait(time) is not working. Time ellapsed: {0}", watch.ElapsedMilliseconds);
        }

        public void Test_3()
        {
            Wait(500);
        }

        public void Test_4()
        {
            Wait(500);
        }
    }
}