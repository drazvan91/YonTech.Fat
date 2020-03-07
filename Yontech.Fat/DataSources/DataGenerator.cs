using System.Collections.Generic;

namespace Yontech.Fat.DataSources
{
    public abstract class DataGenerator<T>
    {
        public abstract IEnumerable<T> Generate();
    }
}
