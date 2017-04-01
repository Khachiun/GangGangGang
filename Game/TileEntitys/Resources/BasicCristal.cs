using Czaplicki.SFMLE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace GangGang
{
    class BasicCristal : ResourceBase
    {

        static BasicCristal()
        {
            ConvexShape crystal = new ConvexShape(8);

            crystal.SetPoint(0, new Vector2f(0, 0));
            crystal.SetPoint(1, new Vector2f(1, 1));
            crystal.SetPoint(2, new Vector2f(0, 2));
            crystal.SetPoint(3, new Vector2f(-1, 1));

            Random rn = new Random();

            crystal.Scale = new Vector2f( rn.Next(6, 12), rn.Next(8, 16));
            crystal.FillColor = Color.Cyan;
            crystal.Position = new Vector2f( 0, -16);

            DrawComponent.Regiser("Cristal", crystal);
        }

        int ueses = 3;
        int useCount = 0;
        int amount = 10;
        
        TileEntity entity;
        public BasicCristal(int x, int y) : base(x, y)
        {
            DrawComponent draw = new DrawComponent("Cristal", Layer.UNIT_BASE);


            draw.Offset += Hexagon.OFFSET_TO_CENTER;
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
