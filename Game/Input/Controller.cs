using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public class Controller
    {
        static Vector2i[] Diractions { get; }
        static Controller()
        {
            Diractions = new Vector2i[] {
                new Vector2i(-1,-1),
                new Vector2i( 0,-1),
                new Vector2i( 1, 0),
                new Vector2i( 1, 1),
                new Vector2i( 0, 1),
                new Vector2i(-1, 0)

            };
        }

        public uint ID;
        public int[] values = new int[(int)Butten.BUTTEN_COUNT];
        public float angel;

        public Vector2i GridDiraction;
        public Vector2f LeftStick;
        public Vector2f RigthStick;

        public float BumperValue { get; private set; }

        private bool[] LasteIteration = new bool[(int)Butten.BUTTEN_COUNT];
        private bool[] EventSate = new bool[(int)Butten.BUTTEN_COUNT];
        private Vector2f dpad = new Vector2f();




        private Controller(uint iD)
        {
            this.ID = iD;
        }

        public int this[int buttenID]
        {
            get { return values[buttenID]; }
            set { values[buttenID] = value; }
        }
        public int this[Butten butten]
        {
            get { return values[(uint)butten]; }
            set { values[(uint)butten] = value; }

        }

        #region Static

        public static Dictionary<uint, Controller> controllers = new Dictionary<uint, Controller>();

        public static void Update()
        {
            foreach (var controller in controllers.Values)
            {
                for (int i = 0; i < (int)Butten.BUTTEN_COUNT; i++)
                {
                    controller.values[i] = 0;
                    if (controller.LasteIteration[i])
                    {
                        controller.values[i] += -1;
                    }
                    if (controller.EventSate[i])
                    {
                        controller.values[i] += 2;
                        controller.LasteIteration[i] = true;
                    }
                    else
                    {
                        controller.LasteIteration[i] = false;
                    }
                }
                controller.angel = (float)(Math.Atan2(-controllers[0].LeftStick.Y, (-controllers[0].LeftStick.X)) * (180 / Math.PI) + 180);
                controller.GridDiraction = Controller.Diractions[(int)(controller.angel / 60)];
            }


            foreach (Controller controller in controllers.Values)
            {

            }


            //debug
            //Game.Out += $"left : {controllers[0].LeftStick.Length()}, {controllers[0].LeftStick} " + "\n";
            //Game.Out += $"right : {controllers[0].RigthStick.Length()}, { controllers[0].RigthStick}" + "\n";
            //Game.Out += $"DPad : {controllers[0].dpad.Length()}, { controllers[0].dpad}" + "\n";

            //Game.Out += $"DPad = Up : {controllers[0][Butten.DPAD_UP]}, Down : {controllers[0][Butten.DEPAD_DOWN]}, Left : {controllers[0][Butten.DPAD_LEFT]}, Right : {controllers[0][Butten.DPAD_RIGHT]} \n";

            //Game.Out += $"left = L : {Math.Round(controllers[0].LeftStick.Length(), 1)}, X : {Math.Round(controllers[0].LeftStick.X, 1)}, Y : {Math.Round(controllers[0].LeftStick.Y, 1)} " + "\n";
            //Game.Out += $"right = L : {Math.Round(controllers[0].RigthStick.Length(), 1)}, X : {Math.Round(controllers[0].RigthStick.X, 1)}, Y : {Math.Round(controllers[0].RigthStick.Y, 1)} " + "\n";


            ////controllers[0].angel *=  (float)(180.0 / Math.PI);
            ////controllers[0].angel += 180;
            //Game.Out += $" Angle : {controllers[0].angel} \n";
            //Game.Out += $" dir : {(ways)(int)(controllers[0].angel / 60)} \n";

            foreach (var controller in controllers.Values)
            {

                for (int i = 0; i < (int)Butten.BUTTEN_COUNT; i++)
                {
                    if (controller.values[i] > 0)
                    {
                        Console.WriteLine($"{controller.ID}: butten {i}");
                    }
                }
            }
        }
        //enum ways
        //{
        //    DownRight,
        //    Down,
        //    DownLeft,
        //    UpRight,
        //    Up,
        //    UpLeft
        //}
        public static void Start(Window window)
        {
            window.JoystickConnected += Window_JoystickConnected;
            window.JoystickDisconnected += Window_JoystickDisconnected;
            window.JoystickButtonPressed += Window_JoystickButtonPressed;
            window.JoystickButtonReleased += Window_JoystickButtonReleased;
            window.JoystickMoved += Window_JoystickMoved;

            for (uint i = 0; i < Joystick.Count; i++)
            {
                controllers.Add(i, new Controller(i));
            }
            //window.SetJoystickThreshold(0);
        }

        private static void Window_JoystickMoved(object sender, JoystickMoveEventArgs e)
        {


            switch (e.Axis)
            {
                case Joystick.Axis.X:
                    controllers[e.JoystickId].LeftStick.X = e.Position / 100;
                    //controllers[e.JoystickId].LeftStick.X /= 2;
                    break;
                case Joystick.Axis.Y:
                    controllers[e.JoystickId].LeftStick.Y = e.Position / 100;
                    //controllers[e.JoystickId].LeftStick.Y /= 2;

                    break;
                case Joystick.Axis.Z:
                    controllers[e.JoystickId].BumperValue = e.Position / 2;
                    break;
                case Joystick.Axis.R: // Y
                    controllers[e.JoystickId].RigthStick.Y = e.Position / 100;
                    break;
                case Joystick.Axis.U: // x
                    controllers[e.JoystickId].RigthStick.X = e.Position / 100;
                    break;
                case Joystick.Axis.V:

                    break;
                case Joystick.Axis.PovX:
                    controllers[e.JoystickId].dpad.X = e.Position / 100;

                    controllers[e.JoystickId].EventSate[(int)Butten.DPAD_LEFT] = controllers[e.JoystickId].dpad.X == -1;

                    controllers[e.JoystickId].EventSate[(int)Butten.DPAD_RIGHT] = controllers[e.JoystickId].dpad.X == 1;

                    break;
                case Joystick.Axis.PovY:
                    controllers[e.JoystickId].dpad.Y = e.Position / 100;

                    controllers[e.JoystickId].EventSate[(int)Butten.DPAD_UP] = controllers[e.JoystickId].dpad.Y == 1;

                    controllers[e.JoystickId].EventSate[(int)Butten.DEPAD_DOWN] = controllers[e.JoystickId].dpad.Y == -1;

                    break;
                default:
                    break;
            }
        }

        //public static event EventHandler<ControllerConnectEventArgs> ControllerConnectEvent;

        private static void Window_JoystickButtonReleased(object sender, JoystickButtonEventArgs e)
        {
            controllers[e.JoystickId].EventSate[e.Button] = false;
        }
        private static void Window_JoystickButtonPressed(object sender, JoystickButtonEventArgs e)
        {
            controllers[e.JoystickId].EventSate[e.Button] = true;
            Console.WriteLine($"{e.JoystickId}: {e.Button}");


            if (e.Button == 6)
            {
                Console.Clear();
            }
        }
        private static void Window_JoystickConnected(object sender, JoystickConnectEventArgs e) // Create Event dublicats, for static part of klass
        {
            //Console.WriteLine(e.JoystickId);
            //Controller controller = new Controller(e.JoystickId);
            //controllers.Add(e.JoystickId, controller);
            //ControllerConnectEvent?.Invoke(null, new ControllerConnectEventArgs(true, controller));
        }
        private static void Window_JoystickDisconnected(object sender, JoystickConnectEventArgs e)
        {
            //ControllerConnectEvent?.Invoke(null, new ControllerConnectEventArgs(true, controllers[e.JoystickId]));
            //controllers.Remove(e.JoystickId);
        }

        #endregion

    }
    public class ControllerConnectEventArgs : EventArgs
    {
        public ControllerConnectEventArgs(bool connected, Controller controller)
        {
            this.connected = connected;
            this.controller = controller;
        }

        public bool connected { get; private set; }
        public Controller controller { get; private set; }
    }
}
