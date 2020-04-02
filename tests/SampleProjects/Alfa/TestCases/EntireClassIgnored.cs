using Yontech.Fat;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    [IgnoreTest]
    public class EntireClassIgnored : FatTest
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
