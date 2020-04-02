using Yontech.Fat;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    public class OneTestIgnored : FatTest
    {
        [IgnoreTest]
        public void Test_this_is_ignored()
        {
        }

        public void Test_this_is_not_ignored()
        {
        }
    }
}
