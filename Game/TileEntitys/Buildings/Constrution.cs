using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class Contrution : TileEntity
    {
        TextComponent text;
        int buildTime;
        int state;
        Func<int, int, Player, TileEntity> create;
        public Contrution(Func<int, int, Player, TileEntity> create, int buildTime, int x, int y, Player owner = null) : base(x, y, owner)
        {
            this.create = create;
            this.buildTime = buildTime;
            text = new TextComponent(0 + "/" + buildTime, Layer.UI_BASE);
            Adopt(text);

        }
        public override void OnNewTurn()
        {
            text.Text = state + "/" + buildTime;
            state++;

            if (state > buildTime)
            {
                TileMap map = Parent.Parent as TileMap;
                map.RemoveEntity(this);
                map.AddTileEntity(create(X, Y, Owner));
            }
        }
    }

}
