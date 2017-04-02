using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class Capital : Building
    {
        public Capital(int x, int y, Player owner) : base(x, y, owner)
        {
            Adopt(new TextComponent("Capital\n" + Owner.ID, Layer.UI_BASE));
        }
    }

}
