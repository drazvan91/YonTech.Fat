namespace Yontech.Fat
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Class, AllowMultiple = true)]
    public class FatLabel : System.Attribute
    {
        public string Name { get; }

        public FatLabel(string name)
        {
            this.Name = name;
        }
    }
}
