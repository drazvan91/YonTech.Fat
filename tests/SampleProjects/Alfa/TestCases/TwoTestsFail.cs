using Yontech.Fat;

namespace Alfa.TestCases
{
    public class TwoTestsFail : FatTest
    {
        public void Test_some_tests_fail_1()
        {
        }

        public void Test_some_tests_fail_2()
        {
            Fail("Fail 1");
        }

        public void Test_some_tests_fail_3()
        {
        }

        public void Test_some_tests_fail_4()
        {
            Fail("Fail 1");
        }

        public void Test_some_tests_fail_5()
        {
        }
    }
}
