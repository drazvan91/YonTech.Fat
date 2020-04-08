using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.InlineDataTestCases
{
    public class ParametersNumberMissmatchTests : FatTest
    {
        [InlineData("string 1")]
        public void Test_fewer_parameters(string value1, string value2)
        {
        }

        [InlineData("string 1", "string 2", "string 3")]
        public void Test_more_parameters(string value1, string value2)
        {
        }
    }
}
