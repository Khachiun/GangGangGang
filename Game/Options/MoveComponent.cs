using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GangGang
{
    class MoveComponent : OptionFoundation
    {
        static MoveComponent()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();
            hexagon.FillColor = Color.Blue.SetAlpha(100);
            hexagon.OutlineThickness = 1;
            DrawComponent.Regiser("MoveHexagon", hexagon);
        }
        public MoveComponent() : base(new DrawComponent("MoveHexagon", Layer.UNIT_BASE - 1))
        {
            UiName = "Move";
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            Map.GetSuroundingPositions(
                new Vector2i(parent.X, parent.Y),
                4,
                (e) => e == null,
                ref list);
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            TileMap map = resiver.Parent as TileMap;
            map.MoveEntity(executer, new Vector2i(resiver.X, resiver.Y));
            executer.Move(new Vector2i(resiver.X, resiver.Y));
        }
    }
}
