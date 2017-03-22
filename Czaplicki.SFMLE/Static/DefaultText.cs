using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE
{
    public static class DefaultText
    {
        public static Font Font { get; set; }
        private static uint size;
        public static uint Size
        {
            get { return size; }
            set
            {
                size = value;
                PixelSize = new Text("", Font, value).CharacterSize;
                SizeChangedEvent?.Invoke(null, new EventArgs());
                drawObject = GenerateText("", new Vector2f());
                
            }
        }
        public static uint PixelSize { get; private set; }
        public static Color Color { get; set; }

        private static Text drawObject;

        public static void Display(RenderTarget target, object text, RenderStates renderstates)
        {
            drawObject.DisplayedString = text.ToString();
            target.Draw(drawObject, renderstates);
        }

        public static void Load(string fontAddress, uint size, Color color)
        {
            Font = new Font(fontAddress);
            Size = size;
            Color = color;

            drawObject = GenerateText("", new Vector2f());

            
        }
        public static Text GenerateText(object text)
        {
            var final = new Text(text.ToString(), Font, Size);
            final.Color = Color;
            return final;
        }
        public static Text GenerateText(object text, Vector2f position)
        {
            var final = new Text(text.ToString(), Font, Size);
            final.Rotation += 90;
            final.Color = Color;
            final.Position = position;
            return final;
        }
        public static Text GenerateText(object text, Vector2f position, Color color)
        {
            var final = new Text(text.ToString(), Font, Size);
            final.Color = color;
            final.Position = position;
            return final;
        }
        public static Text GenerateText(object text, Vector2f position, Color color, uint size)
        {
            var final = new Text(text.ToString(), Font, Size);
            final.Color = color;
            final.Position = position;
            final.CharacterSize = size;
            return final;
        }
        public static event EventHandler SizeChangedEvent;
    }
}
