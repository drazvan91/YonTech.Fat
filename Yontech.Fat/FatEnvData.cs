namespace Yontech.Fat
{
    public abstract class FatEnvData
    {
        internal string FilePath { get; private set; }

        public FatEnvData(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
