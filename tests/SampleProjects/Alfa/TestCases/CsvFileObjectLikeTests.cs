using Yontech.Fat;
using Yontech.Fat.DataSources;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    public class CsvFileObjectLike : FatTest
    {
        [CsvFileData("files/persons.csv")]
        public void Test_ok(PersonModel person)
        {
            LogInfo("name: {0}", person.Name);
            LogInfo("age: {0}", person.Age);
        }

        public class PersonModel
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
