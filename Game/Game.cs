using System;
using System.Collections.Generic;
using Czaplicki.SFMLE;
using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using Czaplicki.Universal.Input;
using Czaplicki.Universal.Chain;

namespace GangGang
{
    class Player : Chain<Player>
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int Cristals { get; set; }
    }


    class Resource : TileEntity
    {

        static Resource()
        {
            Square rect = new Square(Hexagon.HEX_SIZE * 1, Hexagon.HEX_SIZE * 2);

            RectangelShape shape = new RectangelShape(rect, new Texture("Content/Assets/Textures/Concept_Kristal.png"));
            DrawManager.Register.Add("Cristal", shape);

        }

        int ueses = 3;
        int useCount = 0;
        int amount = 10;

        private TileEntity entity;

        public Resource(int x, int y) : base(x, y, new CircleCollition(Hexagon.HEX_R))
        {
            Game.NextTurnEvent += Game_NextTurnEvent;
            DrawComponent draw = new DrawComponent("Cristal", Layer.UNIT_BASE);
            draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            Adopt(draw);
        }

        private void Game_NextTurnEvent()
        {
            if (useCount >= ueses)
            {
                TileMap map = Parent.Parent as TileMap;
                map.RemoveEntity(this);
                map.AddTileEntity(entity);
            }
            if (entity != null && entity.Owner != null)
            {
                entity.Owner.Cristals += amount;
                useCount++;
            }

        }

        public void SetOwner(TileEntity entity)
        {
            this.entity = entity;
            TileMap map = Parent.Parent as TileMap;
            map.RemoveEntity(entity);
        }

    }


    class Worker : TileEntity
    {
        static List<Vector2i> movementPattern;
        static Worker()
        {
            RectangelShape shape = new RectangelShape(new Square() * Hexagon.HEX_SIZE * 2, new Texture("Content/Assets/Textures/Ponn.png"));
            DrawManager.Register.Add("Worker", shape);

            movementPattern = new List<Vector2i>() {
                new Vector2i(-1,-1),
                new Vector2i( 0,-1),
                new Vector2i( 1, 0),
                new Vector2i( 1, 1),
                new Vector2i( 0, 1),
                new Vector2i(-1, 0)

            };

        }
        public Worker(int x, int y) : base(x, y, new CircleCollition(Hexagon.HEX_R))
        {
            DrawComponent draw = new DrawComponent("Worker", Layer.UNIT_BASE);
            draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            Adopt(draw);

            //Option o = new Option();
            //o.UiName = "option";
            //Adopt(o);

            MyWorkComponent work = new MyWorkComponent();
            Adopt(work);


            MyMoveComponent move = new MyMoveComponent();
            Adopt(move);

            //MoveComponent move = new MoveComponent(movementPattern);
            //Adopt(move);


            //WorkComponent work = new WorkComponent();
            //Adopt(work);
        }

        public override void Click(bool yes)
        {
            base.Click(yes);
        }
        public override void Hover(bool yes)
        {

        }
    }



    class Caret : Entity
    {

        static Caret()
        {
            ConvexShape shape = Hexagon.GenHexagon();
            shape.FillColor = Color.Yellow.SetAlpha(150);
            DrawManager.Register.Add("Caret", shape);
        }

        private Vector2i gridPos = new Vector2i();

        private DrawComponent drawComponent;

        public Caret()
        {
            drawComponent = new DrawComponent("Caret", Layer.UNIT_BASE - 1);
            Adopt(drawComponent);

        }
        public override void Update()
        {
            #region InteractiveEntitys

            Game.Out += gridPos + "\n";

            List<InteractivEntity> entitys = new List<InteractivEntity>();
            Parent.FetchAllActive<InteractivEntity>(ref entitys);
            entitys.Sort((left, right) => right.Priority - left.Priority);


            bool clickedThisIteration = false;
            bool clicked;
            bool collided;
            foreach (var entity in entitys)
            {
                Game.Out += $"Type : {entity.GetType()}, Parent : {entity.Parent?.ToString() ?? "null"}\n";

                if (Game.UseController)
                {
                    clicked = Input.Controller[Butten.A] == 2;
                    collided = entity.Collider.Collide(Hexagon.TRANSLATE(gridPos) + Hexagon.OFFSET_TO_CENTER);

                }
                else
                {
                    clicked = Input.MouseState.Left == 2;
                    collided = entity.Collider.Collide(Input.WorldMouse);
                }

                entity.Hover(collided);
                if (clicked)
                {
                    bool click = collided && !clickedThisIteration;
                    entity.Click(click);
                    Console.WriteLine($"GETS CLICKED = Type : {entity.GetType()}, Parent : {entity.Parent?.ToString() ?? "null"} ");
                    if (click)
                    {
                        clickedThisIteration = true;
                    }
                }
            }

            #endregion

            #region Mouse Movement and Visualization
            if (Game.UseController)
            {
                if (Input.Controller[Butten.B] == 2 || Input.Controller[Butten.LEFT_STICK] == 2) // chache to timer thining
                {

                    gridPos += -Input.Diraction;
                    drawComponent.Offset += Hexagon.TRANSLATE(gridPos.X, gridPos.Y) - drawComponent.Position;
                    //drawComponent.Offset += drawComponent.Position - Hexagon.TRANSLATE(Input.Diraction.X, Input.Diraction.Y);

                }
            }
            else
            {
                List<CollitionComponent> tileCols = new List<CollitionComponent>();
                Parent.FetchAllActive<CollitionComponent>(ref tileCols, "Tile");
                foreach (CollitionComponent item in tileCols)
                {
                    if (item.Collide(Input.WorldMouse))
                    {
                        drawComponent.Offset += item.Parent.Position - drawComponent.Position;
                        Tile tile = item.Parent as Tile;
                        gridPos = new Vector2i(tile.X, tile.Y);
                        break;
                    }
                }
            }
            #endregion

            base.Update();
        }

    }


    class Game : Entity
    {
        public static bool UseController { get; set; }
        public static Player CurrrentPlayer { get; set; }
        public static string Out = "";

        public static Random randome = new Random();

        public static event Action NextTurnEvent;

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

            Resource r = new Resource(9, 2);
            tilemap.AddTileEntity(r);

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
            DrawManager.Render(window, this);

            if (debugEnabled)
            {

                List<RectangelCollition> recs = new List<RectangelCollition>();
                FetchAllActive<RectangelCollition>(ref recs);
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

            NextTurnEvent?.Invoke();

        }
    }


    class List : InteractivEntity
    {
        static RectangelShape backgrund;
        static Square buttenSize;
        static List()
        {
            buttenSize = new Square(200, 50);
            backgrund = new RectangelShape(new Square(200, 70), new Color(60, 60, 60, 100).ToTexture());
            DrawManager.Register.Add("bg", backgrund);
            RectangelShape shape = new RectangelShape(buttenSize, new Color(80, 80, 80, 100).ToTexture());
            DrawManager.Register.Add("btn", shape);

            RectangelShape selectShape = new RectangelShape(buttenSize, new Color(150, 150, 255, 150).ToTexture());
            DrawManager.Register.Add("btnSelect", selectShape);


        }

        List<Option> options;
        CollitionComponent[] buttens;
        DrawComponent selectingGrafik;

        int _selected;
        int selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                selectingGrafik.Offset = new Vector2f(10, (buttenSize.Size.Y + 10) * _selected + 10);
            }
        }

        public List(List<Option> options)
        {
            this.Priority = GangGang.Priority.UI_BASE;

            this.options = options;
            Adopt(new DrawComponent("bg", Layer.UI_BASE));

            selectingGrafik = new DrawComponent("btnSelect", Layer.UI_BASE + 3);
            selected = 0;
            options[selected].Display = true;
            Adopt(selectingGrafik);

            Square backgruond = new Square(0, 0, 220, (buttenSize.Size.Y + 10) * options.Count + 10);
            backgrund.Rectangel = backgruond;

            RectangelCollition col = new RectangelCollition(backgruond, new Vector2f());
            Adopt(col);
            Collider = col;


            buttens = new CollitionComponent[options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Calculate();
                CollitionComponent butten = new RectangelCollition(buttenSize, new Vector2f());
                butten.Adopt(new DrawComponent("btn", Layer.UI_BASE + 1));
                butten.Adopt(new TextComponent(options[i].UiName, Layer.UI_BASE + 2));
                butten.Offset = new Vector2f(10, (buttenSize.Size.Y + 10) * i + 10);
                buttens[i] = butten;
                Adopt(butten);
            }


        }
        public override void Update()
        {
            if (Game.UseController)
            {
                if (Input.Controller[Butten.DPAD_UP] == 2)
                {
                    options[selected].Display = false;
                    selected--;
                    options[selected].Display = true;

                }
                if (Input.Controller[Butten.DEPAD_DOWN] == 2)
                {
                    options[selected].Display = false;
                    selected++;
                    options[selected].Display = true;

                }
            }

            base.Update();
        }

        public override void Click(bool yes)
        {
            if (yes)
            {
                if (Game.UseController)
                {
                    //options[selected].Activate();
                    //Parent.Reject(this);
                    ////options[selected].Display = false;

                    for (int i = 0; i < options.Count; i++)
                    {
                        if (i == selected)
                        {
                            options[i].Activate();
                            Parent.Reject(this);
                            //options[selected].Display = false;

                        }
                        else
                        {
                            options[i].CleanUp();
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < options.Count; i++)
                    {
                        if (buttens[i].Collide(Input.WorldMouse))
                        {
                            options[i].Activate();
                            Parent.Reject(this);
                            //options[selected].Display = false;

                        }
                        else
                        {
                            options[i].CleanUp();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    options[i].CleanUp();
                }
                Parent.Reject(this);
                options[selected].Display = false;

            }
        }

        public override void Hover(bool yes)
        {
            if (!Game.UseController)
            {

                for (int i = 0; i < options.Count; i++)
                {
                    if (buttens[i].Collide(Input.WorldMouse))
                    {
                        options[i].Display = true;
                        selected = i;
                    }
                    else
                    {
                        options[i].Display = false;
                    }
                }
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