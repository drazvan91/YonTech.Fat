using Yontech.Fat;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    public class OneTestSkipped : FatTest
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
