﻿using System;
using System.Collections.Generic;
using Czaplicki.SFMLE;
using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using Czaplicki.Universal.Input;
using Czaplicki.Universal.Chain;

namespace GangGang
{
    public class Player : Chain<Player>
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int Cristals { get; set; }
    }

    public class Game : Entity
    {
        public static bool UseController { get; set; }
        public static Player CurrrentPlayer { get; set; }
        public static string Out = "";

        public static Random randome = new Random();
        
        public View Camera { get; set; }

        Player players;

        private CircleShape shape;
        private bool debugEnabled = false;

        private int turnCount;

        public void Init(CRenderWindow window)
        {

            players = new Player();
            players.InsertElement(new Player());
            players.ID = 0;
            players.Cristals = 100;
            players.GetNext().ID = 1;
            CurrrentPlayer = players;



            Camera.Center += new Vector2f(-100, -300);

            Caret caret = new Caret();
            Adopt(caret);

            TileMap tilemap = new TileMap(new Vector2i(10, 10));
            Adopt(tilemap);

            Worker worker = new Worker(2, 1);
            worker.Owner = players;
            tilemap.AddTileEntity(worker);

            Worker worker2 = new Worker(16, 8);
            worker2.Owner = players.GetNext();
            tilemap.AddTileEntity(worker2);

            BasicCristal r = new BasicCristal(9, 2);
            tilemap.AddTileEntity(r);

            Building b = new Building(9, 7);
            tilemap.AddTileEntity(b);

            shape = new CircleShape();
            shape.FillColor = new Color(0, 0, 0, 0);
            shape.OutlineThickness = 1;
            shape.OutlineColor = Color.Red;


        }

        public void Update(CRenderWindow window)
        {

            MouseState ms = window.MouseState;
            KeyboardState kbs = window.KeyboardState;
            Vector2f worldMouse = window.MapPixelToCoords(ms.position);

            if (Input.Controller[Butten.LEFT_TRIGGER] > 0)
            {
                Vector2f pvelocity = Input.Controller.RigthStick.Normalize();
                float pspeed = Math.Min(1, Input.Controller.RigthStick.Length());
                if (pspeed < 0.2f)
                {
                    pspeed = 0;

                }
                else
                {
                    pspeed *= 10;
                    
                }
                if (Input.Controller[Butten.RIGHT_STICK] > 0)
                {
                    pspeed *= 3;
                }
                Program.Offset += pvelocity * pspeed;
            }


            Vector2f velocity = new Vector2f();
            float speed;

            if (UseController)
            {
                if ((Input.Controller[Butten.A] &
                     Input.Controller[Butten.X] &
                     Input.Controller[Butten.RIGHT_TRIGGER] &
                     Input.Controller[Butten.LEFT_TRIGGER]) > 0 &&
                     Input.Controller[Butten.LEFT_STICK] == 2)
                {
                    Program.Exit();
                }
                if (Input.Controller[Butten.RIGHT_TRIGGER] == 2)
                {
                    NextTurn();
                    Console.Clear();
                    Console.WriteLine("next turn");
                }


                velocity = Input.Controller.RigthStick.Normalize();
                speed = Math.Min(1, Input.Controller.RigthStick.Length());
                if (speed < 0.2f)
                {
                    speed = 0;

                }
                else
                {
                    speed *= 10;
                    if (Input.Controller[Butten.RIGHT_STICK] > 0)
                    {
                        speed *= 3;
                    }
                }
            }
            else
            {
                speed = kbs[Key.LShift] > 0 ? 10 : 4;
                if (kbs[Key.A] > 0)
                    velocity += new Vector2f(-1, 0);
                if (kbs[Key.D] > 0)
                    velocity += new Vector2f(1, 0);
                if (kbs[Key.W] > 0)
                    velocity += new Vector2f(0, -1);
                if (kbs[Key.S] > 0)
                    velocity += new Vector2f(0, 1);

            }
            Camera.Center += velocity * speed;


            if (Input.KeyBoard[Key.Escape] == 2)
            {
                Program.Exit();
            }
            if (Input.KeyBoard[Key.F11] == 2 || Input.Controller[Butten.BACK] == 2)
            {
                Game.UseController = !Game.UseController;
            }

            if (kbs[Key.F12] == 2 || Input.Controller[Butten.START] == 2)
            {
                debugEnabled = !debugEnabled;
            }

            base.Update();
        }


        public void Draw(CRenderWindow window)
        {
            //DrawManager.Render(window, this);
            DrawComponent.Manager.Render(window, this);

            if (debugEnabled)
            {

                List<RectangleCollition> recs = new List<RectangleCollition>();
                FetchAllActive<RectangleCollition>(ref recs);
                List<Vertex> verts = new List<Vertex>();
                foreach (var item in recs)
                {
                    verts.AddRange(item.testArea.ToVertexArrayLine(Color.Green));
                }
                window.Draw(verts.ToArray(), PrimitiveType.Lines, RenderStates.Default);

                List<CircleCollition> cir = new List<CircleCollition>();
                FetchAllActive<CircleCollition>(ref cir);
                List<Drawable> shapes = new List<Drawable>();


                foreach (CircleCollition item in cir)
                {
                    shape.Radius = (float)Math.Sqrt(item.radiePowTwo);
                    shape.Position = item.Position - new Vector2f(shape.Radius, shape.Radius);
                    window.Draw(shape);
                }
            }
        }
        public void UI_Draw(CRenderWindow window)
        {
            DefaultText.Display(window, CurrrentPlayer.ID, RenderStates.Default);
            DefaultText.Display(window, CurrrentPlayer.Cristals, new RenderArgs().Translate(new Vector2f(0, 70)));
            if (debugEnabled)
                DefaultText.Display(window, Out, RenderStates.Default);
        }
        private void NextTurn()
        {
            turnCount++;
            players = players.GetNext();
            CurrrentPlayer = players;

            List<TileEntity> list = new List<TileEntity>();
            FetchAllActive<TileEntity>(ref list);
            foreach (var item in list)
            {
                item.NextTurn();
            }
        }
    }


    


    
}
    //abstract class OneClick : InteractivEntity
    //{
    //    protected abstract void OnClick();

    //    public override void Click(bool yes)
    //    {
    //        if (yes)
    //        {
    //            OnClick();
    //        }
    //        this.Parent.Reject(this);
    //    }
    //}

    //class ExternalOneClick : OneClick
    //{
    //    Action Del { get; set; }
    //    public ExternalOneClick(Action del)
    //    {
    //        this.Del = del;
    //    }
    //    protected override void OnClick()
    //    {
    //        Del?.Invoke();
    //    }
    //}
    

    

    //class Recuiermant
    //{
    //    DrawComponent Icon;
    //    int Ammount;
    //}
    
    //class MoveComponent : Option
    //{
    //    static MoveComponent()
    //    {
    //        ConvexShape hexagon = Hexagon.GenHexagon();
    //        hexagon.FillColor = Color.Magenta.SetAlpha(100);
    //        hexagon.OutlineThickness = 1;
    //        DrawManager.Register.Add("MoveHexagon", hexagon);
    //    }
    //    private List<Vector2i> pattern = new List<Vector2i>();
    //    public MoveComponent(List<Vector2i> pattern)
    //    {
    //        this.UiName = "Move";
    //        this.pattern = pattern;
    //        //CalculateDrawComponents(parent);
    //    }
    //    public override bool Display
    //    {
    //        set
    //        {
    //            //base.Display = value;

    //            List<ExtenalAction> list = new List<ExtenalAction>();
    //            FetchAll<ExtenalAction>(ref list, "MoveComp");
    //            foreach (var item in list)
    //            {
    //                item.Enable = value;
    //            }
    //        }
    //    }
    //    public override void Calculate()
    //    {
    //        //this.Parent.Parent.Parent
    //        //Option.TileEntity.Tile.TileMap

    //        TileMap map = Parent.Parent.Parent as TileMap;

    //        TileEntity tileEntity = Parent as TileEntity;


    //        foreach (Vector2i spot in pattern)
    //        {
    //            if (true)
    //            {
    //                ExtenalAction entity = new ExtenalAction(spot.X, spot.Y, new DrawComponent("MoveHexagon", Layer.UNIT_BASE - 1), "MoveComp");
    //                entity.ClickDel = () =>
    //                {
    //                    Vector2i gridpos = new Vector2i(tileEntity.X + spot.X, tileEntity.Y + spot.Y);
    //                    //  map.RemoveEntity(tileEntity);
    //                    map.MoveEntity(tileEntity, gridpos);
    //                    Game.CurrrentPlayer.Cristals -= 10;
    //                };
    //                Adopt(entity);
    //            }
    //        }
    //    }
    //    public override void Activate()
    //    {
    //        //          base.Activate();

    //        List<ExtenalAction> list = new List<ExtenalAction>();
    //        FetchAll<ExtenalAction>(ref list);
    //        foreach (var item in list)
    //        {
    //            //item.Parent.Enable = true;
    //            item.exint.Enable = true;
    //        }
    //    }

    //}

    //abstract class ActionOptionComponent : Option// do komplete compy of movecomp , with abstract onClick
    //{

    //    DrawComponent visual;
    //    public ActionOptionComponent(string name, DrawComponent visual)
    //    {
    //        UiName = name;
    //        this.visual = visual;
    //        visual.Enable = false;
    //    }
    //    protected abstract List<Vector2i> GetAvlaibleSpots();
    //    // protected abstract bool IsSpotAvalible();
    //    protected abstract void WorldInteraction();
    //    public override void Calculate()
    //    {
    //        //TileMap map = Parent.Parent.Parent as TileMap;

    //        foreach (var spot in GetAvlaibleSpots())
    //        {
    //            //TileEntity e = Parent as TileEntity;
    //            ExtenalAction exint = new ExtenalAction(spot.X, spot.Y, visual.Clone(), UiName/*change to better tag later*/);
    //            exint.ClickDel = WorldInteraction;
    //            Adopt(exint);
    //        }
    //    }
    //    public override void Activate()
    //    {
    //        //          base.Activate();

    //        List<ExtenalAction> list = new List<ExtenalAction>();
    //        FetchAll<ExtenalAction>(ref list);
    //        foreach (var item in list)
    //        {
    //            //item.Parent.Enable = true;
    //            item.exint.Enable = true;
    //        }
    //    }
    //    public override bool Display
    //    {
    //        set
    //        {
    //            //base.Display = value;

    //            List<ExtenalAction> list = new List<ExtenalAction>();
    //            FetchAll<ExtenalAction>(ref list, UiName/*change to better tag later*/);
    //            foreach (var item in list)
    //            {
    //                item.Enable = value;
    //            }
    //        }
    //    }

    //}
    //class WorkComponent : ActionOptionComponent
    //{
    //    static WorkComponent()
    //    {
    //        ConvexShape hexagon = Hexagon.GenHexagon();
    //        hexagon.FillColor = Color.Green.SetAlpha(100);
    //        hexagon.OutlineThickness = 1;
    //        DrawManager.Register.Add("WorkHexagon", hexagon);
    //    }

    //    public WorkComponent()
    //            : base("Work", new DrawComponent("WorkHexagon", Layer.UNIT_BASE - 1))
    //    {

    //    }

    //    protected override List<Vector2i> GetAvlaibleSpots()
    //    {
    //        //return null;
    //        //          option.tileEntity.Tile.Tilmap
    //        TileMap map = this.Parent.Parent.Parent as TileMap;
    //        Tile tileStart = Parent.Parent as Tile;
    //        List<Vector2i> pattern = new List<Vector2i>();

    //        foreach (Tile tile in tileStart)
    //        {
    //            //if (tile.Entity is Resource)
    //            {
    //                pattern.Add(new Vector2i(-tile.X, tile.Y));
    //            }
    //        }

    //        return pattern;
    //    }

    //    //protected override bool IsSpotAvalible()
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    protected override void WorldInteraction()
    //    {
    //        Console.WriteLine("LMOA XD LOL 1337 keysyNiceAssOK");
    //    }
    //}
    ////class WorkComponent : Option
    ////{
    ////    static WorkComponent()
    ////    {
    ////        ConvexShape hexagon = Hexagon.GenHexagon();
    ////        hexagon.FillColor = Color.Green.SetAlpha(100);
    ////        hexagon.OutlineThickness = 1;
    ////        DrawManager.Register.Add("WorkHexagon", hexagon);
    ////    }
    ////    public WorkComponent()
    ////    {

    ////    }
    ////    public override void Calculate()
    ////    {
    ////        Tile firstTile = Parent.Parent as Tile;
    ////        foreach (Tile tile in firstTile)
    ////        {
    ////            if (tile.Entity is Resourse)
    ////            {

    ////            }
    ////        }
    ////    }

    ////}
    //class ExtenalAction : Entity //generic obect to be yoused for a bidding. "UI" elemet to do external acting on click /hover
    //{

    //    public Action ClickDel { get; set; }

    //    public ExternalInteractiveEntity exint;

    //    public ExtenalAction(int x, int y, DrawComponent drawComponent, string tag)
    //    {
    //        SetTag(tag);
    //        Adopt(drawComponent);
    //        Offset = Hexagon.TRANSLATE(x, y);
    //        exint = new ExternalInteractiveEntity(new CircleCollition(Hexagon.HEX_R, Hexagon.OFFSET_TO_CENTER), GangGang.Priority.UI_BASE);
    //        Adopt(exint);
    //        exint.Enable = false;

    //        exint.ClickDel = Click;
    //    }
    //    private void Click(bool yes)
    //    {
    //        if (yes)
    //        {
    //            ClickDel?.Invoke();
    //        }
    //        Parent.Reject(this);

    //    }
    //}
//abstract class MyOption : Option
//{
//    DrawComponent grafic;



//    protected MyOption(DrawComponent draw)
//    {
//        grafic = draw;
//    }
//    public override bool Display
//    {
//        set
//        {
//            List<MyEntity> list = new List<MyEntity>();
//            FetchAll<MyEntity>(ref list);
//            foreach (MyEntity item in list)
//            {
//                item.Show = value;
//            }
//        }
//    }
//    public override void Calculate()
//    {
//        TileMap map = Parent.Parent.Parent as TileMap;
//        TileEntity parent = Parent as TileEntity;
//        foreach (var item in GetAvalibleSpots(map, parent))
//        {
//            DrawComponent draw = grafic.Clone();
//            //draw.Offset = Hexagon.OFFSET_TO_CENTER;
//            MyEntity e = new MyEntity(item.X, item.Y, draw, OnSelectedClick, this);
//            e.Offset -= Position;
//            Adopt(e);
//        }
//    }
//    public override void Activate()
//    {
//        Display = true;
//        List<MyEntity> list = new List<MyEntity>();
//        FetchAll<MyEntity>(ref list);
//        foreach (MyEntity item in list)
//        {
//            item.oneClick.Enable = true;
//        }
//    }
//    public override void CleanUp()
//    {
//        foreach (Entity child in FetchChildren<MyEntity>())
//        {
//            Reject(child);
//        }
//    }
//    protected abstract void OnSelectedClick(TileEntity executer, Tile resiver);
//    protected abstract List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent);
//}