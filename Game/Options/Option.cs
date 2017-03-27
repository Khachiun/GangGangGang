using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System;

namespace GangGang
{


    public abstract class Option : Entity, IUseReadTurns
    {
        public class OptionObject : Entity
        {
            public bool Show { set { draw.Enable = value; } }
            public bool Activated { set { oneClick.Enable = value; } }
            private DrawComponent draw;
            public ExternalInteractiveEntity oneClick;
            public OptionObject(int x, int y, DrawComponent draw, OptionObjectDelegate del, OptionFoundation parent)
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

        public delegate void OptionObjectDelegate(TileEntity executer, Tile resiver);

        public abstract bool Display { set; }
        public virtual bool Avalible { get; set; } = true;
        public string UiName { get; set; } = "Default";

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

        public virtual void OnNewTurn()
        {

        }
    }
}
