using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public class DrawComponent : Entity
    {
        public string ID { get; protected set; }
        public int Layer { get; protected set; }

        public DrawComponent(string ID, int layer)
        {
            this.ID = ID;
            this.Layer = layer;
        }
        public DrawComponent Clone()
        {
            return new DrawComponent(ID, Layer);
        }
    }
}
