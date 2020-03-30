using Yontech.Fat.Selenium;

namespace Yontech.Fat.Exceptions
{
    public class MultipleWebControlsFoundException : FatException
    {
        internal MultipleWebControlsFoundException(SelectorNode selectorNode)
            : base($"Multiple web-controls were found for selector '{selectorNode.GetFullPath()}' instead of single one.")
        {
        }
    }
}
