using Yontech.Fat;
using Yontech.Fat.DataSources;
using Yontech.Fat.Labels;

namespace Alfa.TestCases
{
    public class CsvFile : FatTest
    {
        [CsvFileData("files/csvFile.csv")]
        public void Test_csv_existing_columns(string column1, string column3)
        {
            LogInfo("Value from column1: {0}", column1);
            LogInfo("Value from column3: {0}", column3);
        }

        [CsvFileData("files/file_not_found.csv")]
        public void Test_csv_does_not_exist(string noColumn)
        {
        }
    }
}
