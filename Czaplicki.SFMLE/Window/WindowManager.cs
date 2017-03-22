using Czaplicki.Universal.Extentions;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE
{
    public static class WindowManager
    {

        private static CRenderWindow[] _windows;
        public static CRenderWindow[] windows => _windows;

        public static void Initilze()
        {
            _windows = new CRenderWindow[1];
        }
        public static int CreateWindow(uint width, uint height, string title, Styles style)
        {
            VideoMode mode = new VideoMode(width, height);
            CRenderWindow rw = new CRenderWindow(mode, title, style);
            int index;
            _windows.DynamicAdd(rw, out index);
            //rw.Closed += (s, e) => Program.Exit();
            return index;
        }

        public static void Run(ref bool isRunning)
        {
            while (isRunning)
            {
                Thread.Sleep(1);
                foreach (var window in _windows)
                {
                    window?.DispatchEvents();
                    window?.Display();
                    window?.Clear(new Color(40, 40, 40, 255));
                }
            }
        }
    }
}
