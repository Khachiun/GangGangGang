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
    class Capital : Building
    {
        static Capital()
        {
            Player.IndexRegisterShape("capitalShape", CreateCapitalShape);
            Player.IndexRegisterShape("capitalTower1", CreateCapitalTower1);
            Player.IndexRegisterShape("capitalTower2", CreateCapitalTower2);
        }

        DrawComponent t1, t2;
        float s1, s2;
        float o1 = 10, o2 = 10;

        public Capital(int x, int y, Player owner) : base(x, y, owner)
        {
            //Adopt(new TextComponent("Capital\n" + Owner.ID, Layer.UI_BASE));

            Adopt(Player.GetIndexedShape("capitalShape", Layer.UNIT_BASE, Owner));
            t1 = Player.GetIndexedShape("capitalTower1", Layer.UNIT_BASE + 1, Owner);
            t2 = Player.GetIndexedShape("capitalTower2", Layer.UNIT_BASE + 2, Owner);
            Adopt(t1, t2);
            this.MaxHealth = 20;
            this.Heath = 10;

        }


        public override void Update()
        {
            base.Update();
            s1 += 0.020f;
            t1.Offset = new Vector2f(t1.Offset.X, o1 * (float)Math.Sin(s1));
            s2 += 0.01f;
            t2.Offset = new Vector2f(t2.Offset.X, o1 * (float)Math.Sin(s2));
           

        }


        //Creating shit ton of shapes
        static ConvexShape CreateCapitalShape()
        {
            ConvexShape capitalShape = new ConvexShape(3);

            capitalShape.SetPoint(0, new Vector2f(0, -100));
            capitalShape.SetPoint(1, new Vector2f(-26, 0));
            capitalShape.SetPoint(2, new Vector2f(26, 0));

            capitalShape.FillColor = new Color(100, 100, 100);

            capitalShape.Position += Hexagon.OFFSET_TO_CENTER + new Vector2f(0, Hexagon.HEX_H);

            return capitalShape;
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
