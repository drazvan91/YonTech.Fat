using System.Diagnostics.CodeAnalysis;

namespace Yontech.Fat
{
    public abstract class FatBusyCondition : BaseFatDiscoverable
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "This is an alias to improve readability")]
        protected internal IControlFinder _ => WebBrowser.ControlFinder;
        protected internal int WaitSessionPollingNumber { get; set; }

        protected internal abstract bool IsBusy();

        public FatBusyCondition()
        {
            SinkableLogs = false;
        }
    }
}
