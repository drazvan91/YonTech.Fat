using Yontech.Fat;
using Yontech.Fat.Logging;

namespace Alfa
{
    public class OverrideEnvData : FatEnvData
    {
        public OverrideEnvData()
            : base("files/override-env-data.json")
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
