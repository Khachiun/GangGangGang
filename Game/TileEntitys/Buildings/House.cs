using Czaplicki.SFMLE;
using SFML.Graphics;
using SFML.System;
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
            Player.IndexRegisterShape("House1", CreateCapitalTower1);
            Player.IndexRegisterShape("House2", CreateCapitalTower2);
        }
        public House(int x, int y, Player owner) : base(x, y, owner)
        {
            SpawnTileEntityComponent spawn = new SpawnTileEntityComponent("Spawn", 10, 3, (X, Y) => new Worker(X, Y, Owner));
            spawn.Enable = false;
            Adopt(spawn);
            Adopt(Player.GetIndexedShape("House1", Layer.UI_BASE, Owner));
            Adopt(Player.GetIndexedShape("House2", Layer.UI_BASE, Owner));

        }

        static ConvexShape CreateCapitalTower1()
        {
            ConvexShape capitalTower1 = new ConvexShape(3);

            capitalTower1.SetPoint(0, new Vector2f(-20, -64));
            capitalTower1.SetPoint(1, new Vector2f(-34, 0));
            capitalTower1.SetPoint(2, new Vector2f(-3, 0));

            capitalTower1.Position += Hexagon.OFFSET_TO_CENTER + new Vector2f(0, Hexagon.HEX_H * 0);

            capitalTower1.FillColor = new Color(150, 150, 150);

            return capitalTower1;
        }
        static ConvexShape CreateCapitalTower2()
        {
            ConvexShape capitalTower2 = new ConvexShape(3);

            capitalTower2.SetPoint(0, new Vector2f(20, -48));
            capitalTower2.SetPoint(1, new Vector2f(3, 0));
            capitalTower2.SetPoint(2, new Vector2f(34, 0));

            capitalTower2.FillColor = new Color(170, 170, 170);

            capitalTower2.Position += Hexagon.OFFSET_TO_CENTER + new Vector2f(0, Hexagon.HEX_H * -0.5f);


            return capitalTower2;
        }
    }
}
