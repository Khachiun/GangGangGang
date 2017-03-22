using Czaplicki.Universal.Application;
using Czaplicki.Universal.Input;
using Czaplicki.Universal.Interfaces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLEEEEEEe
{

    
    class CollitionMask
    {

        List<ICollitionCheck> objects = new List<ICollitionCheck>();

        //public bool Check(out int hit, out int type)
        //{
        //    var temp = new List<int>();
        //    for (int i = 0; i < objects.Count; i++)
        //    {

        //    }
        //    //hits = temp.ToArray();
        //    //return hits.Length > 0;
        //}
        public int NewSqaure()
        {
            return 0;
        }

        interface ICollitionCheck
        {
            bool Check(Vector2f mouse);
        }
        class Square : ICollitionCheck
        {
            public bool Check(Vector2f mouse)
            {
                throw new NotImplementedException();
            }
        }
        class Circle : ICollitionCheck
        {
            public bool Check(Vector2f mouse)
            {
                throw new NotImplementedException();
            }
        }
    }


    static class Input
    {
        private static Action ku;
        public static CKeyboard Keyboard { get; private set; } = new CKeyboard(out ku);


        public static void ControllerAttatchemnt()
        {
            ku();
        }

        public class CKeyboard
        {
            public CKeyboard()
            {


            }
            public int[] Values
            {
                get
                {
                    return MaskValues ? new int[(int)Key.KeyCount] : values;
                }

                set
                {
                    values = value;
                }
            }
            public bool MaskValues { get; set; }

            public int this[Key index]
            {
                get
                {
                    return MaskValues ? 0 : values[(int)index];
                }

                set
                {
                    values[(int)index] = value;
                }
            }
            public int this[int index]
            {
                get
                {
                    return MaskValues ? 0 : values[index];
                }

                set
                {
                    values[index] = value;
                }
            }

            public CKeyboard(out Action tick)
            {
                tick = Tick;
            }

            private int[] values;
            private List<int> list = new List<int>();

            private void Tick()
            {
                MaskValues = false;
                values = new int[(int)Key.KeyCount];
                List<int> temp = new List<int>();

                for (int item = 0; item < (int)Key.KeyCount; item++)
                {
                    if (list.Contains(item))
                        values[item] -= 1;
                    if (SFML.Window.Keyboard.IsKeyPressed((Keyboard.Key)item))
                    {
                        values[item] += 2;
                        temp.Add(item);
                    }
                }
                list = temp;

                //wtf SFML ?!?!?! Why is '0' allways down?!?!?                          //BUG!!!!
                values[75] = 0;
            }
        }

        public class CMouse : ICMouse
        {
            //Button Properties
            public int Left => values[0];
            public int Right => values[1];
            public int Middle => values[2];
            public int XB1 => values[3];
            public int XB2 => values[4];

            //position properies
            public int X => Position.X;
            public int Y => Position.Y;

            public Vector2i Position { get { return MaskPositon ? savedPosition : Mouse.GetPosition(window); } set { Mouse.SetPosition(value, window); } }

            

            //public propeties
            public bool MaskValues { get; set; }
            public int[] Values { get { return values; } set { values = value; } }

            public bool MaskPositon
            {
                get { return maskPosition; }
                set
                {
                    if ((maskPosition = value))
                        savedPosition = Position;
                }
            }

            public void UpdateWindowReference(RenderWindow window)
            {
                this.window = window;
            }

            //ctor
            public CMouse(RenderWindow window, out Action tick)
            {
                this.window = window;
                tick = () => Tick();
                values = new int[5];
                pdown = new bool[5];
            }

            //private values
            private bool maskPosition;
            private RenderWindow window;
            private Vector2i savedPosition;
            private int[] values;
            private bool[] pdown;

            //private Methodes
            private void Tick()
            {
                values = new int[5];

                for (int i = 0; i < 5; i++)
                {
                    if (pdown[i])
                        values[i] -= 1;
                    if (Mouse.IsButtonPressed((Mouse.Button)i))
                    {
                        values[i] += 2;
                        pdown[i] = true;
                    }
                    else pdown[i] = false;
                }
            }


        }
    }
}
