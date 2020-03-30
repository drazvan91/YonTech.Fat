using Yontech.Fat;

namespace RealWorld.Angular.Tests.TestCases.FatFeatures
{
    public class LoggerTests : FatTest
    {
        public override void BeforeAllTestCases()
        {
            LogInfo("Log before all test cases");
        }

        public override void BeforeEachTestCase()
        {
            LogInfo("Log before each test case");
        }

        public override void AfterAllTestCases()
        {
            LogInfo("Log after all test cases");
        }

        public override void AfterEachTestCase()
        {
            LogInfo("Log after each test case");
        }

        public void Test_debug_logger()
        {
            LogDebug("This is a {0} message", "debug");
        }

        public void Test_info_logger()
        {
            LogInfo("This is {0} message", "info");
        }

        public void Test_warning_logger()
        {
            LogWarning("This is a {0} message", "warning");
        }

        public void Test_error_logger()
        {
            LogError("This is an {0} message", "error");
        }
    }
}