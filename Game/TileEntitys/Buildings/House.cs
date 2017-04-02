using Czaplicki.SFMLE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class House : Building
    {
        static House()
        {
            //RectangleShape shape = new RectangleShape(new Square() * Hexagon.HEX_SIZE * 2, new Texture("Content/Assets/Textures/Building.png"));
           // DrawComponent.Regiser("Buliding", shape);
        }
        public House(int x, int y, Player owner) : base(x, y, owner)
        {
            SpawnTileEntityComponent spawn = new SpawnTileEntityComponent("Spawn", 10, 3, (X, Y) => new Worker(X, Y, Owner));
            spawn.Enable = false;
            Adopt(spawn);
            //DrawComponent draw = new DrawComponent("Buliding", Layer.UNIT_BASE);
            //draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            //Adopt(draw);

        }
    }
}
