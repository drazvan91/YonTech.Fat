using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.JsonFileDataTestCases
{
    public class TypeMismatchTests : FatTest
    {

        [JsonFileData("files/persons.json")]
        public void Test_type_not_supported(string age)
        {
        }
    }
}
