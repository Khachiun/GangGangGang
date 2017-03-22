using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{

    

    public class Tile : Entity
    {

        static Tile()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();

            DrawComponent.Regiser("Hexagon", hexagon);
        }

        public TileEntity Entity { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Tile(int x, int y)
        {
            this.X = x;
            this.Y = y;

            base.Offset = Hexagon.TRANSLATE(x, y);

            //adds a grafik
            base.Adopt(new DrawComponent("Hexagon", Layer.TILE_BASE));

            //Add Collition Check
            CollitionComponent col = new CircleCollition(Hexagon.HEX_R, Hexagon.OFFSET_TO_CENTER);
            col.SetTag("Tile");
            base.Adopt(col);
        }
        public override void Update()
        {



            base.Update();
        }


    }

    public class TileMap : Entity
    {

        Tile[,] array;
        public TileMap(Vector2i mapSize)
        {
            array = new Tile[mapSize.X + mapSize.Y, mapSize.Y];


            for (int y = 0; y < mapSize.Y; y++)
                for (int x = y; x < y + mapSize.X; x++)
                {
                    Tile t = new Tile(x, y);
                    array[x, y] = t;
                    base.Adopt(t);
                }


        }
        public Tile this[int x, int y]
        {
            get
            {
                if (x > -1 && y > -1 && x < array.GetLength(0) && y < array.GetLength(1))
                {
                    return array[x, y];

                }
                return null;
            }
        }
        public void RemoveEntity(TileEntity tileEntity)
        {
            if (tileEntity != null)
            {
                array[tileEntity.X, tileEntity.Y].Entity = null;
                array[tileEntity.X, tileEntity.Y].Reject(tileEntity);
            }
        }
        public void MoveEntity(TileEntity tileEntity, Vector2i pos)
        {
            array[tileEntity.X, tileEntity.Y].Entity = null;
            tileEntity.X = pos.X;
            tileEntity.Y = pos.Y;
            array[pos.X, pos.Y].Adopt(tileEntity);
            array[pos.X, pos.Y].Entity = tileEntity;
        }

        public void AddTileEntity(TileEntity entity)
        {
            array[entity.X, entity.Y].Entity = entity;
            array[entity.X, entity.Y].Adopt(entity);
        }
    }

    

}
