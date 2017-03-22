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

            pattern = new List<Vector2i>() {
                new Vector2i(-1, -1),
                new Vector2i(0, -1),
                new Vector2i(1, 0),
                new Vector2i(1, 1),
                new Vector2i(0, 1),
                new Vector2i(-1, 0)

            };
        }
        static List<Vector2i> pattern;
        public MoveComponent() : base(new DrawComponent("MoveHexagon", Layer.UNIT_BASE - 1))
        {
            UiName = "Move";
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap Map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            foreach (var item in pattern)
            {
                Tile t = Map[item.X + parent.X, item.Y + parent.Y];
                if (t != null && t.Entity == null)
                {
                    list.Add(new Vector2i(item.X + parent.X, item.Y + parent.Y));
                }
            }
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            TileMap map = resiver.Parent as TileMap;
            map.MoveEntity(executer, new Vector2i(resiver.X, resiver.Y));
        }
    }
}
