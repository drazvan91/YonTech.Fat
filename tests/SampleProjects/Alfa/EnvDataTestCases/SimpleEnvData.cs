using Yontech.Fat;
using Yontech.Fat.Logging;

namespace Alfa
{
    public class SimpleEnvData : FatEnvData
    {
        public SimpleEnvData()
            : base("files/simple-env-data.json")
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
