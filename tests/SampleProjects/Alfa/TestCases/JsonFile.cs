using Yontech.Fat;
using Yontech.Fat.DataSources;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    public class JsonFile : FatTest
    {
        public class JsonFileModel
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
        }

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

        [JsonFileData("files/jsonFile.json")]
        public void Test_json_object(JsonFileModel model)
        {
            LogInfo("model.column1: {0}", model.Column1);
        }
    }


}
