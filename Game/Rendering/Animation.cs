using Czaplicki.SFMLE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
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

                Square source = new Square( 
                    (CurrFrame % frameAmountWidth) * frameWidth,
                    ((CurrFrame - (CurrFrame % frameAmountWidth)) / frameAmountWidth) * frameWidth,
                    frameWidth, frameWidth);

                verts = worldSquare.ToVertexArray(source, color);

            }
        }
        Texture texture;
        public Animation(Square sq, Color color, string texturePath, int layer, int frameSpeed, int frameAmountWidth, int startFrame, int endFrame) : base("", layer)
        {
            this.color = color;
            worldSquare = sq;
            this.ID = DrawComponent.CreateRandomID();
            DrawComponent.Regiser(this.ID, this);
            texture = new Texture(texturePath);

            this.frameSpeed = frameSpeed;
            this.frameAmountWidth = frameAmountWidth;
            frameWidth = (int)(texture.Size.X / frameAmountWidth);
            this.startFrame = startFrame;
            this.endFrame = endFrame;
            CurrFrame = startFrame;
            

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
            states.Texture = texture;
            target.Draw(verts, PrimitiveType.Quads, states);
        }
    }
}
