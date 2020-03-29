using OpenQA.Selenium;

namespace Yontech.Fat.Selenium.SeleniumExtensions
{
    internal static class IWebElementExtensions
    {
        public static bool IsClickable(this IWebElement webElement)
        {
            return webElement != null && webElement.Displayed && webElement.Enabled;
        }
    }
}
