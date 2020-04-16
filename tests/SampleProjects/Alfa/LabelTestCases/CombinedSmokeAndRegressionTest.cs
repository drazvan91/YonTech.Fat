using Yontech.Fat;
using Yontech.Fat.Labels;

namespace Alfa.LabelTestCases
{
    public class CombinedSmokeAndRegressionTest : FatTest
    {
        [SmokeTest]
        [RegressionTest]
        public void Test_with_both()
        {
        }

        [RegressionTest]
        public void Test_with_regression()
        {
        }

        [SmokeTest]
        public void Test_with_smoke()
        {
        }
    }
}
