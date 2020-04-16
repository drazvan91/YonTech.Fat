using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Filters;

namespace Yontech.Fat.TestAdapter
{
    internal class VsTestCaseFilterByFullName : ITestCaseFilter
    {
        private readonly IEnumerable<string> _names;

        public VsTestCaseFilterByFullName(IEnumerable<string> names)
        {
            this._names = names;
        }

        public bool ShouldExecuteTestCase(FatTestCase fatTestCase)
        {
            return this._names.Contains(fatTestCase.FullyQualifiedName);
        }
    }
}
