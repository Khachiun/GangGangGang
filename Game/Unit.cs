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

    class Resource : TileEntity
    {

        static Resource()
        {
            Square rect = new Square(Hexagon.HEX_SIZE * 1, Hexagon.HEX_SIZE * 2);

            RectangelShape shape = new RectangelShape(rect, new Texture("Content/Assets/Textures/Concept_Kristal.png"));
            DrawManager.Register.Add("Cristal", shape);

        }

        int ueses = 3;
        int useCount = 0;
        int amount = 10;

        private TileEntity entity;

        public Resource(int x, int y) : base(x, y, new CircleCollition(Hexagon.HEX_R))
        {
            Game.NextTurnEvent += Game_NextTurnEvent;
            DrawComponent draw = new DrawComponent("Cristal", Layer.UNIT_BASE);
            draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            Adopt(draw);
        }

        private void Game_NextTurnEvent()
        {
            if (useCount >= ueses)
            {
                TileMap map = Parent.Parent as TileMap;
                map.RemoveEntity(this);
                map.AddTileEntity(entity);
            }
            if (entity != null && entity.Owner != null)
            {
                entity.Owner.Cristals += amount;
                useCount++;
            }

        }

        public void SetOwner(TileEntity entity)
        {
            this.entity = entity;
            TileMap map = Parent.Parent as TileMap;
            map.RemoveEntity(entity);
        }

    }


    class Worker : TileEntity
    {
        static List<Vector2i> movementPattern;
        static Worker()
        {
            RectangelShape shape = new RectangelShape(new Square() * Hexagon.HEX_SIZE * 2, new Texture("Content/Assets/Textures/Ponn.png"));
            DrawManager.Register.Add("Worker", shape);

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

            MyWorkComponent work = new MyWorkComponent();
            Adopt(work);


            MyMoveComponent move = new MyMoveComponent();
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
