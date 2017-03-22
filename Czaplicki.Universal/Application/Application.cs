#define debug

using Czaplicki.Universal.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Czaplicki.Universal.Application
{
    public class CallDistrutibuterDeltaTime
    {
        public event Action Initilze;
        public event Action<float> EarlyEarlyTick;
        public event Action<float> EarlyTick;
        public event Action<float> TickEvent;
        public event Action<float> LateTick;
        public event Action<float> LateLateTick;

        public void TakeOver(ref bool isRunning)
        {
            Initilze?.Invoke();
            DeltaTime deltaTime = new DeltaTime();
            while (isRunning)
            {
#if debug
                Thread.Sleep(1);
#endif
                deltaTime.Lap();
                EarlyEarlyTick?.Invoke(deltaTime);
                EarlyTick?.Invoke(deltaTime);
                TickEvent?.Invoke(deltaTime);
                LateTick?.Invoke(deltaTime);
                LateLateTick?.Invoke(deltaTime);
            }
        }

    }

}
