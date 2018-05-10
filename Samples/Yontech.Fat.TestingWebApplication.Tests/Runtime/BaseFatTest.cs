using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yontech.Fat.TestingWebApplication.Tests.Runtime
{
    [Collection("FatTests")]
    public class BaseFatTest
    {
        private readonly BaseTestContext _testContext;

        protected IWebBrowser Browser
        {
            get
            {
                return _testContext.Browser;
            }
        }

        public BaseFatTest(BaseTestContext testContext)
        {
            this._testContext = testContext;
        }
    }
}
