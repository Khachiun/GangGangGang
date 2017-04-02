using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using Czaplicki.SFMLE.Extentions;
using Czaplicki.SFMLE;

namespace GangGang
{
    abstract class Building : TileEntity
    {
        public Building(int x, int y, CollitionComponent collition, Player owner = null) : base(x, y, collition, owner)
        {

        }
        public Building(int x, int y, Player owner = null) : base(x, y, owner)
        {

        }
    }
}
