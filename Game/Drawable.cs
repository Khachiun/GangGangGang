using Czaplicki.SFMLE;
using SFML.Graphics;
using System.Collections.Generic;
using System;
using Czaplicki.SFMLE.Extentions;

namespace GangGang
{
    static class Layer
    {
        public const int TILE_BASE = -16;
        public const int UNIT_BASE = 0;
        public const int UI_BASE = 16;
        public const int CARET = 32;
        public const int DEBUG_BASE = 64;
    }

    class RectangelShape : Drawable
    {

        public Square Rectangel { get { return rectangel; } set { this.rectangel = value; ReloadVertices(); } }

        public Square Source { get { return source; } set { this.source = value; ReloadVertices(); } }

        public Color Color { get { return color; } set { this.color = value; ReloadVertices(); } }

        private Square rectangel;
        private Square source;
        private Texture texture;
        private Color color;

        public bool ignorRenderSates = false;

        private Vertex[] vertices;
        
        public RectangelShape(Square rectangel, Square source, Texture texture, Color color)
        {
            this.rectangel = rectangel;
            this.source = source;
            this.texture = texture;
            this.color = color;
            ReloadVertices();
        }
        public RectangelShape(Square rectangel, Square source, Texture texture) : this (rectangel, source, texture, Color.White){ }
        public RectangelShape(Square rectangel, Texture texture, Color color) : this(rectangel, texture.GetSqaure(), texture, color) { }
        public RectangelShape(Square rectangel, Texture texture) : this(rectangel, texture, Color.White) { }

        private void ReloadVertices()
        {
            vertices = rectangel.ToVertexArray(Source, Color);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (ignorRenderSates)
            {
                states = RenderStates.Default;
            }
            states.Texture = texture;
            target.Draw(vertices, PrimitiveType.Quads, states);
        }


    }
    class StaticRectangel : Drawable
    {
        private Texture texture;
        private Vertex[] vertices;

        public StaticRectangel(Square rectangel, Square sourceRectangel, Texture texture)
        {
            this.vertices = rectangel.ToVertexArray(sourceRectangel);
            this.texture = texture;
        }

        public StaticRectangel(Square rectangel, Texture texture)
        {
            this.vertices = rectangel.ToVertexArray();
            this.texture = texture;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = texture;
            target.Draw(vertices, PrimitiveType.Quads, states);
        }
    }
    class DrawComponent : Entity
    {
        public string ID { get; protected set; }
        public int Layer { get; protected set; }

        public DrawComponent(string ID, int layer)
        {
            this.ID = ID;
            this.Layer = layer;
        }
        public DrawComponent Clone()
        {
            return new DrawComponent(ID, Layer);
        }
    }


    class TextComponent : DrawComponent
    {
        Text text;
        public string Text
        {
            get { return text.DisplayedString; }
            set { text.DisplayedString = value; }
        }
        private static int Count { get; set; }
        public TextComponent(string text, int layer) : base("", layer)
        {
            base.ID = "TextComponent" + Game.randome.Next();
            this.text = DefaultText.GenerateText(text);
            DrawManager.Register.Add(ID, this.text);
        }

        ~TextComponent()
        {
            DrawManager.Register.Remove(this.ID);
        }
    }
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
                }else
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
