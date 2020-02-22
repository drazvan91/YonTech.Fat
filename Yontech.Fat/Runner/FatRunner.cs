using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;
using Yontech.Fat.DataSources;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Interceptors;

namespace Yontech.Fat.Runner
{

    public class FatRunner
    {
        private IWebBrowser _webBrowser;

        private InterceptDispatcher _interceptorDispatcher;
        private IocService _iocService;
        private FatDiscoverer _fatDiscoverer;
        private FatConfig _options;

        public FatRunner(Action<FatConfig> optionsCallback)
        {
            this._fatDiscoverer = new FatDiscoverer();

            var options = this._fatDiscoverer.DiscoverConfig();

            if (options == null)
            {
                options = FatConfig.Default();
            }

            optionsCallback(options);
            Init(options);
        }

        public FatRunner(FatConfig options)
        {
            this._fatDiscoverer = new FatDiscoverer();
            Init(options);
        }

        private void Init(FatConfig options)
        {
            this._options = options;

            var interceptors = options.Interceptors?.ToList() ?? new List<FatInterceptor>();

            this._interceptorDispatcher = new InterceptDispatcher(interceptors);
            this._iocService = new IocService(_fatDiscoverer, () =>
            {
                return this._webBrowser;
            });
        }

        public void Run<TFatTest>() where TFatTest : FatTest
        {
            var testCollections = new List<FatTestCollection>();
            var testCollection = _fatDiscoverer.DiscoverTestCollection<TFatTest>(_options.Filter);
            if (testCollection != null)
            {
                testCollections.Add(testCollection);
            }

            this.Run(testCollections);
        }

        public void Run()
        {
            var testCollections = _fatDiscoverer.DiscoverTestCollections(_options.Filter);
            this.Run(testCollections);
        }

        public void Run(IEnumerable<Assembly> assemblies)
        {
            var testCollections = _fatDiscoverer.DiscoverTestCollections(assemblies, _options.Filter);
            this.Run(testCollections);
        }

        public void Run(Assembly assembly)
        {
            var testCollections = new List<FatTestCollection>();
            var testCollection = _fatDiscoverer.DiscoverTestCollection(assembly, _options.Filter);
            if (testCollection != null)
            {
                testCollections.Add(testCollection);
            }

            this.Run(testCollections);
        }

        private void Run(IEnumerable<FatTestCollection> testCollections)
        {
            var browserStartOptions = new BrowserStartOptions()
            {
                RunHeadless = this._options.RunInBackground,
                DriversFolder = this._options.DriversFolder ?? "drivers",
                AutomaticDriverDownload = this._options.AutomaticDriverDownload
            };

            var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory();
            this._webBrowser = factory.Create(this._options.Browser, browserStartOptions);
            this._webBrowser.Configuration.BusyConditions.Add(new DocumentReadyBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new PendingRequestsBusyCondition());
            this._webBrowser.Configuration.BusyConditions.Add(new InstructionDelayTimeBusyCondition(this._options.DelayBetweenSteps));

            try
            {
                _interceptorDispatcher.OnExecutionStarts(new ExecutionStartsParams());
                foreach (var collection in testCollections)
                {
                    this.ExecuteTestCollection(collection);
                }
                _interceptorDispatcher.OnExecutionFinished(new ExecutionFinishedParams());

                // ConsoleReporter reporter = new ConsoleReporter();
                // reporter.Report(strategy);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRRORRRRRRR");

                Console.WriteLine((ex.InnerException ?? ex).StackTrace);
                Console.WriteLine((ex).StackTrace);

            }
            finally
            {
                this._webBrowser.Close();
                this._webBrowser = null;
            }
        }

        private void ExecuteTestCollection(FatTestCollection collection)
        {
            foreach (var testClass in collection.TestClasses)
            {
                _interceptorDispatcher.BeforeTestClass(testClass.Class);
                ExecuteTestClass(testClass);
                _interceptorDispatcher.AfterTestClass(testClass.Class);
            }
        }

        private void ExecuteTestClass(FatTestClass testClass)
        {
            var fatTest = _iocService.GetService<FatTest>(testClass.Class) as FatTest;

            fatTest.BeforeAllTestCases();
            foreach (var testCase in testClass.TestCases)
            {
                var watch = Stopwatch.StartNew();
                try
                {
                    _interceptorDispatcher.BeforeTestCase(testCase);
                    ExecuteTestCase(fatTest, testCase);
                    Thread.Sleep(_options.DelayBetweenTestCases);
                    _interceptorDispatcher.OnTestCasePassed(testCase, watch.Elapsed);
                }
                catch (Exception ex)
                {
                    var exception = ex.InnerException ?? ex;
                    _interceptorDispatcher.OnTestCaseFailed(testCase, watch.Elapsed, exception);
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