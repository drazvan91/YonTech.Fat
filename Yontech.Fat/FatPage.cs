
namespace Yontech.Fat
{
    public abstract class FatPage : BaseFatDiscoverable
    {
        /// ControlFinder is the same as _. We recommend using _ to increase readability
        protected internal IControlFinder ControlFinder => _;
        protected internal IControlFinder _ => WebBrowser.ControlFinder;
    }
}
