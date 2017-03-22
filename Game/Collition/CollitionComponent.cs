using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public abstract class CollitionComponent : Entity
    {
        public abstract bool Collide(Vector2f point);
    }
}
