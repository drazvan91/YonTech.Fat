
namespace Yontech.Fat
{
    public abstract class FatPageSection : BaseFatDiscoverable
    {
        internal protected IControlFinder _ => WebBrowser.ControlFinder;
        internal protected IControlFinder ControlFinder => _;
    }
}
