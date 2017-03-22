using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extras
{
    public class For2d
    {
        public int X, Y;
        protected int i, x, y;
        protected int I
        {
            get { return i; }
            set
            {
                i = value;
                X = i / y;
                Y = i % y;
            }
        }

        public For2d(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.I = 0;
        }
        public For2d(float x, float y)
        {
            this.x = (int)x;
            this.y = (int)y;
            this.I = 0;
        }

        public static For2d operator +(For2d f, int i)
        {
            f.I += i;
            return f;
        }
        public static For2d operator -(For2d f, int i)
        {
            f.I -= i;
            return f;
        }

        public static For2d operator ++(For2d f)
        {
            f.I++;
            return f;
        }
        public static implicit operator int(For2d f)
        {
            return f.I;
        }
        public static implicit operator bool(For2d f)
        {
            return f.I < (f.x * f.y);
        }
    }
}
