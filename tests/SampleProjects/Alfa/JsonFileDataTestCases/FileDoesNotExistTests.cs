using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.JsonFileDataTestCases
{
    public class FileDoesNotExistTests : FatTest
    {

        [JsonFileData("files/file_not_found.json")]
        public void Test_file_not_found(string column1, string column3)
        {
        }
    }
}
