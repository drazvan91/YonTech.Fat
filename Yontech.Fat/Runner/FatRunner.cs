﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;
using Yontech.Fat.DataSources;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Labels;
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Runner
{
    public class FatRunner
    {
        private readonly FatExecutionContext _execContext;

        private IWebBrowser[] _webBrowsers;
        private InterceptDispatcher _interceptorDispatcher;
        private IocService _iocService;
        private FatDiscoverer _fatDiscoverer;
        private LogsSink _logsSink;
        private ILogger _logger;
        private RunResults _runResults;

        public FatRunner(FatExecutionContext execContext, Action<FatConfig> configCallback = null)
        {
            this._execContext = execContext;
            this._fatDiscoverer = new FatDiscoverer(this._execContext);

            if (execContext.Config == null)
            {
                execContext.Config = this._fatDiscoverer.DiscoverConfig();
                if (execContext.Config == null)
                {
                    execContext.Config = new FatConfig();
                }
            }

            if (configCallback != null)
            {
                configCallback(execContext.Config);
            }

            Init();
        }

        public RunResults Run<TFatTest>() where TFatTest : FatTest
        {
            var testCollections = new List<FatTestCollection>();
            var testCollection = _fatDiscoverer.DiscoverTestCollection<TFatTest>(this._execContext.Config.Filter);
            if (testCollection != null)
            {
                testCollections.Add(testCollection);
            }

            return this.Run(testCollections);
        }

        public RunResults Run()
        {
            var testCollections = _fatDiscoverer.DiscoverTestCollections(this._execContext.Config.Filter);
            return this.Run(testCollections);
        }

        public RunResults Run(IEnumerable<Assembly> assemblies)
        {
            var testCollections = _fatDiscoverer.DiscoverTestCollections(assemblies, this._execContext.Config.Filter);
            return this.Run(testCollections);
        }

        public RunResults Run(Assembly assembly)
        {
            var testCollections = new List<FatTestCollection>();
            var testCollection = _fatDiscoverer.DiscoverTestCollection(assembly, this._execContext.Config.Filter);
            if (testCollection != null)
            {
                testCollections.Add(testCollection);
            }

            return this.Run(testCollections);
        }

        private void Init()
        {
            this._logger = this._execContext.LoggerFactory.Create(this);
            this._execContext.Config.Log(this._execContext.LoggerFactory);

            this._logsSink = new LogsSink();

            var interceptors = this._execContext.Config.Interceptors?.ToList() ?? new List<FatInterceptor>();

            this._interceptorDispatcher = new InterceptDispatcher(interceptors);
            this._iocService = new IocService(_execContext, _fatDiscoverer, _logsSink);

            this._logger.Info("Using Fat Framework version {0}", AssemblyVersionUtils.GetFatVersion());
        }

        private RunResults Run(IEnumerable<FatTestCollection> testCollections)
        {
            FatException initializationError = null;
            try
            {
                this._webBrowsers = this.CreateWebBrowsers();

                _logger.Info("Number of Busy conditions configured {0}", this._webBrowsers[0].Configuration.BusyConditions.Count);
                _logger.Info("Execution started");
            }
            catch (FatException fatException)
            {
                _logger.Error("Execution failed to start");
                _logger.Error(fatException, true);
                initializationError = fatException;
            }

            try
            {
                _runResults = new RunResults();
                _interceptorDispatcher.OnExecutionStarts(new ExecutionStartsParams());
                foreach (var collection in testCollections)
                {
                    this.ExecuteTestCollection(collection, initializationError);
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
                // the webBrowser can be null in case that it failed to initialize
                if (this._webBrowsers != null)
                {
                    foreach (var webBrowser in this._webBrowsers)
                    {
                        webBrowser.Close();
                    }

                    this._webBrowsers = null;
                }
            }

            return _runResults;
        }

        private IWebBrowser[] CreateWebBrowsers()
        {
            var factory = new Yontech.Fat.Selenium.SeleniumWebBrowserFactory(this._execContext);

            return this._execContext.Config.Browsers.Select(browserConfig =>
            {
                var webBrowser = factory.Create(browserConfig);

                webBrowser.Configuration.BusyConditions.AddRange(this.GetBusyConditions());

                foreach (var busyCondition in webBrowser.Configuration.BusyConditions)
                {
                    // TODO: make sure that busyConditions are different instances
                    _iocService.InjectFatDiscoverableProps(busyCondition, webBrowser);
                }

                return webBrowser;
            }).ToArray();
        }

        private IEnumerable<FatBusyCondition> GetBusyConditions()
        {
            yield return new DocumentReadyBusyCondition();
            yield return new PendingRequestsBusyCondition();
            yield return new InstructionDelayTimeBusyCondition(this._execContext.Config.DelayBetweenSteps);
            foreach (var condition in this._execContext.Config.BusyConditions)
            {
                yield return condition;
            }
        }

        private void ExecuteTestCollection(FatTestCollection collection, FatException initializationError)
        {
            foreach (var testClass in collection.TestClasses)
            {
                _interceptorDispatcher.BeforeTestClass(testClass.Class);
                ExecuteTestClass(testClass, initializationError);
                _interceptorDispatcher.AfterTestClass(testClass.Class);
            }
        }

        private void ExecuteTestClass(FatTestClass testClass, FatException initializationError)
        {
            FatTest[] fatTests = null;

            Exception beforeAllTestCasesException = initializationError;
            try
            {
                if (initializationError == null)
                {
                    fatTests = this._webBrowsers.Select(webBrowser =>
                    {
                        var fatTest = _iocService.GetService<FatTest>(testClass.Class, webBrowser);
                        fatTest.BeforeAllTestCases();
                        return fatTest;
                    }).ToArray();
                }
            }
            catch (Exception ex)
            {
                beforeAllTestCasesException = ex;
            }

            foreach (var testCase in testClass.TestCases)
            {
                if (this.ShouldSkipTestCase(testCase))
                {
                    _runResults.AddSkipped(testCase);
                    _interceptorDispatcher.OnTestCaseSkipped(testCase);
                    continue;
                }

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
                    ExecuteTestCase(fatTests, testCase);

                    var logs = _logsSink.GetLogs().ToList();
                    _runResults.AddPassed(testCase, watch.Elapsed, logs);
                    _interceptorDispatcher.OnTestCasePassed(testCase, watch.Elapsed, logs);
                    _logger.Info("Passed");
                }
                catch (Exception ex)
                {
                    var exception = ex.InnerException ?? ex;
                    _logger.Error(exception);
                    _logsSink.Add(Log.ERROR, exception.Message);

                    var logs = _logsSink.GetLogs().ToList();
                    _runResults.AddFailed(testCase, ex, watch.Elapsed, logs);
                    _interceptorDispatcher.OnTestCaseFailed(testCase, watch.Elapsed, exception, logs);
                }
                finally
                {
                    watch.Stop();
                }

                Thread.Sleep(this._execContext.Config.DelayBetweenTestCases);
            }

            if (fatTests != null)
            {
                foreach (var testInstance in fatTests)
                {
                    try
                    {
                        testInstance.AfterAllTestCases();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                    }
                }
            }
        }

        private bool ShouldSkipTestCase(FatTestCase testCase)
        {
            var labels = System.Attribute.GetCustomAttributes(testCase.Method).OfType<SkipTest>();
            if (labels.Any())
            {
                return true;
            }

            var classLabels = System.Attribute.GetCustomAttributes(testCase.Method.ReflectedType).OfType<SkipTest>();
            if (classLabels.Any())
            {
                return true;
            }

            return false;
        }

        private void ExecuteTestCase(FatTest[] testInstances, FatTestCase testCase)
        {
            var methodParameters = testCase.Method.GetParameters();
            if (methodParameters.Length == 0)
            {
                this.ExecuteTestCaseWithDataSourceArguments(testInstances, testCase, new object[0]);
            }
            else
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(testCase.Method);

                foreach (System.Attribute attr in attrs.OfType<TestCaseDataSource>())
                {
                    var dataSource = (TestCaseDataSource)attr;
                    var executionArguments = dataSource.GetExecutionArguments(this._execContext, testCase.Method);
                    foreach (var arguments in executionArguments)
                    {
                        this.ExecuteTestCaseWithDataSourceArguments(testInstances, testCase, arguments);
                    }
                }
            }
        }

        private void ExecuteTestCaseWithDataSourceArguments(FatTest[] testInstances, FatTestCase testCase, object[] executionArguments)
        {
            foreach (var testInstance in testInstances)
            {
                testInstance.WebBrowser.SimulateFastConnection();
                testInstance.BeforeEachTestCase();
                testInstance.WebBrowser.WaitForIdle();
            }

            foreach (var testInstance in testInstances)
            {
                testCase.Method.Invoke(testInstance, executionArguments);
            }

            foreach (var testInstance in testInstances)
            {
                testInstance.WebBrowser.WaitForIdle();
                testInstance.AfterEachTestCase();
                testInstance.WebBrowser.WaitForIdle();
            }
        }
    }
}
