using Czaplicki.SFMLE;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultText.Load("arial.ttf", 32, Color.White);
            Program program = new Program();
            CRenderWindow window = new CRenderWindow(new VideoMode(1020, 720), "Editor", Styles.Default);
            window.UpdateEvent += program.Window_UpdateEvent;
            window.DrawEvent += program.Window_DrawEvent;
            bool exit = false;
            window.Closed += () => exit = true;
            while (!exit)
            {
                window.DispatchEvents();
            }
        }

        private void Window_DrawEvent(CRenderWindow obj)
        {
            obj.
        }

        private void Window_UpdateEvent(CRenderWindow obj)
        {
            
        }
    }
}
