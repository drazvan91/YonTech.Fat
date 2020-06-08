using System.Diagnostics.CodeAnalysis;

namespace Yontech.Fat
{
    public abstract class FatWarmup : BaseFatDiscoverable
    {
        public string WarmupName { get; protected set; }

        public FatWarmup()
        {
            this.WarmupName = this.GetType().FullName;
        }

        /// ControlFinder is the same as _. We recommend using _ to increase readability
        protected internal IControlFinder ControlFinder => _;

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "This is an alias to improve readability")]
        protected internal IControlFinder _ => WebBrowser.ControlFinder;

        protected internal abstract void Warmup();
    }
}
