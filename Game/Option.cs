using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    delegate void MyDeleagate(TileEntity executer, Tile resiver);

    class MyEntity : Entity
    {
        public bool Show { set { draw.Enable = value; } }
        public bool Activated { set { oneClick.Enable = value; } }
        private DrawComponent draw;
        public ExternalInteractiveEntity oneClick;
        public MyEntity(int x, int y, DrawComponent draw, MyDeleagate del, BaseOption parent)
        {
            Offset = Hexagon.TRANSLATE(x, y);
            this.draw = draw;
            Adopt(draw);

            TileEntity executer = parent.Parent as TileEntity;

            TileMap map = executer.Parent.Parent as TileMap;

            Tile resiver = map[x, y];

            oneClick = new ExternalInteractiveEntity(new CircleCollition(Hexagon.HEX_R, Hexagon.OFFSET_TO_CENTER));
            oneClick.ClickDel = (b) => { if (b) del(executer, resiver); oneClick.Parent.Parent.Reject(oneClick.Parent); };
            oneClick.Enable = false;
            oneClick.Priority = Priority.UI_BASE;
            Adopt(oneClick);
        }
    }

    abstract class Option : Entity
    {
        public abstract bool Display { set; }
        public virtual bool Avalible { get; set; } = true;
        public string UiName { get; set; } = "Default";
        //public List<Recuiermant> recuierments;



        public Option()
        {
            Display = false;
        }
        /// <summary>
        /// When butten is pressed in list
        /// </summary>
        public virtual void Activate()
        {

        }

        /// <summary>
        /// When a new list is created contating this option
        /// </summary>
        public virtual void Calculate() { }

        /// <summary>
        /// run if another option is choosen in the list
        /// </summary>
        public virtual void CleanUp()
        {

        }


    }
    abstract class BaseOption : Option
    {
        DrawComponent grafic;



        protected BaseOption(DrawComponent draw)
        {
            grafic = draw;
        }
        public override bool Display
        {
            set
            {
                List<MyEntity> list = new List<MyEntity>();
                FetchAll<MyEntity>(ref list);
                foreach (MyEntity item in list)
                {
                    item.Show = value;
                }
            }
        }
        public override void Calculate()
        {
            TileMap map = Parent.Parent.Parent as TileMap;
            TileEntity parent = Parent as TileEntity;
            foreach (var item in GetAvalibleSpots(map, parent))
            {
                DrawComponent draw = grafic.Clone();
                //draw.Offset = Hexagon.OFFSET_TO_CENTER;
                MyEntity e = new MyEntity(item.X, item.Y, draw, OnSelectedClick, this);
                e.Offset -= Position;
                Adopt(e);
            }
        }
        public override void Activate()
        {
            Display = true;
            List<MyEntity> list = new List<MyEntity>();
            FetchAll<MyEntity>(ref list);
            foreach (MyEntity item in list)
            {
                item.oneClick.Enable = true;
            }
        }
        public override void CleanUp()
        {
            foreach (Entity child in FetchChildren<MyEntity>())
            {
                Reject(child);
            }
        }
        protected abstract void OnSelectedClick(TileEntity executer, Tile resiver);
        protected abstract List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent);
    }

    class MyMoveComponent : BaseOption
    {
        static MyMoveComponent()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();
            hexagon.FillColor = Color.Blue.SetAlpha(100);
            hexagon.OutlineThickness = 1;
            DrawManager.Register.Add("MoveHexagon", hexagon);

            pattern = new List<Vector2i>() {
                new Vector2i(-1, -1),
                new Vector2i(0, -1),
                new Vector2i(1, 0),
                new Vector2i(1, 1),
                new Vector2i(0, 1),
                new Vector2i(-1, 0)

            };
        }
        static List<Vector2i> pattern;
        public MyMoveComponent() : base(new DrawComponent("MoveHexagon", Layer.UNIT_BASE - 1))
        {
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            foreach (var item in pattern)
            {
                Tile t = Map[item.X + parent.X, item.Y + parent.Y];
                if (t != null && t.Entity == null)
                {
                    list.Add(new Vector2i(item.X + parent.X, item.Y + parent.Y));
                }
            }
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            TileMap map = resiver.Parent as TileMap;
            map.MoveEntity(executer, new Vector2i(resiver.X, resiver.Y));
        }
    }

    class MyWorkComponent : BaseOption
    {
        static MyWorkComponent()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();
            hexagon.FillColor = Color.Green.SetAlpha(100);
            hexagon.OutlineThickness = 1;
            DrawManager.Register.Add("MyWorkHexagon", hexagon);
        }
        public MyWorkComponent() : base(new DrawComponent("MyWorkHexagon", Layer.UNIT_BASE - 1))
        {
            UiName = "MyWork";
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            foreach (Tile item in Map.Children)
            {
                if (item.Entity is Resource)
                    list.Add(new Vector2i(item.X, item.Y));
            }
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            Resource r = resiver.Entity as Resource;
            r.SetOwner(executer);
        }
    }


}
