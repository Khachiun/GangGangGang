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
        private int maxUsePerTrun;
        private int usedThisTurn = 0;

        protected OptionFoundation(DrawComponent draw, int usePerTurn = 1)
        {
            grafic = draw;
            maxUsePerTrun = usedThisTurn;
        }
        public override void OnNewTurn()
        {
            this.Enable = true;
            usedThisTurn = 0;
            base.OnNewTurn();
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
        /// <summary>
        /// When menue is opend
        /// </summary>
        public override void Calculate()
        {
            TileMap map = Parent.Parent.Parent as TileMap;
            TileEntity parent = Parent as TileEntity;



            foreach (var item in GetAvalibleSpots(map, parent))
            {
                DrawComponent draw = grafic.Clone();
                //draw.Offset = Hexagon.OFFSET_TO_CENTER;

                OptionObjectDelegate del = (m, l) =>
               {
                   usedThisTurn++;
                   List<Option> list = new List<Option>();
                   parent.FetchAllActive<Option>(ref list);

                   foreach (var option in list)
                   {
                       option.Enable = false;
                   }
               };

                OptionObject e = new OptionObject(item.X, item.Y, draw, OnSelectedClick + del , this);
                e.Offset -= Position;
                e.Show = false;
                Adopt(e);
            }
            
        }
        /// <summary>
        /// when butten is chossen in the menu
        /// </summary>
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
            List<OptionObject> list = new List<OptionObject>();
            FetchChildren<OptionObject>(ref list);
            foreach (Entity child in list)
            {
                Reject(child);
            }
        }
        protected abstract void OnSelectedClick(TileEntity executer, Tile resiver);
        protected abstract List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent);
    }
}
