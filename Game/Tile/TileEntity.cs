using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public abstract class TileEntity : InteractiveEntity, IUseReadTurns
    {
        TextComponent text;

        public TileEntity(int x, int y, CollitionComponent collition, Player owner = null) : base(collition)
        {
            collition.Offset += Hexagon.OFFSET_TO_CENTER;
            this.X = x;
            this.Y = y;

            text = new TextComponent(Heath.ToString(), GangGang.Priority.UNIT_BASE + 1);
            text.Offset += new SFML.System.Vector2f(0, -Hexagon.HEX_H);
            text.Color = Color.White;
            Adopt(text);

            Priority = GangGang.Priority.UNIT_BASE;
            this.Owner = owner;
        }
        public TileEntity(int x,int y, Player owner): this(x,y, new CircleCollition(Hexagon.HEX_R), owner)
        {

        }

        public Player Owner { get; set; }



        private int health = 0;
        public int Heath { get { return health; } set { health = value; text.Text = Heath.ToString(); } }
        public int MaxHealth { get; set; }
        public int Regen { get; set; }

        protected float Speed { get; set; } = 0.9f;


        public int X { get; set; }
        public int Y { get; set; }

        public virtual void Move(Vector2i pos)
        {
            Offset = Position - Hexagon.TRANSLATE(pos);
            
            TileMap map = Parent.Parent as TileMap;
            map.MoveEntity(this, pos);
        }

        public override void Update()
        {
            base.Update();

            Offset *= 0.9f;

        }



        public virtual void RetrivedDamage(int Damage, Entity sender)
        {
            Heath -= Damage;
            if (health < 1)
            {
                Destryed(sender);
            }
        }

        public virtual void Destryed(Entity sender) {
            //createParicles
            TileMap map = Parent.Parent as TileMap;
            map.RemoveEntity(this);
        }

        //public virtual void CleanUp() { }
        public virtual void OnNewTurn()
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
                    this.FetchAllActive<Option>(ref options);
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
