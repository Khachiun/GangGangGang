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
    class Worker : TileEntity
    {
        static Worker()
        {
            ConvexShape headShape = new ConvexShape(4);
            //fill with vertexes
            headShape.SetPoint(0, new Vector2f(0, 0));
            headShape.SetPoint(1, new Vector2f(-1, -1));
            headShape.SetPoint(2, new Vector2f(0, -2));
            headShape.SetPoint(3, new Vector2f(1, -1));

            headShape.Position = new Vector2f( Hexagon.HEX_H, Hexagon.HEX_R - 4 );
            headShape.Scale = new Vector2f(16, 24);

            DrawComponent.Regiser("WorkerHead", headShape);

            ConvexShape bodyShape = new ConvexShape(4);
            //fill with vertexes
            bodyShape.SetPoint(0, new Vector2f(0, 0));
            bodyShape.SetPoint(1, new Vector2f(-1, -1));
            bodyShape.SetPoint(2, new Vector2f(0, -2));
            bodyShape.SetPoint(3, new Vector2f(1, -1));

            bodyShape.FillColor = Color.Blue;
            bodyShape.Position = new Vector2f(Hexagon.HEX_H, Hexagon.HEX_R + 8);
            bodyShape.Scale = new Vector2f(24, 16);

            DrawComponent.Regiser("WorkerBody", bodyShape);

            ConvexShape feetShape = new ConvexShape(4);
            //fill with vertexes
            feetShape.SetPoint(0, new Vector2f(-12, 0));
            feetShape.SetPoint(1, new Vector2f(-6, -6));
            feetShape.SetPoint(2, new Vector2f(6, -6));
            feetShape.SetPoint(3, new Vector2f(12, 0));

            feetShape.FillColor = Color.Magenta;
            feetShape.Position = new Vector2f(Hexagon.HEX_H, Hexagon.HEX_R + 14);
            feetShape.Scale = new Vector2f(1.5f, 3);

            DrawComponent.Regiser("WorkerFeet", feetShape);





        }

        DrawComponent head, body, feet;

        public Worker(int x, int y, Player owner) : base(x, y, new CircleCollition(Hexagon.HEX_R), owner)
        {
            DrawComponent body = new DrawComponent("WorkerBody", Layer.UNIT_BASE + 1);
            DrawComponent head = new DrawComponent("WorkerHead", Layer.UNIT_BASE + 2);
            DrawComponent feet = new DrawComponent("WorkerFeet", Layer.UNIT_BASE);
            
            Adopt(body);
            Adopt(head);
            Adopt(feet);

            Heath = 10;
            MaxHealth = 10;
            Regen = 1;

            //Option o = new Option();
            //o.UiName = "option";
            //Adopt(o);

            WorkComponent work = new WorkComponent();
            Adopt(work);

            AttackComponent attack = new AttackComponent(5, 3);
            Adopt(attack);


            SpawnTileEntityComponent spawn = new SpawnTileEntityComponent("build C:" + "", 40, 2, (dx, dy) => new Building(dx, dy, Owner));
            Adopt(spawn);

            MoveComponent move = new MoveComponent();
            Adopt(move);

            //MoveComponent move = new MoveComponent(movementPattern);
            //Adopt(move);


            //WorkComponent work = new WorkComponent();
            //Adopt(work);
        }

        public override void Click(bool yes)
        {
            base.Click(yes);
        }
        public override void Hover(bool yes)
        {

        }
    }


}
