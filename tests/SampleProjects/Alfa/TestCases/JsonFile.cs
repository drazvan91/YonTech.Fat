using Yontech.Fat;
using Yontech.Fat.DataSources;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    public class JsonFile : FatTest
    {
        [JsonFileData("files/jsonFile.json")]
        public void Test_json_existing_properties(string column1, string column3)
        {
            LogInfo("Value from column1: {0}", column1);
            LogInfo("Value from column3: {0}", column3);
        }

        [JsonFileData("files/file_not_found.csv")]
        public void Test_json_does_not_exist(string noColumn)
        {
        }
    }
}
