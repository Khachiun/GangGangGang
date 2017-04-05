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
        static ConvexShape CreateWorkerHead()
        {
            ConvexShape headShape = new ConvexShape(4);
            //fill with vertexes
            headShape.SetPoint(0, new Vector2f(0, 0));
            headShape.SetPoint(1, new Vector2f(-1, -1));
            headShape.SetPoint(2, new Vector2f(0, -2));
            headShape.SetPoint(3, new Vector2f(1, -1));

            headShape.Position = new Vector2f(Hexagon.HEX_H, Hexagon.HEX_R - 4);
            headShape.Scale = new Vector2f(16, 24);
            headShape.FillColor = new Color(200,200, 200);

            return headShape;
        }
        static ConvexShape CreateWorkerBody()
        {
            ConvexShape bodyShape = new ConvexShape(4);
            //fill with vertexes
            bodyShape.SetPoint(0, new Vector2f(0, 0));
            bodyShape.SetPoint(1, new Vector2f(-1, -1));
            bodyShape.SetPoint(2, new Vector2f(0, -2));
            bodyShape.SetPoint(3, new Vector2f(1, -1));

            bodyShape.FillColor = new Color(150, 150, 150);
            bodyShape.Position = new Vector2f(Hexagon.HEX_H, Hexagon.HEX_R + 8);
            bodyShape.Scale = new Vector2f(24, 16);

            return bodyShape;
        }
        static ConvexShape CreateWorkerFeet()
        {
            ConvexShape feetShape = new ConvexShape(4);
            //fill with vertexes
            feetShape.SetPoint(0, new Vector2f(-12, 0));
            feetShape.SetPoint(1, new Vector2f(-6, -6));
            feetShape.SetPoint(2, new Vector2f(6, -6));
            feetShape.SetPoint(3, new Vector2f(12, 0));

            feetShape.FillColor = new Color(100, 100, 100);
            feetShape.Position = new Vector2f(Hexagon.HEX_H, Hexagon.HEX_R + 14);
            feetShape.Scale = new Vector2f(1.5f, 3);

            return feetShape;
        }
        static Worker()
        {

            Player.IndexRegisterShape("WorkerHead", CreateWorkerHead);
            Player.IndexRegisterShape("WorkerBody", CreateWorkerBody);
            Player.IndexRegisterShape("WorkerFeet", CreateWorkerFeet);
        }

        DrawComponent head, body, feet;
        float h = 1, b, f;
        float ho = 2, bo = 1f, fo;

        public Worker(int x, int y, Player owner) : base(x, y, new CircleCollition(Hexagon.HEX_R), owner)
        {
            body = Player.GetIndexedShape("WorkerBody", Layer.UNIT_BASE + 1, owner);
            head = Player.GetIndexedShape("WorkerHead", Layer.UNIT_BASE + 2, owner);
            feet = Player.GetIndexedShape("WorkerFeet", Layer.UNIT_BASE, owner);
            
            Adopt(body);
            Adopt(head);
            Adopt(feet);

            MaxHealth = 10;
            Heath = 5;
            Regen = 1;

            //Option o = new Option();
            //o.UiName = "option";
            //Adopt(o);

            WorkComponent work = new WorkComponent();
            Adopt(work);

            AttackComponent attack = new AttackComponent(5, 3);
            Adopt(attack);


            SpawnTileEntityComponent spawn = 
                new SpawnTileEntityComponent("Build", 40, 2,
                (dx, dy) => new Contrution(
                    (hx,hy,o) => new House(
                        hx,hy,o), 3, dx,dy, Owner));
            Adopt(spawn);

            MoveComponent move = new MoveComponent();
            Adopt(move);

            //MoveComponent move = new MoveComponent(movementPattern);
            //Adopt(move);


            //WorkComponent work = new WorkComponent();
            //Adopt(work);
        }

        public override void Update()
        {
            base.Update();
            h += 0.04f;
            head.Offset = new Vector2f(head.Offset.X,  ho * (float)Math.Sin(h) - 3);
            b += 0.05f;
            body.Offset = new Vector2f(body.Offset.X, bo * (float)Math.Sin(b));

        }
    }


}
