using System.Diagnostics.CodeAnalysis;

namespace Yontech.Fat
{
    public abstract class FatPage : BaseFatDiscoverable
    {
        /// ControlFinder is the same as _. We recommend using _ to increase readability
        protected internal IControlFinder ControlFinder => _;

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "This is an alias to improve readability")]
        protected internal IControlFinder _ => WebBrowser.ControlFinder;
    }
}
