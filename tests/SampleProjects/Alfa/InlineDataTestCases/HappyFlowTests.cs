using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.InlineDataTestCases
{
    public class HappFlowTests : FatTest
    {
        [InlineData("string value")]
        public void Test_one_string_inline_data(string stringValue)
        {
            LogInfo("data: {0}", stringValue);
        }

        [InlineData(4)]
        public void Test_one_number_inline_data(int number)
        {
            LogInfo("data: {0}", number);
        }

        [InlineData("string 1")]
        [InlineData("string 2")]
        [InlineData("string 3")]
        public void Test_multiple_inline_data_values(string value)
        {
            LogInfo("data: {0}", value);
        }

        [InlineData("string 1", 2, "string 3")]
        public void Test_multiple_params(string value1, int value2, string value3)
        {
            LogInfo("test: {0} {1} {2}", value1, value2, value3);
        }
    }
}
