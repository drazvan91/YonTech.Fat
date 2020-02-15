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
using Yontech.Fat.Discoverer;
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
                RunHeadless = options.RunInBackground,
                DriversFolder = options.DriversFolder ?? "drivers",
                AutomaticDriverDownload = options.AutomaticDriverDownload
            };

            var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory();
            this._webBrowser = factory.Create(Yontech.Fat.BrowserType.Chrome, browserStartOptions);
            this._webBrowser.Configuration.BusyConditions.Add(new DocumentReadyBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new PendingRequestsBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new InstructionDelayTimeBusyCondition(options.DelayBetweenSteps));

            _interceptorDispatcher = new InterceptDispatcher(options.Interceptors?.ToList());
            _iocService = new IocService(options.Assemblies, this._webBrowser);

            var fatDiscoverer = new FatDiscoverer();
            var testCollections = fatDiscoverer.DiscoverTestCollections(options.Assemblies);
            //var strategy = RunStrategyFactory.Create(options);

            try
            {
                _interceptorDispatcher.OnExecutionStarts(new ExecutionStartsParams());
                foreach (var collection in testCollections)
                {
                    this.ExecuteTestCollection(collection, options);
                }
                _interceptorDispatcher.OnExecutionFinished(new ExecutionFinishedParams());

                // ConsoleReporter reporter = new ConsoleReporter();
                // reporter.Report(strategy);

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

        private void ExecuteTestCollection(FatTestCollection collection, FatRunOptions options)
        {
            foreach (var testClass in collection.TestClasses)
            {
                _interceptorDispatcher.BeforeTestClass(testClass.Class);
                ExecuteTestClass(testClass, options);
                _interceptorDispatcher.AfterTestClass(testClass.Class);
            }
        }

        private void ExecuteTestClass(FatTestClass testClass, FatRunOptions options)
        {
            var fatTest = _iocService.GetService<FatTest>(testClass.Class) as FatTest;

            fatTest.BeforeAllTestCases();

            foreach (var testCase in testClass.TestCases)
            {
                var shouldExecute = options.Filter?.ShouldExecuteTestCase(testCase) ?? true;
                if (!shouldExecute)
                {
                    continue;
                }

                var watch = Stopwatch.StartNew();
                try
                {
                    _interceptorDispatcher.BeforeTestCase(testCase);
                    ExecuteTestCase(fatTest, testCase);
                    Thread.Sleep(options.DelayBetweenTestCases);
                    _interceptorDispatcher.OnTestCasePassed(testCase, watch.Elapsed);
                }
                catch (Exception ex)
                {
                    var exception = ex.InnerException ?? ex;
                    // testCase.Result = TestCaseRunResult.ResultType.Error;
                    // testCase.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
                    // testCase.Exception = exception;
                    _interceptorDispatcher.OnTestCaseFailed(testCase, watch.Elapsed, ex);
                }
                finally
                {
                    watch.Stop();
                }
            }

            fatTest.AfterAllTestCases();
        }

        private void ExecuteTestCase(FatTest testClassInstance, FatTestCase testCase)
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
        }

        private void ExecuteTestCaseWithDataSourceArguments(FatTest testClassInstance, FatTestCase testCase, object[] executionArguments)
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