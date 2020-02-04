using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;
using Yontech.Fat.DataSources;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Runner.ConsoleRunner;
using Yontech.Fat.Runner.Results;

namespace Yontech.Fat.Runner
{

    public class FatRunner
    {
        private readonly FatTestDiscoverer _discoverer = new FatTestDiscoverer();
        private IWebBrowser _webBrowser;

        private InterceptDispatcher _interceptorDispatcher;
        private IocService _iocService;

        public void Run(FatRunOptions options)
        {
            var browserStartOptions = new BrowserStartOptions()
            {
                RunHeadless = options.RunInBackground
            };

            var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory();
            this._webBrowser = factory.Create(Yontech.Fat.BrowserType.Chrome, browserStartOptions);
            this._webBrowser.Configuration.BusyConditions.Add(new DocumentReadyBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new PendingRequestsBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new InstructionDelayTimeBusyCondition(options.DelayBetweenSteps));

            _interceptorDispatcher = new InterceptDispatcher(options.Interceptors?.ToList());
            _iocService = new IocService(options.Assemblies, this._webBrowser);
            var strategy = RunStrategyFactory.Create(options);

            try
            {
                _interceptorDispatcher.OnExecutionStarts(new ExecutionStartsParams());
                foreach (var collection in strategy)
                {
                    this.ExecuteTestCollection(collection, options);
                }
                _interceptorDispatcher.OnExecutionFinished(new ExecutionFinishedParams());

                ConsoleReporter reporter = new ConsoleReporter();
                reporter.Report(strategy);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRRORRRRRRR");
                Console.WriteLine((ex.InnerException ?? ex).StackTrace);
            }
            finally
            {
                this._webBrowser.Close();
                this._webBrowser = null;
            }
        }

        private void ExecuteTestCollection(TestCollectionRunResult collection, FatRunOptions options)
        {
            foreach (var testClass in collection.TestClasses)
            {
                _interceptorDispatcher.BeforeTestClass(testClass.Class);
                ExecuteTestClass(testClass, options);
                _interceptorDispatcher.AfterTestClass(testClass.Class);
            }
        }

        private void ExecuteTestClass(TestClassRunResult testClass, FatRunOptions options)
        {
            var fatTest = _iocService.GetService<FatTest>(testClass.Class) as FatTest;

            fatTest.BeforeAllTestCases();

            foreach (var testCase in testClass.TestCases)
            {
                var watch = Stopwatch.StartNew();
                try
                {
                    _interceptorDispatcher.BeforeTestCase(fatTest.GetType(), testCase.Method);
                    ExecuteTestCase(fatTest, testCase);
                    Thread.Sleep(options.DelayBetweenTestCases);
                    _interceptorDispatcher.OnTestCasePassed(fatTest.GetType(), testCase.Method, watch.Elapsed);
                }
                catch (Exception ex)
                {
                    var exception = ex.InnerException ?? ex;
                    testCase.Result = TestCaseRunResult.ResultType.Error;
                    testCase.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
                    testCase.Exception = exception;
                    _interceptorDispatcher.OnTestCaseFailed(fatTest.GetType(), testCase.Method, watch.Elapsed, ex);
                }
                finally
                {
                    watch.Stop();
                    testCase.Duration = watch.Elapsed;
                }
            }

            fatTest.AfterAllTestCases();
        }

        private void ExecuteTestCase(FatTest testClassInstance, TestCaseRunResult testCase)
        {

            var methodParameters = testCase.Method.GetParameters();
            if (methodParameters.Length == 0)
            {
                this.ExecuteTestCaseWithDataSourceArguments(testClassInstance, testCase, new object[0]);
            }
            else
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(testCase.Method);

                foreach (System.Attribute attr in attrs.OfType<TestCaseDataSource>())
                {
                    var dataSource = (TestCaseDataSource)attr;
                    var executionArguments = dataSource.GetExecutionArguments(methodParameters);

                    foreach (var arguments in executionArguments)
                    {
                        this.ExecuteTestCaseWithDataSourceArguments(testClassInstance, testCase, arguments);

                    }
                }
            }

            testCase.Result = TestCaseRunResult.ResultType.Success;
        }

        private void ExecuteTestCaseWithDataSourceArguments(FatTest testClassInstance, TestCaseRunResult testCase, object[] executionArguments)
        {
            _webBrowser.SimulateFastConnection();

            testClassInstance.BeforeEachTestCase();
            _webBrowser.WaitForIdle();

            testCase.Method.Invoke(testClassInstance, executionArguments);
            _webBrowser.WaitForIdle();

            testClassInstance.AfterEachTestCase();
            _webBrowser.WaitForIdle();
        }

    }
}