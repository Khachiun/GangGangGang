using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE.Game
{
    public class GameMouse
    {
        
        private CMouse mouse;
        private Camera camera;
        public Vector2f Position { get { return (Vector2f)mouse.Position - camera.position; } }
        public GameMouse(CMouse m, Camera c)
        {
            mouse = m;
            camera = c;
        }
    }
}
