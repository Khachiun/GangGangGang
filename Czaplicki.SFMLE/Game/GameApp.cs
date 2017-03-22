using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Czaplicki.SFMLE.Game
{

    public class GameApp : IApp
    {
        AbstractGame game;

        public GameApp(AbstractGame Game)
        {
            this.game = Game;
        }

        public void Subscribe(ApplicationHandler applicationManager)
        {
            applicationManager.InitializeEvent += game.Initialize;

            var rw = applicationManager.WindowManager.Window;
            game.WorldView = rw.DefaultView;
            game.GuiView = rw.DefaultView;
            game.Window = rw;
        }

        public void Update(CKeyboard Keyboard, CMouse Mouse)
        {
            game.Update(Keyboard, Mouse);
        }

        public void Render(RenderWindow window)
        {
            RenderStates rs = RenderStates.Default;

            game.RenderWorld(game.WorldRendrer, rs);


            rs = RenderStates.Default;
            game.RenderUI(game.GuiRendrer, rs);

        }

        public void Close(ApplicationHandler applicationManager)
        {

        }
    }
}
