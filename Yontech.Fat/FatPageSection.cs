﻿using System.Diagnostics.CodeAnalysis;

namespace Yontech.Fat
{
    public abstract class FatPageSection : BaseFatDiscoverable
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "This is an alias to improve readability")]
        protected internal IControlFinder _ => WebBrowser.ControlFinder;
        protected internal IControlFinder ControlFinder => _;
    }
}
