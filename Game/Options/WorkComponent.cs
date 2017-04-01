using Czaplicki.SFMLE.Extentions;
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
            DrawComponent.Regiser("WorkHexagon", hexagon);
        }
        public WorkComponent() : base(new DrawComponent("WorkHexagon", Layer.UNIT_BASE - 1))
        {
            UiName = "Work";
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            Map.GetSuroundingPositions(new Vector2i(parent.X, parent.Y), 2, (e) => e is ResourceBase, ref list);
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            ResourceBase r = resiver.Entity as ResourceBase;
            r.Interacte(executer);
        }
    }

}
