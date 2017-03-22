using Czaplicki.SFMLE.Extentions;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class CircleCollition : CollitionComponent
    {
        internal float radiePowTwo;

        public CircleCollition(float radie)
        {
            this.radiePowTwo = radie * radie;
        }
        public CircleCollition(float radie, Vector2f offset) : this(radie)
        {
            base.Offset = offset;
        }

        public override bool Collide(Vector2f point)
        {
            return (Position - point).Pow2().Length() < radiePowTwo;
        }

    }
}
