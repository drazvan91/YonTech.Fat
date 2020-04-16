using Yontech.Fat.Discoverer;

namespace Yontech.Fat.Filters
{
    public interface ITestCaseFilter
    {
        bool ShouldExecuteTestCase(FatTestCase testCase);
    }
}
