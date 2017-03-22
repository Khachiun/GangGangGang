using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using System.Diagnostics;
using SFML.System;
using SFML.Window;
using Czaplicki.Universal;
using Czaplicki.Universal.Console;
using System.Threading;

namespace Czaplicki.SFMLE
{
    public interface IApp
    {
        void Subscribe(ApplicationHandler applicationManager);
        void Update(CKeyboard Keyboard, CMouse Mouse);
        void Render(RenderWindow window);
        void Close(ApplicationHandler applicationManager);

    }

    public class ApplicationHandler
    {
        //Input
        private CKeyboard Keyboard { get; set; }
        private Action keyboardUpdate;
        private CMouse Mouse { get; set; }
        private Action mouseUpdate;

        //window
        public WindowManager WindowManager { get; set; } = new WindowManager();

        //Active App info
        public string ActiveAppID { get; private set; }
        public Action<CKeyboard, CMouse> UpdateActive;
        public Action<RenderWindow> Render;


        #region Events
        public event Action InitializeEvent;
        public event Action LoadContentEvent;

        public event Action<CKeyboard, CMouse> UpdateEvent;
        public event Action<RenderWindow> RenderOverlayEvent;

        public event Action<ApplicationHandler> PreNewActiveAppEvent;
        public event Action<ApplicationHandler> PostNewActiveAppEvent;

        //public event Action<ApplicationHandler> CloseEvent;

        #endregion
        private bool Running;

        Dictionary<string, IApp> BindedApps = new Dictionary<string, IApp>();

        private int FrameRate = 1000 / 60;
        //private int FrameRate = 1000 / 100;

        public ApplicationHandler()
        {
            #region Console Commands
            //CConsole.ExecuteEvent += (args) =>
            //    {
            //        if (args[0] == "sw" && args.Length > 1)
            //        {
            //            if (BindedApps.ContainsKey(args[1]))
            //                SetActive(args[1]);
            //            else
            //            {
            //                //return "Invalid APP ID";
            //            }
            //        }

            //        if (args[0].ToUpper() == "EXIT")
            //        {
            //            Running = false;
            //            CloseEvent?.Invoke(this);
            //        }

            //        if (args.Length > 1)
            //        {
            //            if (args[0].ToUpper() == "CLOSEAPP")
            //            {
            //                if (BindedApps.ContainsKey(args[1]))
            //                {
            //                    if (ActiveAppID == args[1])
            //                    {
            //                        BindedApps[args[1]].Close(this);
            //                        BindedApps.Remove(args[1]);
            //                        if (BindedApps.Keys.Count > 0)
            //                            SetActive(BindedApps.Keys.First());
            //                        else
            //                            CConsole.Execute("EXIT");
            //                        CConsole.Logg("AppHandler", args[1] + " is Closed");
            //                        //return args[1] + " is Closed";
            //                    }
            //                    else
            //                    {
            //                        BindedApps[args[1]].Close(this);
            //                        BindedApps.Remove(args[1]);
            //                        CConsole.Logg("AppHandler", args[1] + " is Closed");
            //                        //return args[1] + " is Closed";
            //                    }
            //                }
            //            }
            //        }
            //        if (args[0].ToUpper() == "GBA")
            //        {
            //            string s = string.Empty;
            //            foreach (string key in BindedApps.Keys)
            //            {
            //                s += key + ", ";
            //            }
            //            CConsole.Logg("AppHandler", s);
            //        }
            //        //return null;
            //    };// Gets all binded app ids // GBA


            #endregion
        }

        public void Start(string StartUpApp)
        {
            Keyboard = new CKeyboard(out keyboardUpdate);
            Mouse = new CMouse(WindowManager.Window, out mouseUpdate); // FIX ADD NEW REFRENCE TO MOUSE WHEN WINDOW IS UPDATED
            LoadContentEvent?.Invoke();
            InitializeEvent?.Invoke();
            Running = true;
            Stopwatch timer = new Stopwatch();
            SetActive(StartUpApp);
            timer.Start();
            while (Running)
            {
                WindowManager.Window.DispatchEvents();


                //if (timer.ElapsedMilliseconds > FrameRate)
                //{
                //    keyboardUpdate();
                //    mouseUpdate();
                //    UpdateEvent(Keyboard, Mouse);
                //    UpdateActive(Keyboard, Mouse);
                //    timer.Restart();
                //}
                //WindowManager.Window.Clear(WindowManager.Color);
                //Render(WindowManager.Window);
                //RenderOverlayEvent(WindowManager.Window);
                //WindowManager.Window.Display();

                Thread.Sleep(1);
                if (timer.ElapsedMilliseconds > FrameRate)
                {
                    keyboardUpdate();
                    mouseUpdate();
                    UpdateEvent(Keyboard, Mouse);
                    UpdateActive(Keyboard, Mouse);

                    WindowManager.Window.Clear(WindowManager.Color);

                    Render(WindowManager.Window);
                    RenderOverlayEvent(WindowManager.Window);

                    WindowManager.Window.Display();

                    timer.Restart();
                }

            }
        }
        public void Bind(string appID, IApp app)
        {
            BindedApps.Add(appID, app);
            app.Subscribe(this);
        }

        [Command("setactive")]
        private void SetActive(string AppID)
        {
            PreNewActiveAppEvent?.Invoke(this);
            IApp app = BindedApps[AppID];
            UpdateActive = app.Update;
            Render = app.Render;
            ActiveAppID = AppID;
            PostNewActiveAppEvent?.Invoke(this);
        }
        [Command("exit")]
        private void Terminate()
        {
            this.Running = false;
        }
        [Command("appid")]
        private void GetAppName()
        {
            CConsole.Logg(ActiveAppID);
        }        

    }

    public class WindowManager
    {
        public RenderWindow Window => window;
        private RenderWindow window;
        public event Action<WindowManager> WindowUpdated;

        public Vector2u Resolution { get; set; } = new Vector2u(800, 600);
        public string Title { get; set; } = "BlankApp";
        public Styles Style { get; set; } = Styles.Close;
        public Color Color { get; set; } = Color.White;

        public WindowManager()
        {
            window = new RenderWindow(new VideoMode((uint)Resolution.X, (uint)Resolution.Y), Title, Style);
            window.Closed += Window_Closed;

            //  CConsole.ExecuteEvent += CConsole_ExecuteCommandEvent;
        }

        [Command("setres")]
        private void SetResulution(int width, int height)
        {
            Resolution = new Vector2u((uint)width, (uint)height); /// FORTSET HÄR
            CConsole.Logg("Resolution: " + Resolution);
        }
        [Command("fullscreen")]
        private void SetFullscreenMode(bool fullscreen)
        {
            Style = fullscreen ? Styles.Fullscreen : Styles.Close;
            CConsole.Logg(Style.ToString());
        }
        [Command("fullscreen")]
        private void ToggleFullscreenMode()
        {
            Style = Style != Styles.Fullscreen ? Styles.Fullscreen : Styles.Close;
            CConsole.Logg(Style.ToString());
        }
        [Command("settile")]
        private void SetTitle(string newTitle)
        {
            Title = newTitle;
            CConsole.Logg("Title: " + newTitle);
        }
        [Command("apply")]
        private void ApplyWindowChanges()
        {
            UpdateWindow(Resolution, Title, Style);
        }
        [Command("color=")]
        private void SetColor(int r, int g, int b)
        {
            Color = new Color((byte)r, (byte)g, (byte)b);
        }

        private void UpdateWindow(Vector2u res, string title, Styles style)
        {
            window.Close();
            window = new RenderWindow(new VideoMode(res.X, res.Y), title, style);
            window.Closed += Window_Closed;
            WindowUpdated(this);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            CConsole.Execute("exit");
            window.Close();
        }

    }

}
