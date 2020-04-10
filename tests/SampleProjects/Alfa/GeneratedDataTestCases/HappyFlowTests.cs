using System.Collections.Generic;
using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.GeneratedDataTestCases
{
    public class HappFlowTests : FatTest
    {
        [GeneratedData(typeof(PersonDataGenerator))]
        public void Test_person_generator(Person person)
        {
            LogInfo("Person name: {0}", person.FirstName);
        }

        [GeneratedData(typeof(StringDataGenerator))]
        public void Test_string_generator(string value)
        {
            LogInfo("String value: {0}", value);
        }
    }
}
