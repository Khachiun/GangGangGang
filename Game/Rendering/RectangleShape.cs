using Czaplicki.SFMLE;
using Czaplicki.SFMLE.Extentions;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    //class RectangleShape : Drawable
    //{

    //    public Square Rectangel { get { return rectangel; } set { this.rectangel = value; ReloadVertices(); } }

    //    public Square Source { get { return source; } set { this.source = value; ReloadVertices(); } }

    //    public Color Color { get { return color; } set { this.color = value; ReloadVertices(); } }

    //    private Square rectangel;
    //    private Square source;
    //    private Texture texture;
    //    private Color color;

    //    public bool ignorRenderSates = false;

    //    private Vertex[] vertices;

    //    public RectangleShape(Square rectangel, Square source, Texture texture, Color color)
    //    {
    //        this.rectangel = rectangel;
    //        this.source = source;
    //        this.texture = texture;
    //        this.color = color;
    //        ReloadVertices();
    //    }
    //    public RectangleShape(Square rectangel, Square source, Texture texture) : this(rectangel, source, texture, Color.White) { }
    //    public RectangleShape(Square rectangel, Texture texture, Color color) : this(rectangel, texture.GetSqaure(), texture, color) { }
    //    public RectangleShape(Square rectangel, Texture texture) : this(rectangel, texture, Color.White) { }

    //    private void ReloadVertices()
    //    {
    //        vertices = rectangel.ToVertexArray(Source, Color);
    //    }

    //    public void Draw(RenderTarget target, RenderStates states)
    //    {
    //        if (ignorRenderSates)
    //        {
    //            states = RenderStates.Default;
    //        }
    //        states.Texture = texture;
    //        target.Draw(vertices, PrimitiveType.Quads, states);
    //    }


    //}

}
