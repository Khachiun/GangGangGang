using Czaplicki.SFMLE;
using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    public class InWorldList : InteractiveEntity
    {
        static RectangleShape backgrund;
        static Square buttenSize;
        static InWorldList()
        {
            buttenSize = new Square(200, 50);
            backgrund = new RectangleShape(new Square(200, 70), new Color(60, 60, 60, 100).ToTexture());
            DrawComponent.Regiser("bg", backgrund);
            RectangleShape shape = new RectangleShape(buttenSize, new Color(80, 80, 80, 100).ToTexture());
            DrawComponent.Regiser("btn", shape);

            RectangleShape selectShape = new RectangleShape(buttenSize, new Color(150, 150, 255, 150).ToTexture());
            DrawComponent.Regiser("btnSelect", selectShape);


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

        public InWorldList(List<Option> options)
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

            RectangleCollition col = new RectangleCollition(backgruond, new Vector2f());
            Adopt(col);
            Collider = col;


            buttens = new CollitionComponent[options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                options[i].Calculate();
                CollitionComponent butten = new RectangleCollition(buttenSize, new Vector2f());
                butten.Adopt(new DrawComponent("btn", Layer.UI_BASE + 1));
                butten.Adopt(new TextComponent(options[i].UiName +" C: " + options[i].cristalConsumtion, Layer.UI_BASE + 2));
                

                butten.Offset = new Vector2f(10, (buttenSize.Size.Y + 10) * i + 10);
                buttens[i] = butten;
                Adopt(butten);
            }

            options[selected].Display = true;

        }
        public override void Update()
        {
            if (Game.UseController)
            {
                if (Input.Controller[Butten.DPAD_UP] == 2)
                {
                    options[selected].Display = false;
                    selected--;

                    if (selected <= -1)
                    {
                        selected = options.Count - 1;
                    }

                    options[selected].Display = true;

                }
                if (Input.Controller[Butten.DEPAD_DOWN] == 2)
                {
                    options[selected].Display = false;
                    selected++;

                    if (selected >= options.Count)
                    {
                        selected = 0;
                    }

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
