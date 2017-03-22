using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using Czaplicki.Universal.Input;
using SFML.System;

namespace Czaplicki.SFMLE
{
    public class CRenderWindow : RenderWindow
    {

        public Color ClearColor { get; set; } = Color.Black;

        public object[] Buffer { get; set; }

        //keyboard
        bool[] lastIteration = new bool[(int)Key.KeyCount];

        //mouse
        bool[] buttenDownLastIteration = new bool[5];
        public CRenderWindow(VideoMode mode, string title, Styles style, ContextSettings contextSettings, int bufferSize = 10) : base(mode, title, style, contextSettings)
        {
            Buffer = new object[bufferSize];
        }

        public CRenderWindow(VideoMode mode, string title, Styles style, int bufferSize = 10) : base(mode, title, style)
        {
            Buffer = new object[bufferSize];
        }
        public void BasicDispatchEvents()
        {
            base.DispatchEvents();
            //update keyboard

            int keyCount = (int)Key.KeyCount;
            int[] kdata = new int[keyCount];
            for (int item = 0; item < keyCount; item++)
            {
                if (lastIteration[item])
                    kdata[item] = -1;
                if (Keyboard.IsKeyPressed((Keyboard.Key)item))
                {
                    kdata[item] += 2;
                    lastIteration[item] = true;
                }
                else
                {
                    lastIteration[item] = false;
                }
            }



            KeyboardState = new KeyboardState(kdata);

            //update Mouse
            int[] mdata = new int[5];
            for (int i = 0; i < 5; i++)
            {
                if (buttenDownLastIteration[i])
                    mdata[i] = -1;
                if (Mouse.IsButtonPressed((Mouse.Button)i))
                {
                    mdata[i] += 2;
                    buttenDownLastIteration[i] = true;
                }
                else buttenDownLastIteration[i] = false;
            }
            MouseState = new MouseState() { values = mdata, position = Mouse.GetPosition(this) };


        }

        public void DispatchEvents(bool drawCall = true)
        {
            base.DispatchEvents();
            //update keyboard

            int keyCount = (int)Key.KeyCount;
            int[] kdata = new int[keyCount];
            for (int item = 0; item < keyCount; item++)
            {
                if (lastIteration[item])
                    kdata[item] = -1;
                if (Keyboard.IsKeyPressed((Keyboard.Key)item))
                {
                    kdata[item] += 2;
                    lastIteration[item] = true;
                }
                else
                {
                    lastIteration[item] = false;
                }
            }
            KeyboardState = new KeyboardState(kdata);

            //update Mouse
            int[] mdata = new int[5];
            for (int i = 0; i < 5; i++)
            {
                if (buttenDownLastIteration[i])
                    mdata[i] = -1;
                if (Mouse.IsButtonPressed((Mouse.Button)i))
                {
                    mdata[i] += 2;
                    buttenDownLastIteration[i] = true;
                }
                else buttenDownLastIteration[i] = false;
            }
            MouseState = new MouseState() { values = mdata, position = Mouse.GetPosition(this) };

            EarlyUpdateEvent?.Invoke(this);

            if (drawCall)
                this.Clear(ClearColor);

            UpdateEvent?.Invoke(this);
            EarlyDrawEvent?.Invoke(this);
            DrawEvent?.Invoke(this);

            if (drawCall)
                this.Display();

            LateDrawEvent?.Invoke(this);
            LateUpdateEvent?.Invoke(this);
        }
        public new void Display()
        {
            base.Display();
        }

        public KeyboardState KeyboardState { get; set; }
        public MouseState MouseState { get; set; }

        public event Action<CRenderWindow> EarlyDrawEvent;
        public event Action<CRenderWindow> DrawEvent;
        public event Action<CRenderWindow> LateDrawEvent;
        public event Action<CRenderWindow> EarlyUpdateEvent;
        public event Action<CRenderWindow> UpdateEvent;
        public event Action<CRenderWindow> LateUpdateEvent;

        public new event Action Closed { add { base.Closed += (s, e) => value(); } remove { base.Closed -= (s, e) => value(); } }

    }

    public struct KeyboardState
    {
        public int[] table;
        public KeyboardState(int[] table)
        {
            this.table = table;
        }
        public int this[Key index]
        {
            get { return table[(int)index]; }
            set { table[(int)index] = value; }
        }
    }
    public struct MouseState
    {


        //Button Properties
        public int Left => values[0];
        public int Right => values[1];
        public int Middle => values[2];
        public int XB1 => values[3];
        public int XB2 => values[4];

        //position properies
        public int X => position.X;
        public int Y => position.Y;


        public Vector2i position;


        public int[] values;
    }
}
