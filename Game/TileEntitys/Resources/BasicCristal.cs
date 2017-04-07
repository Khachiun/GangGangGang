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

            crystal.Scale = new Vector2f(rnd.Next(8, 12), rnd.Next(12, 16)) * 0.6f;
            crystal.FillColor = new Color((byte)rnd.Next(100, 150), (byte)rnd.Next(200, 250), (byte)rnd.Next(200, 250));
            crystal.OutlineColor = Color.Black;
            //crystal.OutlineThickness = 0.1f;

            float ix = (float)rnd.NextDouble();
            float iy = (float)rnd.NextDouble();

            ix *= 32;
            iy *= 64;

            crystal.Position = new Vector2f( ix, iy - 10 );

            return crystal;

            
        }


        int ueses = 3;
        int useCount = 0;
        int amount = 10;

        DrawComponent[] crystals;
        float[] refSin;
        float[] speed;

        TileEntity entity;
        public BasicCrystal(int x, int y) : base(x, y)
        {
            int crystalAmount = rnd.Next(3,5);

            crystals = new DrawComponent[crystalAmount];
            refSin = new float[ crystalAmount ];
            speed = new float[crystalAmount];

            for (int i = 0; i < refSin.Length; i++)
            {
                refSin[i] = 0;
                speed[i] = rnd.Next(1,4);
            }
            for (int i = 0; i < crystalAmount; i++)
            {
                crystals[i]= new DrawComponent(createRndCrystal(), Layer.UNIT_BASE + i);
                Adopt(crystals[i]);
            }

            //draw.Offset += Hexagon.OFFSET_TO_CENTER;
            //Adopt(draw);
            this.MaxHealth = 100;
            this.Regen = 5;
            
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < refSin.Length; i++)
            {
                refSin[i] += ( rnd.Next(0,50) / 10 + 1) * 0.01f;
            }

            for (int i = 0; i < crystals.Length; i++)
            {
                crystals[i].Offset = new Vector2f((float)Math.Cos((double)refSin[i]) * speed[i] / 2, (float)Math.Sin((double)refSin[i]) * speed[i] / 1 );
            }
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
