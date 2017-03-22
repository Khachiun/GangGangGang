using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class Tile : Entity
    {

        static Tile()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();

            DrawManager.Register.Add("Hexagon", hexagon);
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

    class TileMap : Entity
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

    abstract class TileEntity : InteractivEntity
    {
        public TileEntity(int x, int y, CollitionComponent collition) : base(collition)
        {
            collition.Offset += Hexagon.OFFSET_TO_CENTER;
            this.X = x;
            this.Y = y;

            Priority = GangGang.Priority.UNIT_BASE;
        }

        public Player Owner { get; set; }

        public int Heath { get; set; }
        public int MaxHealth { get; set; }
        public int Regen { get; set; }
        //public int ___ { get; set; }
        //public int ___ { get; set; }
        //public int ___ { get; set; }
        //public int ___ { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public virtual void RetrivedDamage(int Damage, Entity sender) { }

        public virtual void Destryed(Entity sender) { }

        

        public virtual void CleanUp() { }



        public override void Click(bool yes)
        {
            if (yes)
            {
                if (Owner == null || Owner == Game.CurrrentPlayer)
                {

                    List<Option> options = new List<Option>();
                    this.FetchAll<Option>(ref options);
                    if (options.Count > 0)
                    {
                        List list = new List(options);
                        this.Adopt(list);
                        Console.WriteLine(yes);
                    }

                }
            }
        }
    }


}
