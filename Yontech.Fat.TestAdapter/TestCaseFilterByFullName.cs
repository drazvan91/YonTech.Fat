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
    internal class TestCaseFilterByFullName : ITestCaseFilter
    {
        private readonly IEnumerable<string> _names;

        public TestCaseFilterByFullName(IEnumerable<string> names)
        {
            this._names = names;
        }

        public bool ShouldExecuteTestCase(FatTestCase fatTestCase)
        {
            return this._names.Contains(fatTestCase.FullyQualifiedName);
        }
    }

}