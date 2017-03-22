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
    class CharecterSpawnComponent : OptionFoundation
    {

        static CharecterSpawnComponent()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();
            hexagon.FillColor = Color.White.SetAlpha(100);
            hexagon.OutlineThickness = 1;
            DrawComponent.Regiser("PlaceHexagon", hexagon);
        }
        Func<int, int, TileEntity> createDel;
        float reange;
        public CharecterSpawnComponent(float reange, Func<int, int, TileEntity> createDel) : base(new DrawComponent("PlaceHexagon", Layer.UNIT_BASE - 1))
        {
            UiName = "Spawn";
            this.createDel = createDel;
            this.reange = (Hexagon.HEX_R * Hexagon.HEX_R) * (10 * reange);
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            foreach (Tile item in map.Children)
            {
                if (item.Entity == null && (Position - item.Position).Pow2().Length() < reange)
                    list.Add(new Vector2i(item.X, item.Y));
            }
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            TileEntity e = createDel(resiver.X, resiver.Y);
            e.Owner = executer.Owner;
            TileMap map = resiver.Parent as TileMap;
            map.AddTileEntity(e);
        }
    }
}
