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

            RectangleShape shape = new RectangleShape(rect, new Texture("Content/Assets/Textures/Concept_Kristal.png"));
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

        public virtual void Interacte(TileEntity entity)
        {
            this.entity = entity;
            TileMap map = Parent.Parent as TileMap;
            map.RemoveEntity(entity);
        }

    }


    class Cristal
    {

    }
}
