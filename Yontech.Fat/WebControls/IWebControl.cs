namespace Yontech.Fat
{
    public interface IWebControl
    {
        void ScrollTo();
        void ShouldBeVisible();
        void ShouldNotBeVisible();

        string SelectorDescription { get; }

        bool IsVisible { get; }
        bool Exists { get; }
        bool IsDisabled { get; }

        string GetAttribute(string attributeName);
        string GetCssPropertyValue(string propertyName);

        string[] CssClasses { get; }
        string CssClass { get; }
        bool IsClickable { get; }

        void Click();
        void Click(int relativeX, int relativeY);
        void DoubleClick();
        void DoubleClick(int relativeX, int relativeY);

        void RageClick(int numberOfClicks);

        void Hover();
        void DragAndDrop(int offsetX, int offsetY);

        void WaitToDisappear();
        void WaitToAppear();

        void WaitForClickable();
    }
}
