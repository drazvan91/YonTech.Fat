using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Yontech.Fat.Waiters
{
    public class Waiter
    {
        public static void WaitForConditionToBeTrue(Func<bool> condition, int timeout)
        {
            DateTime timeoutDate = DateTime.Now.AddMilliseconds(timeout);
            while (DateTime.Now < timeoutDate)
            {
                if (condition() == true)
                    return;

                Wait(50);
            }

            // todo: specific exception please
            throw new Exception("Operation did timeout. One or more busy conditions indicate that Browser is still busy.");
        }

        public static void Wait(int timeToWait)
        {
            Thread.CurrentThread.Join(timeToWait);
        }
    }
}
