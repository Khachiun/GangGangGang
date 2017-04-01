﻿using Czaplicki.SFMLE;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class Worker : TileEntity
    {
        static Worker()
        {
            for (int i = 0; i < Game.playerCount + 1; i++)
            {
                RectangleShape shape = new RectangleShape(new Square() * Hexagon.HEX_SIZE * 2, new Texture("Content/Assets/Textures/Ponn.png"));
                shape.Color = Player.colors[i];
                DrawComponent.Regiser("Worker" + (i), shape);
            }

        }
        public Worker(int x, int y, Player owner) : base(x, y, new CircleCollition(Hexagon.HEX_R), owner)
        {
            int colorID = Owner?.ID + 1 ?? 0;
            DrawComponent draw = new DrawComponent("Worker" + colorID , Layer.UNIT_BASE);
            draw.Offset += -Hexagon.OFFSET_TO_CENTER;
            Adopt(draw);

            Heath = 10;
            MaxHealth = 10;
            Regen = 1;

            //Option o = new Option();
            //o.UiName = "option";
            //Adopt(o);

            WorkComponent work = new WorkComponent();
            Adopt(work);

            AttackComponent attack = new AttackComponent(5, 3);
            Adopt(attack);


            SpawnTileEntityComponent spawn = new SpawnTileEntityComponent(2, (dx, dy) => new Building(dx, dy, Owner));
            Adopt(spawn);

            MoveComponent move = new MoveComponent();
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


}
