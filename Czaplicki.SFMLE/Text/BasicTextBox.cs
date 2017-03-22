using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE.TextInput
{
    public class BasicTextBox : TextInput
    {

        protected Vertex[] vertices;
        protected RenderWindow window;
        protected RenderStates renderStates;
        protected float leftPadding;
        

        public BasicTextBox(RenderWindow window, Square box, Color color) : base(window)
        {
            vertices = box;
            renderStates = new RenderArgs().SetTextrue(color.ToTexture());
            this.window = window;
        }
        public BasicTextBox(RenderWindow window, Square box, Color color, float leftPadding) : base(window)
        {
            vertices = box;
            renderStates = new RenderArgs().SetTextrue(color.ToTexture());
            this.window = window;
            this.leftPadding = leftPadding;
        }

        public virtual void Draw()
        {
            var textPosition = vertices[0].Position;
            textPosition.X += leftPadding;
            var text = DefaultText.GenerateText(base.Text, textPosition); // get Drawable Text object of text in position of topleft of box
            var curserWorldPosition = text.FindCharacterPos(base.CurserPostion); // gets position off curser
            curserWorldPosition.Y = vertices[0].Position.Y - DefaultText.PixelSize * 0.1f; // fixes position of curser
            curserWorldPosition.X += leftPadding;
            var CurserText = DefaultText.GenerateText("_", curserWorldPosition); // et Drawable Text object for curser

            window.Draw(vertices, PrimitiveType.Quads, renderStates); // Box
            window.Draw(text);
            window.Draw(CurserText);
        }


    }
}
