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
    class BasicCrystal : ResourceBase
    {
        static Random rnd = new Random();

        static ConvexShape createRndCrystal()
        {
            ConvexShape crystal = new ConvexShape(8);

            crystal.SetPoint(0, new Vector2f(0, 0));
            crystal.SetPoint(1, new Vector2f(1, 1));
            crystal.SetPoint(2, new Vector2f(0, 2));
            crystal.SetPoint(3, new Vector2f(-1, 1));

            crystal.Scale = new Vector2f(rnd.Next(8, 12), rnd.Next(14, 24));
            crystal.FillColor = Color.Cyan;
            crystal.OutlineColor = Color.Black;
            crystal.OutlineThickness = 0.1f;
            crystal.Position = new Vector2f( rnd.Next( -26, 26 ), rnd.Next( -26, -16) );
            crystal.Position += Hexagon.OFFSET_TO_CENTER;

            return crystal;
        }


        int ueses = 3;
        int useCount = 0;
        int amount = 10;

        TileEntity entity;
        public BasicCrystal(int x, int y) : base(x, y)
        {
            for (int i = 0; i < rnd.Next(3, 5); i++)
            {
                DrawComponent draw = new DrawComponent(createRndCrystal(), Layer.UNIT_BASE + i);
                Adopt(draw);
            }

            //draw.Offset += Hexagon.OFFSET_TO_CENTER;
            //Adopt(draw);
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
