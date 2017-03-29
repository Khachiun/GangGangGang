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
    class SpawnTileEntityComponent : OptionFoundation
    {

        static SpawnTileEntityComponent()
        {
            ConvexShape hexagon = Hexagon.GenHexagon();
            hexagon.FillColor = Color.White.SetAlpha(100);
            hexagon.OutlineThickness = 1;
            DrawComponent.Regiser("PlaceHexagon", hexagon);
        }
        Func<int, int, TileEntity> createDel;
        int range;
        public SpawnTileEntityComponent(int range, Func<int, int, TileEntity> createDel) : base(new DrawComponent("PlaceHexagon", Layer.UNIT_BASE - 1))
        {
            UiName = "Spawn";
            this.createDel = createDel;
            this.range = range;
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            map.GetSuroundingPositions(new Vector2i(parent.X, parent.Y), range, (e) => e == null, ref list);
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
