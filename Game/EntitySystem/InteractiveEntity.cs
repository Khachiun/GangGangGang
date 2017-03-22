using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public class InteractiveEntity : Entity
    {
        public CollitionComponent Collider { get; protected set; }
        public int Priority { get; set; } = 0;

        /// <summary>
        /// Use this contructor only if you know all recuiermants needed
        /// </summary>
        public InteractiveEntity()
        {

        }
        public InteractiveEntity(CollitionComponent collitionComponent, int priority = 0)
        {
            Collider = collitionComponent;
            Adopt(collitionComponent);
            this.Priority = priority;
        }
        public virtual void Click(bool yes) { }
        public virtual void Hover(bool yes) { }
    }
}
