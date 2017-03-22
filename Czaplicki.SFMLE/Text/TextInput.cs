using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE.TextInput
{
    public class TextInput
    {
        protected uint CurserPostion = 0;
        protected string Text = "";
        private Window window;

        public TextInput(Window window)
        {
            window.TextEntered += Window_TextEntered;
            window.KeyPressed += Window_KeyPressed;
            this.window = window;
        }

        protected virtual void Window_KeyPressed(object sender, KeyEventArgs e)
        {

            if (e.Code == Keyboard.Key.Left && CurserPostion > 0)
                CurserPostion -= 1;

            if (e.Code == Keyboard.Key.Right && CurserPostion < Text.Length)
                CurserPostion += 1;
            
            if (e.Code == Keyboard.Key.Delete && CurserPostion < Text.Length)
                Text = Text.Remove((int)CurserPostion, 1);


        }

        protected virtual void Window_TextEntered(object sender, TextEventArgs e)
        {

            if (e.Unicode == "\b")
            {
                if (CurserPostion > 0)
                {
                    CurserPostion -= 1;
                    Text = Text.Remove((int)CurserPostion, 1);
                }
            }
            else if (e.Unicode == "\r")
            {
                return;
            }
            else
            {
                Text = Text.Insert((int)CurserPostion, e.Unicode);
                CurserPostion += 1;
            }
        }

        public void Clear()
        {
            Text = string.Empty;
            CurserPostion = 0;
        }

        ~TextInput()
        {
            window.TextEntered -= Window_TextEntered;
            window.KeyPressed -= Window_KeyPressed;
        }
    }
}
