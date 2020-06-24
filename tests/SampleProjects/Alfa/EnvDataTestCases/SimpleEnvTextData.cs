using Yontech.Fat;
using Yontech.Fat.Logging;

namespace Alfa
{
    public class SimpleEnvTextData : FatEnvData
    {
        public SimpleEnvTextData()
            : base("files/simple-env-data.txt")
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
