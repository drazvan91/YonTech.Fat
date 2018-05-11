using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yontech.Fat.TestingWebApplication.Tests.Runtime
{
    [CollectionDefinition("FatTests")]
    public class FatTestsCollection:ICollectionFixture<BaseTestContext>
    {
       
    }
}
