using System.Collections.Generic;
using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.GeneratedDataTestCases
{
    public class PropertyNotFoundTests : FatTest
    {
        [GeneratedData(typeof(PersonDataGenerator))]
        public void Test_person_generator(int age)
        {
        }
    }
}
