using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public class Player
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int Cristals { get; set; }

        public static Color[] colors;
        static Player()
        {
            colors = new Color[3];
            colors[0] = Color.White;
            colors[1] = Color.Red;
            colors[2] = Color.Blue;

        }
        public static void IndexRegisterShape(string id, Func<Shape> createShape)
        {
            for (int i = 0; i < Game.playerCount + 1; i++)
            {
                Shape s = createShape();
                s.FillColor += colors[i];
                string regId = id + i;
                DrawComponent.Regiser(regId, s);
            }
        }
        public static DrawComponent GetIndexedShape(string id, int layer, Player owner)
        {
            int ownerId = owner?.ID + 1 ?? 0;
            return new DrawComponent(id + ownerId, layer);
        }
    }

}
