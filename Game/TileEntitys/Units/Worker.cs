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
        static List<Vector2i> movementPattern;
        static Worker()
        {
            RectangleShape shape = new RectangleShape(new Square() * Hexagon.HEX_SIZE * 2, new Texture("Content/Assets/Textures/Ponn.png"));
            DrawComponent.Regiser("Worker", shape);

            movementPattern = new List<Vector2i>() {
                new Vector2i(-1,-1),
                new Vector2i( 0,-1),
                new Vector2i( 1, 0),
                new Vector2i( 1, 1),
                new Vector2i( 0, 1),
                new Vector2i(-1, 0)

            };

        }
        public Worker(int x, int y) : base(x, y, new CircleCollition(Hexagon.HEX_R))
        {
            DrawComponent draw = new DrawComponent("Worker", Layer.UNIT_BASE);
            draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            Adopt(draw);

            //Option o = new Option();
            //o.UiName = "option";
            //Adopt(o);

            WorkComponent work = new WorkComponent();
            Adopt(work);


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
