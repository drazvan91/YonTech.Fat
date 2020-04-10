using System.Collections.Generic;
using Yontech.Fat.DataSources;

namespace Alfa.GeneratedDataTestCases
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonDataGenerator : DataGenerator<Person>
    {
        public override IEnumerable<Person> Generate()
        {
            for (int i = 1; i <= 5; i++)
            {
                yield return new Person()
                {
                    FirstName = "Razvan " + i,
                    LastName = "Dragomir",
                };
            }
        }
    }

    public class StringDataGenerator : DataGenerator<string>
    {
        public override IEnumerable<string> Generate()
        {
            for (int i = 1; i < 3; i++)
            {
                yield return "string number " + i;
            }
        }
    }
}
