using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    abstract class ResourceBase : TileEntity
    {
        public ResourceBase(int x, int y) : base(x, y, new CircleCollition(Hexagon.HEX_R)) { }
        public override void OnNewTurn() { }
        public virtual void Interacte(TileEntity entity) { }
    }
}
