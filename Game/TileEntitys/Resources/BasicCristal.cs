using Czaplicki.SFMLE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class BasicCristal : ResourceBase
    {

        static BasicCristal()
        {
            Square rect = new Square(Hexagon.HEX_SIZE * 1, Hexagon.HEX_SIZE * 2);

            RectangleShape shape = new RectangleShape(rect, new Texture("Content/Assets/Textures/Concept_Kristal.png"));
            DrawComponent.Regiser("Cristal", shape);
        }

        int ueses = 3;
        int useCount = 0;
        int amount = 10;


        TileEntity entity;
        public BasicCristal(int x, int y) : base(x, y)
        {
            DrawComponent draw = new DrawComponent("Cristal", Layer.UNIT_BASE);
            draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            Adopt(draw);
        }

        public override void Interacte(TileEntity entity)
        {
            this.entity = entity;
            TileMap map = Parent.Parent as TileMap;
            map.RemoveEntity(entity);
        }
        public override void OnNewTurn()
        {
            if (useCount >= ueses)
            {
                TileMap map = Parent.Parent as TileMap;
                map.RemoveEntity(this);
                entity.X = X;
                entity.Y = Y;
                map.AddTileEntity(entity);
            }
            if (entity != null && entity.Owner != null)
            {
                entity.Owner.Cristals += amount;
                useCount++;
            }
            base.OnNewTurn();
        }
    }
}
