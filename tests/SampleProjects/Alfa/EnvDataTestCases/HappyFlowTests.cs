using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.EnvDataTestCases
{
    public class HappFlowTests : FatTest
    {
        SimpleEnvData simpleEnvData { get; set; }
        OverrideEnvData overrideEnvData { get; set; }

        public void Test_simple_env_data()
        {
            FailIf(simpleEnvData.FirstName != "Razvan", "Simple env data not working");
            FailIf(simpleEnvData.LastName != "Dragomir", "Simple env data not working");
        }

        public void Test_override_env_data()
        {
            FailIf(overrideEnvData.FirstName != "Razvan", "Override env data not working");
            FailIf(overrideEnvData.LastName != "Dragomir", "Override env data not working");
        }
    }
}
