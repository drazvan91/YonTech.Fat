using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.JsonFileDataTestCases
{
    public class PropertyDoesNotExist : FatTest
    {

        [JsonFileData("files/persons.json")]
        public void Test_property_does_not_exist(int age, string city)
        {
        }
    }
}
