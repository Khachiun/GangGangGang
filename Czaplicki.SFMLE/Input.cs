using System;
using System.Collections.Generic;
using Czaplicki.Universal.Input;
using Czaplicki.Universal.Interfaces;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using Czaplicki.Universal.Console;
using System.Linq;

namespace Czaplicki.SFMLE
{

    //public class KeyBinds
    //{
    //    Dictionary<int, string> keybinds = new Dictionary<int, string>();

    //    int w = 6;

    //    //public void Update(CKeyboard keyboard)
    //    //{
    //    //    foreach (var key in keybinds.Keys)
    //    //    {
    //    //        var keysate = keyboard[key / w];

    //    //        var bindState = key % w;
    //    //        switch (bindState)
    //    //        {
    //    //            case 1: // -1
    //    //            case 2: // 0
    //    //            case 3: // 1
    //    //            case 4: // 2
    //    //                if (keysate + 2 == bindState)
    //    //                    CConsole.Execute(keybinds[key]);
    //    //                break;
    //    //            case 0:
    //    //                if (keysate <= 0)
    //    //                    CConsole.Execute(keybinds[key]);
    //    //                break;
    //    //            case 5:
    //    //                if ( keysate > 0)
    //    //                    CConsole.Execute(keybinds[key]);
    //    //                break;
    //    //        }
    //    //    }
    //    //}
    //    [Command("Bind")]
    //    private void Bind(int key, int state, string Command)
    //    {
    //        int fkey = key * w + state;
    //        if (keybinds.ContainsKey(fkey))
    //            keybinds[fkey] = Command;
    //        else
    //            keybinds.Add(fkey, Command);
    //    }
    //    public void Bind(Key key, KeyState state, string Command) => Bind((int)key, (int)state + 2, Command);
    //}



    //public class CKeyboard
    //{
    //    public int[] Values
    //    {
    //        get
    //        {
    //            return MaskValues ? new int[(int)Key.KeyCount] : values;
    //        }

    //        set
    //        {
    //            values = value;
    //        }
    //    }
    //    public bool MaskValues { get; set; }

    //    public int this[Key index]
    //    {
    //        get
    //        {
    //            return MaskValues ? 0 : values[(int)index];
    //        }

    //        set
    //        {
    //            values[(int)index] = value;
    //        }
    //    }
    //    public int this[int index]
    //    {
    //        get
    //        {
    //            return MaskValues ? 0 : values[index];
    //        }

    //        set
    //        {
    //            values[index] = value;
    //        }
    //    }

    //    public CKeyboard(out Action tick)
    //    {
    //        tick = Tick;
    //    }

    //    private int[] values;
    //    private List<int> list = new List<int>();

    //    private void Tick()
    //    {
    //        MaskValues = false;
    //        values = new int[(int)Key.KeyCount];
    //        List<int> temp = new List<int>();

    //        for (int item = 0; item < (int)Key.KeyCount; item++)
    //        {
    //            if (list.Contains(item))
    //                values[item] -= 1;
    //            if (Keyboard.IsKeyPressed((Keyboard.Key)item))
    //            {
    //                values[item] += 2;
    //                temp.Add(item);
    //            }
    //        }
    //        list = temp;

    //        //wtf SFML ?!?!?! Why is '0' allways down?!?!?                          //BUG!!!!
    //        values[75] = 0;
    //    }

    //}
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

        public Vector2i Position
        {
            get
            {
                return MaskPositon ? savedPosition : Mouse.GetPosition(window);
            }
            set
            {
                Mouse.SetPosition(value, window);
            }
        }


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
            tick = Tick;
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


    public sealed class ITT
    {
        private int[] DownTime = new int[(int)Key.KeyCount];

        private SingelModefierFilterString Filter_shift = new SingelModefierFilterString();
        private SingelModefierFilterString Filter_control = new SingelModefierFilterString();
        private SingelModefierFilterString Filter_alt = new SingelModefierFilterString();
        private SingelModefierFilterString Filter_altgr = new SingelModefierFilterString();

        private SingelModefierFilterString Filter_noModifier = new SingelModefierFilterString();

        public ITT()
        {
            #region Default / No Modifiers Fiter
            Filter_noModifier.add("Space", " ");

            Filter_noModifier.add("Equal", "+");
            Filter_noModifier.add("LeftBracket", "´");
            Filter_noModifier.add("RightBracket", "å");
            Filter_noModifier.add("SemiColon", "¨");
            Filter_noModifier.add("Tilde", "ö");
            Filter_noModifier.add("Quote", "ä");
            Filter_noModifier.add("Slash", "'");
            Filter_noModifier.add("Comma", ",");
            Filter_noModifier.add("Period", ".");
            Filter_noModifier.add("Dash", "-");

            Filter_noModifier.add("Tab", "");

            #endregion
            #region Shift Filter

            Filter_shift.add("Equal", "?");
            Filter_shift.add("LeftBracket", "`");
            Filter_shift.add("RightBracket", "Å");
            Filter_shift.add("SemiColon", "^");
            Filter_shift.add("Tilde", "Ö");
            Filter_shift.add("Quote", "Ä");
            Filter_shift.add("Slash", "*");
            Filter_shift.add("Comma", ";");
            Filter_shift.add("Period", ":");
            Filter_shift.add("Dash", "_");

            var keys = new string[10];
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = "Num" + i;
            }

            var repm = new string[]
            {
                "=","!",'"'.ToString(),"#","¤","%","&","/","(",")"
            };

            Filter_shift.add(keys, repm);
            #endregion
            #region altgr Filter

            Filter_altgr.add("Equal", '\\'.ToString());
            //Filter_altgr.add("LeftBracket", "");
            //Filter_altgr.add("RightBracket", "");
            Filter_altgr.add("SemiColon", "~");
            //Filter_altgr.add("Tilde", "");
            //Filter_altgr.add("Quote", "");
            //Filter_altgr.add("Slash", "");
            //Filter_altgr.add("Comma", "");
            //Filter_altgr.add("Period", "");
            //Filter_altgr.add("Dash", "");

            Filter_altgr.add("E", "€");


            List<string> keylist = new List<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                if (i == 1 || i == 6)
                    continue;
                keylist.Add("Num" + i);
            }
            keys = keylist.ToArray();

            repm = new string[]
            {
                "}","@","£","$","€","{","[","]"
            };

            Filter_altgr.add(keys, repm);

            #endregion
        }

        public string test(string Final, ref uint curser, int[] keydata)
        {
            #region Modifiers
            bool Down_Shift = false;
            bool Down_Alt = false;
            bool Down_Control = false;
            bool Down_Altgr = false;

            if (keydata[(int)Key.LShift] > 0 || keydata[(int)Key.RShift] > 0)
                Down_Shift = true;

            if (keydata[(int)Key.LAlt] > 0 || keydata[(int)Key.RAlt] > 0)
                Down_Alt = true;

            if (keydata[(int)Key.LControl] > 0 || keydata[(int)Key.RControl] > 0)
                Down_Control = true;

            if (Down_Alt && Down_Control)
            {
                Down_Altgr = true;
                Down_Alt = false;
                Down_Control = false;
            }
            #endregion

            string addition = "";

            for (int i = 0; i < keydata.Length; i++)
            {
                #region Update Downtime

                if (keydata[i] > 0)
                    DownTime[i]++;
                else
                    DownTime[i] = 0;

                #endregion
                #region Act on key?

                bool act = false;

                if (keydata[i] == 2)
                    act = true;

                if (DownTime[i] > 30 && DownTime[i] % 5 == 0)
                    act = true;

                #endregion

                if (act)
                {
                    #region modifiers +

                    Key[] kts = new Key[] { Key.LShift, Key.RShift, Key.LControl, Key.RControl, Key.LAlt, Key.RAlt, Key.BackSlash };
                    foreach (var item in kts)
                        if ((Key)i == item)
                            continue;

                    #endregion
                    #region Special

                    if ((Key)i == Key.Left && curser > 0)
                        curser--;
                    if ((Key)i == Key.Right && curser < Final.Length)
                        curser++;

                    if ((Key)i == Key.BackSpace && curser > 0)
                    {
                        Final = Final.Remove((int)curser - 1, 1);
                        curser--;
                        continue;
                    }
                    if ((Key)i == Key.Delete && curser < Final.Length)
                    {
                        Final = Final.Remove((int)curser, 1);
                        continue;
                    }
                    #endregion

                    var temp = ((Key)i).ToString();

                    if (Down_Shift)
                    {
                        string ss = "";
                        ss += Filter_shift[temp];

                        if (temp.Length == 1)
                            ss += temp;

                        addition += ss;
                        if (ss.Length > 0)
                            continue;
                    }
                    if (Down_Altgr)
                    {
                        string ss = "";
                        ss += Filter_altgr[temp];
                        addition += ss;
                        if (ss.Length > 0)
                            continue;
                    }

                    addition += Filter_noModifier[temp];

                    if (temp.Length == 1)
                        addition += temp.ToLower();

                    if (temp.Contains("Num"))
                        addition += temp.Substring(temp.Length - 1, 1);

                }
            }

            Final = Final.Insert((int)curser, addition);
            curser += (uint)addition.Length;
            return Final;
        }
    }

    public class SingelModefierFilterString
    {
        List<string> keys = new List<string>();
        List<string> values = new List<string>();

        public string this[string index]
        {
            get
            {
                for (int i = 0; i < keys.Count; i++)
                    if (keys[i].Equals(index))
                        return values[i];
                
                return "";
            }
        }
        public void add(string item, string replacement)
        {
            keys.Add(item);
            values.Add(replacement);
        }
        public void add(string[] item, string[] replacement)
        {
            foreach (var i in item)
                keys.Add(i);
            foreach (var i in replacement)
                values.Add(i);
        }
    }
}
