using System;
using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat.Discoverer;

namespace Yontech.Fat.Interceptors
{
    // The purpose of this class is act as a service which receive an event and 
    // dispatches it to a list of interceptors. More than that it handles exceptions
    // and in the future it might handle timeout.
    internal class InterceptDispatcher
    {
        private readonly List<FatInterceptor> _interceptors;

        internal InterceptDispatcher(List<FatInterceptor> interceptors)
        {
            this._interceptors = interceptors;
        }
        public void AfterTestClass(Type @class)
        {
            var interceptParams = new TestClassParams()
            {
                TestClassFullName = @class.FullName
            };

            this.SafeForEach((interceptor) =>
            {
                interceptor.AfterTestClass(interceptParams);
            });
        }

        public void BeforeTestCase(FatTestCase fatTestCase)
        {
            this.SafeForEach((interceptor) =>
            {
                interceptor.BeforeTestCase(fatTestCase);
            });
        }

        public void BeforeTestClass(Type @class)
        {
            var interceptParams = new TestClassParams()
            {
                TestClassFullName = @class.FullName
            };

            this.SafeForEach((interceptor) =>
            {
                interceptor.BeforeTestClass(interceptParams);
            });
        }

        public void OnExecutionFinished(ExecutionFinishedParams finishedParams)
        {
            this.SafeForEach((interceptor) =>
            {
                interceptor.OnExecutionFinished(finishedParams);
            });
        }

        public void OnExecutionStarts(ExecutionStartsParams startsParams)
        {
            this.SafeForEach((interceptor) =>
            {
                interceptor.OnExecutionStarts(startsParams);
            });
        }

        public void OnTestCaseFailed(FatTestCase testCase, TimeSpan duration, Exception ex)
        {
            var interceptParams = new FatTestCaseFailed()
            {
                Duration = duration,
                Exception = ex
            };

            this.SafeForEach((interceptor) =>
            {
                interceptor.OnTestCaseFailed(testCase, interceptParams);
            });
        }

        public void OnTestCasePassed(FatTestCase testCase, TimeSpan duration)
        {
            var interceptParams = new FatTestCasePassed()
            {
                Duration = duration
            };

            this.SafeForEach((interceptor) =>
            {
                interceptor.OnTestCasePassed(testCase, interceptParams);
            });
        }

        public void OnTestCaseSkipped(OnTestCaseSkippedParams skippedTest)
        {
            this.SafeForEach((interceptor) =>
            {
                interceptor.OnTestCaseSkipped(skippedTest);
            });
        }

        private void SafeForEach(Action<FatInterceptor> action)
        {
            if (_interceptors == null)
            {
                return;
            }

            foreach (var interceptor in _interceptors)
            {
                try
                {
                    action(interceptor);
                }
                catch
                {
                    // todo: we have to provide a way to send logs about failing interceptors
                }
            }
        }
    }
}