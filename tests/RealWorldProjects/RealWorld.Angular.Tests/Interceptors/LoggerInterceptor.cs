using System;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Interceptors;

namespace RealWorld.Angular.Tests.Interceptors
{
    public class LoggerInterceptor : FatInterceptor
    {
        protected override void OnExecutionStarts(ExecutionStartsParams startsParams)
        {
            Console.WriteLine("The execution is going to start");
        }

        protected override void OnTestCasePassed(FatTestCase testCase, FatTestCasePassed passed)
        {
            Console.WriteLine("PASS!! {0}", testCase.FullyQualifiedName);
        }
    }
}