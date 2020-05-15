using Yontech.Fat;
using Yontech.Fat.DataSources;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    public class CsvFileObjectLikeErrors : FatTest
    {
        [CsvFileData("files/persons.csv")]
        public void Test_column_does_not_exist(PersonModel person)
        {
            LogInfo("name: {0}!", person.SomeColumn);
            LogInfo("age: {0}", person.Age);
        }

        public class PersonModel
        {
            public string SomeColumn { get; set; }
            public int Age { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
