namespace Yontech.Fat.BusyConditions
{
    internal class InstructionDelayTimeBusyCondition : FatBusyCondition
    {
        public int DelayTime { get; set; }

        public InstructionDelayTimeBusyCondition(int delay = 0)
        {
            this.DelayTime = delay;
        }

        protected internal override bool IsBusy()
        {
            Wait(this.DelayTime);
            return false;
        }
    }
}
