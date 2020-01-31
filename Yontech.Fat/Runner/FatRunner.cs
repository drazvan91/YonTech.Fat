using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.DataSources;
using Yontech.Fat.Runner.ConsoleRunner;
using Yontech.Fat.Runner.Results;

namespace Yontech.Fat.Runner
{

    public class FatRunner
    {
        private readonly FatTestDiscoverer _discoverer = new FatTestDiscoverer();
        private IWebBrowser _webBrowser;

        public void Run(FatRunOptions options)
        {

            var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory();
            this._webBrowser = factory.Create(Yontech.Fat.BrowserType.Chrome);
            this._webBrowser.Configuration.BusyConditions.Add(new DocumentReadyBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new PendingRequestsBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new InstructionDelayTimeBusyCondition(options.DelayBetweenInstructions));


            var iocService = new IocService(options.Assemblies, this._webBrowser);
            var strategy = RunStrategyFactory.Create(options);

            try
            {
                foreach (var collection in strategy)
                {
                    this.ExecuteTestCollection(collection, options, iocService);
                }

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

        private void ExecuteTestCollection(TestCollectionRunResult collection, FatRunOptions options, IocService iocService)
        {
            foreach (var testClass in collection.TestClasses)
            {
                ExecuteTestClass(testClass, options, iocService);
            }
        }

        private void ExecuteTestClass(TestClassRunResult testClass, FatRunOptions options, IocService iocService)
        {
            var fatTest = iocService.GetService<FatTest>(testClass.Class) as FatTest;

            fatTest.BeforeAllTestCases();

            foreach (var testCase in testClass.TestCases)
            {
                var watch = Stopwatch.StartNew();
                try
                {
                    ExecuteTestCase(fatTest, testCase, iocService);
                    Thread.Sleep(options.WaitAfterEachTestCase);
                }
                catch (Exception ex)
                {
                    var exception = ex.InnerException ?? ex;
                    testCase.Result = TestCaseRunResult.ResultType.Error;
                    testCase.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
                    testCase.Exception = exception;
                }
                finally
                {
                    watch.Stop();
                    testCase.Duration = watch.ElapsedMilliseconds;
                }
            }

            fatTest.AfterAllTestCases();
        }

        private void ExecuteTestCase(FatTest testClassInstance, TestCaseRunResult testCase, IocService iocService)
        {

            if (testCase.Method.GetParameters().Length == 0)
            {
                this.ExecuteTestCaseWithDataSourceArguments(testClassInstance, testCase, iocService, new object[0]);
            }
            else
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(testCase.Method);

                foreach (System.Attribute attr in attrs.OfType<TestCaseDataSource>())
                {
                    var dataSource = (TestCaseDataSource)attr;
                    var executionArguments = dataSource.GetExecutionArguments();

                    foreach (var arguments in executionArguments)
                    {
                        this.ExecuteTestCaseWithDataSourceArguments(testClassInstance, testCase, iocService, arguments);

                    }
                }
            }

            testCase.Result = TestCaseRunResult.ResultType.Success;
        }

        private void ExecuteTestCaseWithDataSourceArguments(FatTest testClassInstance, TestCaseRunResult testCase, IocService iocService, object[] executionArguments)
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