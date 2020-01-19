
namespace Yontech.Fat.ConsoleRunner
{
  public abstract class FatPageSection
  {
    internal protected IControlFinder ControlFinder { get; internal set; }
    internal protected IControlFinder _ { get { return ControlFinder; } }
  }
}