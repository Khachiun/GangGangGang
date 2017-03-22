using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Czaplicki.SFMLE
{
    public class RenderArgs
    {
        public static RenderArgs Default { get; set; } = new RenderArgs(RenderStates.Default);



        RenderStates rs;
        
        public RenderArgs()
        {
            rs = RenderStates.Default;
        }
        public RenderArgs(RenderStates rs)
        {
            this.rs = rs;
        }
        public RenderArgs(Texture texture) : this()
        {
            SetTextrue(texture);
        }

        public RenderArgs SetTextrue(Texture texture) { rs.Texture = texture; return this; }
        public RenderArgs SetShader(Shader shader) { rs.Shader = shader; return this; }
        public RenderArgs SetBlendMode(BlendMode mode) { rs.BlendMode = mode; return this; }
        public RenderArgs SetTransform(Transform transform) { rs.Transform = transform; return this; }
        public RenderArgs Translate(Vector2f offset) { rs.Transform.Translate(offset); return this; }
        public RenderArgs Translate(float offsetX, float offsetY) { Vector2f offset = new Vector2f(offsetX, offsetY); rs.Transform.Translate(offset); return this; }
        public RenderArgs Rotate(float angle) { rs.Transform.Rotate(angle); return this; }

        public static implicit operator RenderStates(RenderArgs args)
        {
            return args.rs;
        }
    }
}
