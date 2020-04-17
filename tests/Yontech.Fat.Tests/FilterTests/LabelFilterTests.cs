using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Filters;
using Yontech.Fat.Labels;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Extensions;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.FilterTests
{
    public class LabelFilterTests
    {
        private readonly MockedLoggerFactory mockedLoggerFactory;
        private readonly StreamProvider streamProvider;
        private readonly MockedAssemblyDiscoverer assemblyDiscoverer;

        public LabelFilterTests()
        {
            this.mockedLoggerFactory = new MockedLoggerFactory();
            this.streamProvider = new StreamProvider(mockedLoggerFactory);
            this.assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);
        }

        [Fact]
        public void When_smoke_tests_set_Then_ignore_the_others()
        {
            var config = new Alfa.Config1()
            {
                Filter = new LabelTestCaseFilter(SmokeTest.SMOKE_TEST_LABEL)
            };

            var runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);
            var result = runner.Run<Alfa.LabelTestCases.OneSmokeTest>();

            Assert.Equal(0, result.Failed);
            Assert.Equal(0, result.Skipped);
            Assert.Equal(1, result.Passed);

            result.AssertTestHasPassed("Test_this_is_a_smoke");
        }

        [Fact]
        public void When_smoke_tests_set_on_class_Then_execute_all()
        {
            var config = new Alfa.Config1()
            {
                Filter = new LabelTestCaseFilter(SmokeTest.SMOKE_TEST_LABEL)
            };

            var runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);
            var result = runner.Run<Alfa.LabelTestCases.OneSmokeTestClass>();

            Assert.Equal(0, result.Failed);
            Assert.Equal(0, result.Skipped);
            Assert.Equal(2, result.Passed);
        }

        [Fact]
        public void When_multiple_labels_Then_execute_all()
        {
            var config = new Alfa.Config1()
            {
                Filter = new LabelTestCaseFilter(SmokeTest.SMOKE_TEST_LABEL, RegressionTest.REGRESSION_TEST_LABEL)
            };

            var runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);
            var result = runner.Run<Alfa.LabelTestCases.CombinedSmokeAndRegressionTest>();

            Assert.Equal(0, result.Failed);
            Assert.Equal(0, result.Skipped);
            Assert.Equal(3, result.Passed);
        }

        [Fact]
        public void When_multiple_labels_assigned_Then_execute_only_the_configured_ones()
        {
            var config = new Alfa.Config1()
            {
                Filter = new LabelTestCaseFilter(SmokeTest.SMOKE_TEST_LABEL)
            };

            var runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);
            var result = runner.Run<Alfa.LabelTestCases.CombinedSmokeAndRegressionTest>();

            Assert.Equal(0, result.Failed);
            Assert.Equal(0, result.Skipped);
            Assert.Equal(2, result.Passed);
        }

        [Fact]
        public void When_multiple_labels_assigned_but_no_test_fits_Then_none_executed()
        {
            var config = new Alfa.Config1()
            {
                Filter = new LabelTestCaseFilter("random_label")
            };

            var runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);
            var result = runner.Run<Alfa.LabelTestCases.CombinedSmokeAndRegressionTest>();

            Assert.Equal(0, result.Failed);
            Assert.Equal(0, result.Skipped);
            Assert.Equal(0, result.Passed);
        }
    }
}
