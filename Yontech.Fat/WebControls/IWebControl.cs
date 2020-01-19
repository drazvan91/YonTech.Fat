namespace Yontech.Fat
{
  public interface IWebControl
  {
    void ScrollTo();
    void ShouldBeVisible();
    void ShouldNotBeVisible();
    bool IsVisible { get; }
    bool Exists { get; }
    bool IsDisabled { get; }
  }
}
