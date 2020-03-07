using Yontech.Fat.Discoverer;

namespace Yontech.Fat.Runner
{
    public interface ITestCaseFilter
    {
        bool ShouldExecuteTestCase(FatTestCase testCase);
    }
}
