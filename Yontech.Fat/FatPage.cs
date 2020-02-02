
namespace Yontech.Fat
{
    public abstract class FatPage
    {
        /// ControlFinder is the same as _. We recommend using _ to increase readability
        protected internal IControlFinder ControlFinder => _;
        protected internal IControlFinder _ => WebBrowser.ControlFinder;
        protected internal IWebBrowser WebBrowser { get; internal set; }
    }
}