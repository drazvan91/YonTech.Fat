namespace Yontech.Fat.Labels
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class, AllowMultiple = true)]
    public class SkipTest : System.Attribute
    {
        public SkipTest()
        {
        }
    }
}
