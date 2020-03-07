namespace Yontech.Fat
{
    public abstract class FatBusyCondition : BaseFatDiscoverable
    {
        protected internal IControlFinder _ => WebBrowser.ControlFinder;
        protected internal int WaitSessionPollingNumber { get; set; }

        protected internal abstract bool IsBusy();
    }
}
