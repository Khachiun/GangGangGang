using Czaplicki.SFMLE;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extentions
{
    class Convert
    {
        // tex 10,10
        public static Vector2f ToVector2f(string value)
        {
            string[] args = value.Split(',');
            float x = System.Convert.ToSingle(args[0]);
            float y = System.Convert.ToSingle(args[1]);
            return new Vector2f(x, y);

        }
        // 10,10,10,10 = x,y,w,h
        public static Square ToSquare(string value)
        {
            string[] args = value.Split(',');
            float x = System.Convert.ToSingle(args[0]);
            float y = System.Convert.ToSingle(args[1]);
            float w = System.Convert.ToSingle(args[3]);
            float h = System.Convert.ToSingle(args[4]);

            return new Square(x, y, w, h);

        }
    }
}
