﻿using Yontech.Fat;
using Yontech.Fat.DataSources;

namespace Alfa.JsonFileDataTestCases
{
    public class TypeNotSupportedTests : FatTest
    {
        public class AddressModel { }

        [JsonFileData("files/persons.json")]
        public void Test_type_not_supported(int age, AddressModel address)
        {
        }
    }
}
