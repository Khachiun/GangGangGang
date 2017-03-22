using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public abstract class TileEntity : InteractiveEntity
    {
        TextComponent text;

        public TileEntity(int x, int y, CollitionComponent collition) : base(collition)
        {
            collition.Offset += Hexagon.OFFSET_TO_CENTER;
            this.X = x;
            this.Y = y;

            text = new TextComponent(Heath.ToString(), GangGang.Priority.UNIT_BASE + 1);
            text.Offset += new SFML.System.Vector2f(0, -Hexagon.HEX_H);
            text.Color = Color.Red;
            Adopt(text);

            Priority = GangGang.Priority.UNIT_BASE;
        }

        public Player Owner { get; set; }



        private int health = 0;
        public int Heath { get { return health; } set { health = value; text.Text = Heath.ToString(); } }
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
