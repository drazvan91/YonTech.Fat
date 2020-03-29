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
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Runner
{

    public class FatRunner
    {
        private IWebBrowser _webBrowser;

        private InterceptDispatcher _interceptorDispatcher;
        private IocService _iocService;
        private FatDiscoverer _fatDiscoverer;
        private FatConfig _options;
        private LogsSink _logsSink;
        private ILogger _logger;
        private RunResults _runResults;
        private readonly IAssemblyDiscoverer _assemblyDiscoverer;
        private readonly ILoggerFactory _loggerFactory;

        public FatRunner(IAssemblyDiscoverer assemblyDiscoverer, ILoggerFactory loggerFactory, Action<FatConfig> optionsCallback)
        {
            this._assemblyDiscoverer = assemblyDiscoverer;
            this._loggerFactory = loggerFactory;
            this._fatDiscoverer = new FatDiscoverer(assemblyDiscoverer, loggerFactory);

            var options = this._fatDiscoverer.DiscoverConfig();
            if (options == null)
            {
                options = new DefaultFatConfig();
            }
            else
            {
                _loggerFactory.LogLevel = options.LogLevel;
                _loggerFactory.LogLevelConfig = options.LogLevelConfig;
            }

            optionsCallback(options);
            Init(options);
        }

        public FatRunner(IAssemblyDiscoverer assemblyDiscoverer, ILoggerFactory loggerFactory, FatConfig options)
        {
            this._assemblyDiscoverer = assemblyDiscoverer;
            this._loggerFactory = loggerFactory;
            this._fatDiscoverer = new FatDiscoverer(assemblyDiscoverer, loggerFactory);
            Init(options);
        }

        private void Init(FatConfig options)
        {
            this._logger = _loggerFactory.Create(this);
            this._options = options;
            this._options.Log(_loggerFactory);

            this._logsSink = new LogsSink();

            var interceptors = options.Interceptors?.ToList() ?? new List<FatInterceptor>();

            this._interceptorDispatcher = new InterceptDispatcher(interceptors);
            this._iocService = new IocService(_fatDiscoverer, _assemblyDiscoverer, _loggerFactory, _logsSink, () =>
            {
                return this._webBrowser;
            });

            this._logger.Info("Using Fat Framework version {0}", AssemblyVersionUtils.GetFatVersion());
        }

        public RunResults Run<TFatTest>() where TFatTest : FatTest
        {
            var testCollections = new List<FatTestCollection>();
            var testCollection = _fatDiscoverer.DiscoverTestCollection<TFatTest>(_options.Filter);
            if (testCollection != null)
            {
                testCollections.Add(testCollection);
            }

            return this.Run(testCollections);
        }

        public RunResults Run()
        {
            var testCollections = _fatDiscoverer.DiscoverTestCollections(_options.Filter);
            return this.Run(testCollections);
        }

        public RunResults Run(IEnumerable<Assembly> assemblies)
        {
            var testCollections = _fatDiscoverer.DiscoverTestCollections(assemblies, _options.Filter);
            return this.Run(testCollections);
        }

        public RunResults Run(Assembly assembly)
        {
            var testCollections = new List<FatTestCollection>();
            var testCollection = _fatDiscoverer.DiscoverTestCollection(assembly, _options.Filter);
            if (testCollection != null)
            {
                testCollections.Add(testCollection);
            }

            return this.Run(testCollections);
        }

        private RunResults Run(IEnumerable<FatTestCollection> testCollections)
        {
            var browserStartOptions = new BrowserStartOptions()
            {
                RunHeadless = this._options.RunInBackground,
                StartMaximized = this._options.StartMaximized,
                InitialSize = this._options.InitialSize,
                DriversFolder = this._options.DriversFolder ?? "drivers",
                AutomaticDriverDownload = this._options.AutomaticDriverDownload,
                RemoteDebuggerAddress = this._options.RemoteDebuggerAddress,
            };

            var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory(this._loggerFactory);
            this._webBrowser = factory.Create(this._options.Browser, browserStartOptions);

            this._webBrowser.Configuration.DefaultTimeout = this._options.Timeouts.DefaultTimeout;
            this._webBrowser.Configuration.FinderTimeout = this._options.Timeouts.FinderTimeout;

            this._webBrowser.Configuration.BusyConditions.AddRange(this.GetBusyConditions());
            foreach (var busyCondition in this._webBrowser.Configuration.BusyConditions)
            {
                _iocService.InjectFatDiscoverableProps(busyCondition);
            }

            _logger.Info("Number of Busy conditions configured {0}", this._webBrowser.Configuration.BusyConditions.Count);

            try
            {
                _runResults = new RunResults();
                _logger.Info("Execution started");
                _interceptorDispatcher.OnExecutionStarts(new ExecutionStartsParams());
                foreach (var collection in testCollections)
                {
                    this.ExecuteTestCollection(collection);
                }
                _logger.Info("Execution finished");
                _interceptorDispatcher.OnExecutionFinished(new ExecutionFinishedParams());
            }
            catch (Exception ex)
            {
                _logger.Error("Execution stopped");
                _logger.Error(ex.Message);
                _logger.Error(ex);
            }
            finally
            {
                this._webBrowser.Close();
                this._webBrowser = null;
            }

            return _runResults;
        }

        private IEnumerable<FatBusyCondition> GetBusyConditions()
        {
            yield return new DocumentReadyBusyCondition();
            yield return new PendingRequestsBusyCondition();
            yield return new InstructionDelayTimeBusyCondition(this._options.DelayBetweenSteps);
            foreach (var condition in this._options.BusyConditions)
            {
                yield return condition;
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
            FatTest fatTest = null;

            Exception beforeAllTestCasesException = null;
            try
            {
                fatTest = _iocService.GetService<FatTest>(testClass.Class);
                fatTest.BeforeAllTestCases();
            }
            catch (Exception ex)
            {
                beforeAllTestCasesException = ex;
            }

            foreach (var testCase in testClass.TestCases)
            {
                var watch = Stopwatch.StartNew();
                _logsSink.Reset();
                try
                {
                    _logger.Info("Executing testcase '{0}'", testCase.FullyQualifiedName);
                    if (beforeAllTestCasesException != null)
                    {
                        throw beforeAllTestCasesException;
                    }

                    _interceptorDispatcher.BeforeTestCase(testCase);
                    ExecuteTestCase(fatTest, testCase);
                    _runResults.Passed++;
                    _interceptorDispatcher.OnTestCasePassed(testCase, watch.Elapsed, _logsSink.GetLogs().ToList());
                    _logger.Info("Passed");
                }
                catch (Exception ex)
                {
                    _runResults.Failed++;
                    var exception = ex.InnerException ?? ex;
                    _logger.Error(exception);
                    _interceptorDispatcher.OnTestCaseFailed(testCase, watch.Elapsed, exception, _logsSink.GetLogs().ToList());
                }
                finally
                {
                    watch.Stop();
                }

                Thread.Sleep(_options.DelayBetweenTestCases);
            }

            try
            {
                fatTest.AfterAllTestCases();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
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
