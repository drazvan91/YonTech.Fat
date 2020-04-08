using Yontech.Fat;
using Yontech.Fat.Logging;

namespace Alfa
{
    public class FileNotFoundEnvData : FatEnvData
    {
        public FileNotFoundEnvData()
            : base("files/no-file.json")
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
