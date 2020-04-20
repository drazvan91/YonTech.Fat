using Yontech.Fat;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    [SkipTest]
    public class EntireClassSkipped : FatTest
    {
        [SkipTest]
        public void Test_this_is_ignored()
        {
        }

        public void Test_this_is_not_ignored()
        {
        }
    }
}
