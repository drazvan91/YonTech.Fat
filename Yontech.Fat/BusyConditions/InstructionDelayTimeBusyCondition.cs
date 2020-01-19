using System.Threading;

namespace Yontech.Fat.BusyConditions
{
  public class InstructionDelayTimeBusyCondition : IBusyCondition
  {
    public int DelayTime { get; set; }

    public InstructionDelayTimeBusyCondition(int delay = 0)
    {
      this.DelayTime = delay;
    }

    public bool IsBusy(IWebBrowser webBrowser)
    {
      Thread.Sleep(this.DelayTime);
      return false;
    }
  }
}