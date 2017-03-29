using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using Czaplicki.SFMLE.Extentions;

namespace GangGang
{
    class AttackComponent : OptionFoundation
    {
        static AttackComponent()
        {
            ConvexShape shape = Hexagon.GenHexagon();
            shape.FillColor = Color.Red.SetAlpha(100);
            shape.OutlineThickness = 1;
            DrawComponent.Regiser("AttackComponent", shape);
        }

        private int damage;
        private float reange;
        public AttackComponent(int damage, int reange) : base(new DrawComponent("AttackComponent", Layer.UNIT_BASE - 1))
        {
            this.damage = damage;
            UiName = "Attack";
            this.reange =  (Hexagon.HEX_A * reange ) * ( Hexagon.HEX_A * reange);
        }

        protected override List<Vector2i> GetAvalibleSpots(TileMap map, TileEntity parent)
        {
            List<Vector2i> list = new List<Vector2i>();
            foreach (Tile item in map.Children)
            {
                if (item.Entity is TileEntity &&
                    (Position - item.Position).Pow2().Length() < reange)
                    list.Add(new Vector2i(item.X, item.Y));
            }
            return list;
        }

        protected override void OnSelectedClick(TileEntity executer, Tile resiver)
        {
            resiver.Entity.RetrivedDamage(damage, executer);
        }
    }
}
