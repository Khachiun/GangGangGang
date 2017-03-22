using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extras
{
    class DeltaTime
    {
        DateTime lastLap;

        public float Lap()
        {
            var now = DateTime.Now;
            var delta = (now - lastLap).Milliseconds;
            lastLap = now;
            return delta;
        }

        public float elapsed()
        {
            return (DateTime.Now - lastLap).Milliseconds;
        }

        public static implicit operator float(DeltaTime dt)
        {
            return dt.elapsed();
        }
    }
}
