using Yontech.Fat;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    [SkipTest]
    public class EntireClassSkipped : FatTest
    {
        public override void BeforeAllTestCases()
        {
            LogError("This should not be called because the class is skipped");
        }

        [SkipTest]
        public void Test_this_is_ignored()
        {
        }

        public void Test_this_is_not_ignored()
        {
        }
    }
}
