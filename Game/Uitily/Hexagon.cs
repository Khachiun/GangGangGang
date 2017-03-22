using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    static class Hexagon
    {

        public static float HEX_SIZE { get; } = 32;
        public static float HEX_H { get; } = 0.5f * HEX_SIZE;
        public static float HEX_R { get; } = 0.8660254f * HEX_SIZE;
        public static float HEX_A { get; } = 2 * HEX_R;
        public static float HEX_B { get; } = HEX_SIZE + HEX_R + HEX_R;

        //, hexH, hexR, hexA, hexB;

        public static Vector2f OFFSET_TO_CENTER { get; private set; }
        static Hexagon()
        {
            //sätter värdena för konstanterna

            float f = (float)Math.Sin(0.523598776);
            float fw = (float)Math.Cos(0.523598776);
            ////hexSize = 32;
            //HEX_H = (float)Math.Sin(0.523598776) * HEX_SIZE;
            //HEX_R = (float)Math.Cos(0.523598776) * HEX_SIZE;
            //HEX_A = 2 * HEX_R;
            //HEX_B = HEX_SIZE + 2 * HEX_H;

            OFFSET_TO_CENTER = new Vector2f(HEX_H, HEX_R);
        }

        public static ConvexShape GenHexagon()
        {
            //Skaper en convexShape
            ConvexShape Hexagon = new ConvexShape(7);

            //filler i alla vertiser för hexagoen, med hjälp av constanterna
            Hexagon.SetPoint(0, new Vector2f(0, 0));
            Hexagon.SetPoint(1, new Vector2f(HEX_SIZE, 0));
            Hexagon.SetPoint(2, new Vector2f(HEX_SIZE + HEX_H, HEX_R));
            Hexagon.SetPoint(3, new Vector2f(HEX_SIZE, HEX_R * 2));
            Hexagon.SetPoint(4, new Vector2f(0, HEX_R * 2));
            Hexagon.SetPoint(5, new Vector2f(-HEX_H, HEX_R));
            Hexagon.SetPoint(6, new Vector2f(0, 0));

            //fixar med outlinen
            Hexagon.OutlineColor = Color.Black;
            Hexagon.OutlineThickness = 2;
            Hexagon.FillColor = new Color(24, 24, 24);

            //retunerar den nyskappade hexagonen
            return Hexagon;
        }

        public static Vector2f TRANSLATE(int x, int y)
        {
            return new Vector2f(x * (Hexagon.HEX_SIZE + Hexagon.HEX_H), y * 2 * Hexagon.HEX_R + (x * -Hexagon.HEX_R));
        }
        public static Vector2f TRANSLATE(Vector2i pos)
        {
            return new Vector2f(pos.X * (Hexagon.HEX_SIZE + Hexagon.HEX_H), pos.Y * 2 * Hexagon.HEX_R + (pos.X * -Hexagon.HEX_R));
        }
        public static Vector2i REVERCE(Vector2f pos)
        {
            //return new Vector2f(x * (Hexagon.HEX_SIZE + Hexagon.HEX_H), y * 2 * Hexagon.HEX_R + (x * -Hexagon.HEX_R));

            //return new Vector2i((int)(pos.X / (Hexagon.HEX_SIZE + Hexagon.HEX_H)),
            //    (int)((pos.Y + pos.X * Hexagon.HEX_R) / (2 * Hexagon.HEX_R)));


            return new Vector2i((int)(pos.X / (Hexagon.HEX_SIZE + Hexagon.HEX_H)),
                (int)(pos.Y / 2 / Hexagon.HEX_R + (pos.X / Hexagon.HEX_R) / Math.PI));
            //        pos.Y * 2 * Hexagon.HEX_R + (pos.X * -Hexagon.HEX_R)
        }
    }
}
