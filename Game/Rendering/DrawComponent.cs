using Czaplicki.SFMLE;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace GangGang
{

    //Instence
    public partial class DrawComponent : Entity
    {
        protected string ID { get; set; }
        protected int Layer { get; set; }

        protected bool singleUseDrawble;

        public DrawComponent(string ID, int layer)
        {
            this.ID = ID;
            this.Layer = layer;
            this.singleUseDrawble = false;

        }
        public DrawComponent(Drawable drawble, int layer)
        {
            this.ID = CreateRandomID();
            this.Layer = layer;
            this.singleUseDrawble = true;

            Register.Add(ID, drawble);
        }

        ~DrawComponent()
        {
            if (singleUseDrawble)
            {
                Register.Remove(ID);
            }
        }


        public DrawComponent Clone()
        {
            return new DrawComponent(ID, Layer);
        }
        public static void Regiser(string ID, Drawable drawable)
        {
            Register.Add(ID, drawable);
        }

        private static int rCount;
        protected static string CreateRandomID()
        {
            rCount++;
            return "#&%#¤" + rCount;
        }
        private static Dictionary<string, Drawable> Register = new Dictionary<string, Drawable>();

        public static class Manager
        {


            public static void Render(RenderTarget target, Entity masterEntity)
            {
                List<DrawComponent> list = new List<DrawComponent>();
                masterEntity.FetchAllActive<DrawComponent>(ref list);
                list.Sort((left, right) =>
                {
                    int diff = left.Layer - right.Layer;
                    if (diff == 0)
                    {
                        return (int)(left.Position.Y - right.Position.Y);
                    }
                    else
                    {
                        return diff;
                    }
                });

                Vector2f lastPos = new Vector2f();
                RenderArgs args = new RenderArgs();
                
                foreach (DrawComponent item in list)
                {
                    args.Translate(item.Position - lastPos);
                    Drawable drawable = Register[item.ID];
                    target.Draw(drawable, args);
                    lastPos = item.Position;
                }

            }
        }

    }
}
