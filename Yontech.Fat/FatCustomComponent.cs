using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public abstract class FatCustomComponent : BaseFatDiscoverable
    {
        internal IWebElement WebElement { get; set; }
        protected internal IGenericControl Container { get; internal set; }
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "This is an alias to improve readability")]
        protected internal IControlFinder _ => Container.ControlFinder;

        protected internal IControlFinder ControlFinder => _;

        public void WaitToDisappear()
        {
            this.Container.WaitToDisappear();
        }
    }
}
