using Czaplicki.Universal.Input;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE.Input
{
    //class CInput
    //{
    //    public static CKeyBoard KeyBoard { get; private set; }
    //    public static CMouse Mouse { get; private set; }
    //    public static void Initilze()
    //    {
    //        KeyBoard = new CKeyBoard();
    //        Mouse = new CMouse();
    //    }
    //    public static void Update(float deltaTime)
    //    {

    //    }
        

    //    public class CKeyBoard : CInput
    //    {
    //        public int[] values { get; private set; }

    //        public bool IsMask { get; private set; }
    //        public void Mask() { IsMask = !IsMask; }
    //        public void Mask(bool value) { IsMask = value; }

    //        private List<int> lastIteration;

    //        public int this[Key index]
    //        {
    //            get { return values[(int)index]; }
    //            set { values[(int)index] = value; }
    //        }
    //        private void Tick()
    //        {
    //            IsMask = false;
    //            values = new int[(int)Key.KeyCount];
    //            List<int> temp = new List<int>();

    //            for (int item = 0; item < (int)Key.KeyCount; item++)
    //            {
    //                if (lastIteration.Contains(item))
    //                    values[item] -= 1;
    //                if (Keyboard.IsKeyPressed((Keyboard.Key)item))
    //                {
    //                    values[item] += 2;
    //                    temp.Add(item);
    //                }
    //            }
    //            lastIteration = temp;

    //            //wtf SFML ?!?!?! Why is '0' allways down?!?!?                          //BUG!!!!
    //            values[75] = 0;
    //        }
    //    }
    //    public class CMouse
    //    {

    //    }
    //}

}
