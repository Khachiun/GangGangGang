using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Czaplicki.SFMLE.Game
{
    public abstract class AbstractGame
    {


        public View WorldView;
        public View GuiView;
        public RenderWindow Window;
        public RenderWindow WorldRendrer
        {
            get
            {
                Window.SetView(WorldView);
                return Window;
            }
                    
        }
        public RenderWindow GuiRendrer
        {
            get
            {
                Window.SetView(GuiView);
                return Window;
            }

        }
        
        
        abstract public void Initialize();
        abstract public void Update(CKeyboard Keyboard, CMouse Mouse);
        abstract public void RenderWorld(RenderWindow window, RenderStates rs);
        abstract public void RenderUI(RenderWindow window, RenderStates rs);

    }
}
