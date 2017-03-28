using Czaplicki.SFMLE;
using Czaplicki.SFMLE.Extentions;
using Czaplicki.Universal.Input;
using Czaplicki.Universal.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace GangGang
{

    class Program
    {
        public static Vector2f Offset = new Vector2f();
        static bool exit;
        static void Main(string[] args)
        {


            //defaultText Andvänds för enkel text utskriving
            DefaultText.Load("Content/Assets/Fonts/arial.ttf", 32, Color.Cyan);

            //ContexStettings för insällningar på försters rendering
            ContextSettings cs = new ContextSettings();
            cs.AntialiasingLevel = 30;
            cs.StencilBits = 0;

            //skapar fönstret
            CRenderWindow window = new CRenderWindow(new SFML.Window.VideoMode(1280, 720), "", SFML.Window.Styles.Default, cs);
            //Update to set Values ta all variblesas
            window.DispatchEvents();
            //Sätter framerate limiten till 60, som får update loopen att även gå i 60
            window.SetFramerateLimit(60);

            //Controller support
            Controller.Start(window);

            //sejer på att ändra på boolens värde nä man klickat på krysset på fönstret
            window.Closed += Exit;

            //skapar en instans av spelet
            Game game = new Game();
            //skapar två "view"s. där den ena är för att rendera värden och den andra till UI
            View ui_view = window.DefaultView;
            View game_view = new View((Vector2f)window.Size / 2, (Vector2f)window.Size);

            //gets the UI Mouse
            window.SetView(ui_view);
            Input.UIMouse = window.MapPixelToCoords(window.MouseState.position);

            //länkar spelet camera variabel till våran värds "View"
            game.Camera = game_view;
            //Sejer att fönstert ska börja andvända sig av "world view"n
            window.SetView(game_view);

            Input.WorldMouse = window.MapPixelToCoords(window.MouseState.position);

            //setting input defaults
            Input.KeyBoard = window.KeyboardState;
            Input.MouseState = window.MouseState;

            //kör spelets initilze funktion
            game.Init(window,2);


            Color backgroundColor = new Color(70, 70, 70);


            //startar main loppen
            while (!exit)
            {
                Game.Out = "";

                //hanterar allt med fönstret
                window.BasicDispatchEvents();
                //controller update
                Controller.Update();
                //updateing keyboard State
                Input.KeyBoard = window.KeyboardState;
                Input.MouseState = window.MouseState;
                //Updateing UI mouse postion
                Input.UIMouse = window.MapPixelToCoords(window.MouseState.position);
                //sätter "view"n till world view:n
                window.SetView(game_view);
                //Updateing World mouse postion
                Input.WorldMouse = window.MapPixelToCoords(window.MouseState.position);
                //updaterar spelet
                game.Update(window);
                //clear:ar fönstret
                window.Clear(backgroundColor);
                //skriver ut allt i värden
                game.Draw(window);



                dedu(window, game);

                //sätter "view"n till UI view:n
                window.SetView(ui_view);
                //skriver ut UI:n
                game.UI_Draw(window);
                //Display:ar allt på skärmen
                window.Display();

            }
        }

        public static void Exit()
        {
            exit = true;
        }
        public static void dedu(RenderTarget target, Entity e)
        {
            DefaultText.Size = 10;
            int sizeX = 15;
            int sizeY = 100;
            List<Vertex> verts = new List<Vertex>();

            verts.AddRange((new Square(sizeX, sizeY)).ToVertexArrayLine());
            DefaultText.Display(target, e.GetType().ToString().Substring("GangGang.".Length), new RenderArgs().Translate( new Vector2f(13, 0) + Offset));
            
            int x = 1, y = 0;
            List<Tuple<int, int, Entity>> list = new List<Tuple<int, int, Entity>>();
            Test(ref list, e, ref x, ref y);

            foreach (var item in list)
            {
                Vector2f pos = new Vector2f(item.Item1 * (sizeX + 10), item.Item2 * (sizeY + 10) + item.Item1 * 0);
                Color c = Color.White;
                string text = item.Item3.GetType().ToString().Substring("GangGang.".Length);

                if (item.Item3 is Game)
                    c = new Color(255, 200, 200);
                if (item.Item3 is DrawComponent)
                    c = Color.Red;
                if (item.Item3 is InteractiveEntity)
                    c = new Color(200, 0, 255);
                if (item.Item3 is Tile)
                    c = new Color(50, 255, 50);
                if (item.Item3 is CollitionComponent)
                    c = new Color(230, 80, 0);
                if (item.Item3 is Option)
                    c = Color.Blue;




                Vertex[] v = (new Square(sizeX, sizeY) + pos).ToVertexArrayLine(c);
                verts.AddRange(v);
                DefaultText.Display(target, text, new RenderArgs().Translate(pos + new Vector2f(13, 0) + Offset));

            }

            target.Draw(verts.ToArray(), PrimitiveType.Lines, new RenderArgs().Translate(new Vector2f(0, 0) + Offset));

            DefaultText.Size = 32;




        }

        public static void Test(ref List<Tuple<int, int, Entity>> list, Entity e, ref int x, ref int y)
        {

            if (e.Children != null)
            {
                x--;
                y++;
                foreach (Entity child in e.Children)
                {
                    //if (child is Tile)
                    {
                      //  if ((child as Tile).Entity != null)
                        {
                        //    list.Add(new Tuple<int, int, Entity>(x, y, child));
                          //  x++;
                          //  Test(ref list, child, ref x, ref y);

                        }
                    }
                    //else
                    {
                        list.Add(new Tuple<int, int, Entity>(x, y, child));
                        x++;
                        Test(ref list, child, ref x, ref y);
                    }
                    
                }
                y--;
                
            }

        }
        public static void drawChildern(RenderTarget target, Entity e, ref int x, ref int y)
        {
            int size = 70;
            List<Vertex> verts = new List<Vertex>();
            foreach (var item in e.Children)
            {
                Square s = new Square() * size + new Vector2f(x * (size + 10), y * (size + 10));
                DefaultText.Display(target, item.GetType().ToString().Substring("GangGang.".Length), new RenderArgs().Translate(s.Position));
                verts.AddRange(s.ToVertexArrayLine());
                x++;

            }
            target.Draw(verts.ToArray(), PrimitiveType.Lines, RenderStates.Default);
        }
    }
}
