using Xunit;
using Yontech.Fat.Configuration;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Mocks;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class FatConfigTests
    {
        [Fact]
        public void If_driver_does_not_exist_Then_error_will_be_thrown()
        {
            var config = new FatConfig();
            config.BrowserConfig.AutomaticDriverDownload = false;
            config.BrowserConfig.DriversFolder = "some_random_folder";

            var context = new MockedExecutionContext(config, typeof(Alfa.Config1).Assembly);
            var runner = new FatRunner(context);

            var result = runner.Run<Alfa.TestCases.CsvFile>();

            Assert.Equal(2, result.Failed);
            context.MockedLoggerFactory.AssertContains(LogLevel.Error, "The driver could not be found at location");
        }
    }
}
