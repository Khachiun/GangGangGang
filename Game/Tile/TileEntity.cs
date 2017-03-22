using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public abstract class TileEntity : InteractiveEntity
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

        public int X { get; set; }
        public int Y { get; set; }

        public virtual void RetrivedDamage(int Damage, Entity sender)
        {
            Heath -= Damage;
        }

        public virtual void Destryed(Entity sender) {
            //createParicles
            TileMap map = Parent.Parent as TileMap;
            map.RemoveEntity(this);
        }

        //public virtual void CleanUp() { }
        public virtual void NextTurn()
        {
            Heath += Regen;
            if (Heath > MaxHealth)
            {
                Heath = MaxHealth;
            }
        }

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
                        InWorldList list = new InWorldList(options);
                        this.Adopt(list);
                        Console.WriteLine(yes);
                    }

                }
            }
        }
    }

}
