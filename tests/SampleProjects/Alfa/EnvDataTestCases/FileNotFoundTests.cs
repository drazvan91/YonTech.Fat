using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.EnvDataTestCases
{
    public class FileNotFoundTests : FatTest
    {
        FileNotFoundEnvData fileNotFoundEnvData { get; set; }

        public void Test_file_not_found()
        {
            FailIf(fileNotFoundEnvData == null, "Empty envData should be provided");
            FailIf(fileNotFoundEnvData.LastName != null, "Empty envData should be provided");
        }
    }
}
