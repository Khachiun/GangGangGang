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

    public class Caret : Entity
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
}
