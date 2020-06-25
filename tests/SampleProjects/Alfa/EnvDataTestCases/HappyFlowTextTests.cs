using Yontech.Fat;

namespace Alfa.EnvDataTestCases
{
    public class HappFlowTxtTests : FatTest
    {
        SimpleEnvTextData simpleEnvData { get; set; }

        public void Test_simple_env_data()
        {
            FailIf(simpleEnvData.FirstName != "Razvan", "Simple env data not working for strings");
            FailIf(simpleEnvData.LastName != "Dragomir", "Simple env data not working for strings");
            FailIf(simpleEnvData.Age != 13, "Simple env data not working for integers");

        }
    }
}
