using System.Diagnostics.CodeAnalysis;

namespace Yontech.Fat
{
    public enum BrowserType
    {
        Chrome,
        RemoteChrome,
        InternetExplorer,
        Firefox,
        Opera,
    }

    public enum ChromeVersion : int
    {
        Latest = 0,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v74 = 74,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v79 = 79,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v80 = 80,
    }
}
