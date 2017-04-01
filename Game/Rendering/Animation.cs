using Czaplicki.SFMLE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang.Rendering
{
    class Animation : DrawComponent, Drawable
    {
        int frameSpeed, frameAmountWidth, startFrame, endFrame, frameCounter, frameWidth;

        Color color;
        Square worldSquare;
        Vertex[] verts;
        int currFrame;
        int CurrFrame
        {
            get { return currFrame; }
            set
            {
                currFrame = value;

                Square source = new Square( (CurrFrame % frameAmountWidth) * frameWidth, ((CurrFrame / frameAmountWidth) - (CurrFrame % frameAmountWidth)) * frameWidth );

                verts = worldSquare.ToVertexArray(source, color);

            }
        }
        RenderStates renderstates = RenderStates.Default;
        public Animation(string texturePath, int layer, int frameSpeed, int frameAmountWidth, int startFrame, int endFrame) : base("", layer)
        {
            this.ID = DrawComponent.CreateRandomID();
            DrawComponent.Regiser(this.ID, this);
            renderstates.Texture = new Texture(texturePath);

            CurrFrame = startFrame;
            this.frameSpeed = frameSpeed;
            this.frameAmountWidth = frameAmountWidth;
            frameWidth = (int)(renderstates.Texture.Size.X / frameAmountWidth);
            this.startFrame = startFrame;
            this.endFrame = endFrame;
            

        }

        public override void Update()
        {
            base.Update();
            frameCounter++;

            if (frameCounter % frameSpeed == 0) CurrFrame++;
            if (CurrFrame > endFrame) CurrFrame = startFrame;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(verts, PrimitiveType.Quads, renderstates);
        }
    }
}
