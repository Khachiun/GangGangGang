using Czaplicki.SFMLE;
using Czaplicki.SFMLE.Extentions;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    static class Input
    {
        public static Vector2f WorldMouse;
        public static Vector2f UIMouse;
        public static MouseState MouseState;
        public static KeyboardState KeyBoard;

        public static float Angle => Controller.angel;
        public static Vector2i Diraction => Controller.GridDiraction;
        public static bool UseController { get; set; }
        public static Controller Controller { get; private set; }

        static Input()
        {
            Controller = Controller.controllers[0];
        }

    }
}
