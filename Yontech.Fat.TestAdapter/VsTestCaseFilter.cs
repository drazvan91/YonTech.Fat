using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Runner;
using Yontech.Fat.TestAdapter.Factories;

namespace Yontech.Fat.TestAdapter
{
    // inspired from: https://github.com/xunit/visualstudio.xunit/blob/master/src/xunit.runner.visualstudio/Utility/TestCaseFilter.cs
    internal class VsTestCaseFilter : ITestCaseFilter
    {
        private const string DISPLAY_NAME_STRING = "DisplayName";
        private const string FULLY_QUALIFIED_NAME_STRING = "FullyQualifiedName";
        private readonly TestCaseFactory _testCaseFactory;

        private ITestCaseFilterExpression _filterExpression;

        private List<string> _supportedPropertyNames = new List<string>()
        {
            DISPLAY_NAME_STRING, FULLY_QUALIFIED_NAME_STRING,
        };

        public VsTestCaseFilter(IRunContext runContext, TestCaseFactory testCaseFactory)
        {
            this._testCaseFactory = testCaseFactory;
            _filterExpression = runContext.GetTestCaseFilter(this._supportedPropertyNames, null);
        }

        public bool ShouldExecuteTestCase(FatTestCase fatTestCase)
        {
            if (_filterExpression == null)
                return true;

            var testCase = _testCaseFactory.Create(fatTestCase);
            return _filterExpression.MatchTestCase(testCase, (p) => PropertyProvider(fatTestCase, p));
        }

        private object PropertyProvider(FatTestCase testCase, string name)
        {
            if (string.Equals(name, FULLY_QUALIFIED_NAME_STRING, StringComparison.OrdinalIgnoreCase))
                return testCase.FullyQualifiedName;
            if (string.Equals(name, DISPLAY_NAME_STRING, StringComparison.OrdinalIgnoreCase))
                return testCase.DisplayName;

            return null;
        }
    }
}
