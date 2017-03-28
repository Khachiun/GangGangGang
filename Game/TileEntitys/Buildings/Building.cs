using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using Czaplicki.SFMLE.Extentions;
using Czaplicki.SFMLE;

namespace GangGang
{

 
    class Building : TileEntity
    {
        static Building()
        {
            RectangleShape shape = new RectangleShape(new Square() * Hexagon.HEX_SIZE * 2, new Texture("Content/Assets/Textures/Building.png"));
            DrawComponent.Regiser("Buliding", shape);
        }

        public Building(int x, int y) : base(x, y, new CircleCollition(Hexagon.HEX_R))
        {
            Adopt(new CharecterSpawnComponent(3, (X, Y) => new Worker(X, Y, Owner)));
            DrawComponent draw = new DrawComponent("Buliding", Layer.UNIT_BASE);
            draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            Adopt(draw);

        }
    }
}
