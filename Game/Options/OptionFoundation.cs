using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public abstract class OptionFoundation : Option
    {
        private DrawComponent grafic;

        protected OptionFoundation(DrawComponent draw)
        {
            grafic = draw;
        }
        public override bool Display
        {
            set
            {
                List<OptionObject> list = new List<OptionObject>();
                FetchAll<OptionObject>(ref list);
                foreach (OptionObject item in list)
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
                OptionObject e = new OptionObject(item.X, item.Y, draw, OnSelectedClick, this);
                e.Offset -= Position;
                Adopt(e);
            }
        }
        public override void Activate()
        {
            Display = true;
            List<OptionObject> list = new List<OptionObject>();
            FetchAll<OptionObject>(ref list);
            foreach (OptionObject item in list)
            {
                item.oneClick.Enable = true;
            }
        }
        public override void CleanUp()
        {
            foreach (Entity child in FetchChildren<OptionObject>())
            {
                Reject(child);
            }
        }
        protected abstract void OnSelectedClick(TileEntity executer, Tile resiver);
        protected abstract List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent);
    }
}
