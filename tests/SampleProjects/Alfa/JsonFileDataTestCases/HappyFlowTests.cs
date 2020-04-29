using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.JsonFileDataTestCases
{
    public class HappyFlowTests : FatTest
    {
        public class PersonModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public bool IsActive { get; set; }
        }

        [JsonFileData("files/persons.json")]
        public void Test_one_inline_param(string firstName)
        {
            LogInfo("firstName: {0}", firstName);
        }

        [JsonFileData("files/persons.json")]
        public void Test_multiple_inline_values(string firstName, string lastName)
        {
            LogInfo("lastName: {0}", lastName);
        }

        [JsonFileData("files/persons.json")]
        public void Test_different_inline_types(int age, bool isActive)
        {
            LogInfo("age: {0}", age);
            LogInfo("isActive: {0}", isActive);
        }

        [JsonFileData("files/persons.json")]
        public void Test_json_object(PersonModel model)
        {
            LogInfo("firstName: {0}", model.FirstName);
        }
    }
}
