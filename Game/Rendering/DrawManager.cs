using Czaplicki.SFMLE;
using SFML.Graphics;
using System.Collections.Generic;

namespace GangGang
{
    class DrawManager
    {
        public static Dictionary<string, Drawable> Register = new Dictionary<string, Drawable>();

        public static void Render(RenderTarget target, Entity grandParent)
        {

            List<DrawComponent> list = new List<DrawComponent>();
            grandParent.FetchAllActive<DrawComponent>(ref list);
            list.Sort((left, right) => // sort based on y too
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

            foreach (DrawComponent item in list)
            {
                RenderStates renderStates = new RenderArgs().Translate(item.Position);
                Drawable drawable = Register[item.ID];
                target.Draw(drawable, renderStates);
            }
        }
    }
}
