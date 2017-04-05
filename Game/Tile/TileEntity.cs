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
        Vector2f HpBarOffset = new Vector2f(0, -Hexagon.HEX_H);
        float HpBarWidth = Hexagon.HEX_SIZE;
        float HpBarHeigt = Hexagon.HEX_H / 2.5f;
        RectangleShape HpBarGreen, HpBarRed;
        DrawComponent hpGreen, hpRed;

        private void CreateHpbar()
        {
            HpBarGreen = new RectangleShape(new Vector2f(1, 1))
            {
                FillColor = Color.Green,
                //Position = new Vector2f(0, -Hexagon.HEX_H)

            };
            HpBarRed = new RectangleShape(new Vector2f(HpBarWidth, HpBarHeigt))
            {
                FillColor = Color.Red,
                // OutlineThickness = 1,
                // OutlineColor = Color.Magenta,
                //Position = new Vector2f(0, -Hexagon.HEX_H)

            };
            UpdateHpBar();
            hpGreen = new DrawComponent(HpBarGreen, Layer.UI_BASE + 1);
            hpRed = new DrawComponent(HpBarRed, Layer.UI_BASE);
            Adopt(hpGreen);
            Adopt(hpRed);

        }
        private void UpdateHpBar()
        {
          //  HpBarGreen.Size = new Vector2f(Heath / (float)MaxHealth * HpBarWidth, HpBarHeigt);
            //HpBarGreen.Scale *= 6;
            //HpBarRed.Size = new Vector2f(-((MaxHealth - 1) - (Heath / (float)MaxHealth * HpBarWidth)), HpBarHeigt);
        }

        public TileEntity(int x, int y, CollitionComponent collition, Player owner = null) : base(collition)
        {
            collition.Offset += Hexagon.OFFSET_TO_CENTER;
            this.X = x;
            this.Y = y;
            

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
                this.health = value;
                UpdateHpBar();

            }
        }
        private int maxHealth = 1;
        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }

            set
            {
                this.maxHealth = value;
                UpdateHpBar();
            }
        }
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

            if (hpGreen.Enable && hpRed.Enable)
            {
                hpRed.Offset += (HpBarOffset - hpRed.Offset) * 0.1f;
                hpGreen.Offset += (HpBarOffset - hpGreen.Offset) * 0.1f;

                HpBarRed.FillColor += new Color(0, 0, 0, 5);
                HpBarGreen.FillColor += new Color(0, 0, 0, 5);
                

                float test = hpGreen.Offset.Y / HpBarOffset.Y;
                HpBarGreen.Size = new Vector2f((Heath * Math.Min(1, test)) /(float)MaxHealth * HpBarWidth, HpBarHeigt);

            }
            else
            {
                hpRed.Offset = new Vector2f();
                hpGreen.Offset = new Vector2f();
                HpBarRed.FillColor -= new Color(0, 0, 0, 255);
                HpBarGreen.FillColor -= new Color(0, 0, 0, 255);
            }


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
            
            hpGreen.Enable = yes;
            hpRed.Enable = yes;
        }
    }

}
