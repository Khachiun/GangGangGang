﻿using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GangGang
{

    class WorkComponent : OptionFoundation
    {
        static WorkComponent()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();
            hexagon.FillColor = Color.Green.SetAlpha(100);
            hexagon.OutlineThickness = 1;
            DrawManager.Register.Add("MyWorkHexagon", hexagon);
        }
        public WorkComponent() : base(new DrawComponent("MyWorkHexagon", Layer.UNIT_BASE - 1))
        {
            UiName = "MyWork";
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            foreach (Tile item in Map.Children)
            {
                if (item.Entity is Resource)
                    list.Add(new Vector2i(item.X, item.Y));
            }
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            Resource r = resiver.Entity as Resource;
            r.Interacte(executer);
        }
    }

}