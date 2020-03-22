using Yontech.Fat;

namespace Alfa.TestCases
{
    public class AllTestsFail : FatTest
    {
        public void Test_all_tests_fail_1()
        {
            Fail("fail 1");
        }

        public void Test_all_tests_fail_2()
        {
            Fail("fail 2");
        }

        public void Test_all_tests_fail_3()
        {
            Fail("fail 3");
        }
    }
}
