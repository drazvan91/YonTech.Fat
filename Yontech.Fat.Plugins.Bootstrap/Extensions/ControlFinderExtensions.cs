using Yontech.Fat;
using Yontech.Fat.Plugins;
using Yontech.Fat.Plugins.Bootstrap;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Plugins.Bootstrap.Extensions
{
    public static class ControlFinderExtensions
    {
        public static IDropdownControl BootstrapDropdown(this IControlFinder controlFinder, string parentCssSelector)
        {
            var parentElement = controlFinder.Generic(parentCssSelector);
            return new BootstrapDropdownControl(parentElement);
        }
    }
}
