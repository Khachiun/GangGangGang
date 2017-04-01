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
            ConvexShape headShape = new ConvexShape();
            //fill with vertexes

            DrawComponent.Regiser("WorkerHead", headShape);

            ConvexShape bodyShape = new ConvexShape();
            //fill with vertexes
            DrawComponent.Regiser("WorkerBody", bodyShape);

            ConvexShape feetShape = new ConvexShape();
            //fill with vertexes

            DrawComponent.Regiser("WorkerFeet", feetShape);





        }

        DrawComponent head, body, feet;

        public Worker(int x, int y, Player owner) : base(x, y, new CircleCollition(Hexagon.HEX_R), owner)
        {
            DrawComponent head = new DrawComponent("WorkerHead", Layer.UNIT_BASE);
            DrawComponent body = new DrawComponent("WorkerBody", Layer.UNIT_BASE);
            DrawComponent feet = new DrawComponent("WorkerFeet", Layer.UNIT_BASE);
            
            Adopt(head);
            Adopt(body);
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


            SpawnTileEntityComponent spawn = new SpawnTileEntityComponent(2, (dx, dy) => new Building(dx, dy, Owner));
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
