using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public abstract class TileEntity : InteractiveEntity, IUseReadTurns
    {
        TextComponent text;


        float HpBarWidth = Hexagon.HEX_WIDTH * 2;
        float HpBarHeigt = Hexagon.HEX_H;
        RectangleShape HpBarGreen, HpBarRed;
        DrawComponent hpGreen, hpRed;

        private void CreateHpbar()
        {
            HpBarGreen = new RectangleShape()
            {
                FillColor = Color.Green
            };
            HpBarRed = new RectangleShape()
            {
                FillColor = Color.Red, OutlineThickness = 2, OutlineColor = Color.Magenta
                
            };
            UpdateHpBar();
            hpGreen = new DrawComponent(HpBarGreen, Layer.UI_BASE + 1);
            hpRed = new DrawComponent(HpBarRed, Layer.UI_BASE );
            Adopt(hpGreen);
            Adopt(hpRed);

        }
        private void UpdateHpBar()
        {
            ((Shape)HpBarGreen).Scale = new Vector2f(Heath / MaxHealth * HpBarWidth, HpBarHeigt);
            //HpBarGreen.Scale *= 6;
            HpBarRed.Size = new Vector2f(Heath / MaxHealth * HpBarWidth, HpBarHeigt);
        }

        public TileEntity(int x, int y, CollitionComponent collition, Player owner = null) : base(collition)
        {
            collition.Offset += Hexagon.OFFSET_TO_CENTER;
            this.X = x;
            this.Y = y;

            text = new TextComponent(Heath.ToString(), GangGang.Priority.UNIT_BASE + 1);
            text.Offset += new SFML.System.Vector2f(0, -Hexagon.HEX_H);
            text.Color = Color.White;
            text.Enable = false;
            Adopt(text);

            Priority = GangGang.Priority.UNIT_BASE;
            this.Owner = owner;


            CreateHpbar();
        }
        public TileEntity(int x, int y, Player owner) : this(x, y, new CircleCollition(Hexagon.HEX_R), owner)
        {

        }

        public Player Owner { get; set; }


        private int health = 0;
        public int Heath
        {
            get { return health; }
            set
            {
                health = value;
                text.Text = Heath.ToString();
                
                
            }
        }
        public int MaxHealth { get; set; } = 1;
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

        public virtual void Destryed(Entity sender)
        {
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

        public override void Hover(bool yes)
        {
            base.Hover(yes);
            text.Enable = yes;
            //hpGreen.Enable = yes;
            //hpRed.Enable = yes;
        }
    }

}
