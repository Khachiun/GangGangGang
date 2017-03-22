using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public class ExternalInteractiveEntity : InteractiveEntity
    {
        private Action<bool> click;
        private Action<bool> hover;

        public Action<bool> ClickDel
        {
            get
            {
                return click;
            }

            set
            {
                this.click = value;
            }
        }

        public Action<bool> HoverDel
        {
            get
            {
                return hover;
            }

            set
            {
                this.hover = value;
            }
        }
        public ExternalInteractiveEntity(CollitionComponent collitionComponent, int priority = 0) : base(collitionComponent, priority)
        {

        }
        public ExternalInteractiveEntity(Action<bool> click, Action<bool> hover, CollitionComponent collitionComponent, int priority = 0) : base(collitionComponent, priority)
        {
            this.click = click;
            this.hover = hover;
        }
        public override void Hover(bool yes)
        {
            hover?.Invoke(yes);
        }
        public override void Click(bool yes)
        {
            click?.Invoke(yes);
        }
    }
}
