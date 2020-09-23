using System.Diagnostics.CodeAnalysis;

namespace Yontech.Fat
{
    public enum BrowserType
    {
        Chrome,
        InternetExplorer,
        Firefox,
        Opera,
    }

    public enum ChromeVersion : int
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v74 = 74,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v79 = 79,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v80 = 80,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v81 = 81,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v83 = 83,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v84 = 84,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v85 = 85,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v86 = 86,
    }

    public enum FirefoxVersion : int
    {
        Latest = 0,
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It describes the version")]
        v026 = 26,
    }
}
