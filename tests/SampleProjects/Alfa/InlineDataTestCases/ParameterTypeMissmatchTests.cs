using Yontech.Fat;
using Yontech.Fat.DataSources;
using Yontech.Fat.Labels;

namespace Alfa.InlineDataTestCases
{
    public class ParameterTypeMissmatchTests : FatTest
    {
        [InlineData("a string value that should be a number")]
        public void Test_type_missmatch_inline_data(int number)
        {
            Fail("This should not be executed because of params mismatch");
        }
    }
}
