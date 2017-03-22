using Czaplicki.SFMLE;
using SFML.Graphics;
using System.Collections.Generic;
using System;
using Czaplicki.SFMLE.Extentions;

namespace GangGang
{
    //static class Layer
    //{
    //    public const int TILE_BASE = -16;
    //    public const int UNIT_BASE = 0;
    //    public const int UI_BASE = 16;
    //    public const int CARET = 32;
    //    public const int DEBUG_BASE = 64;
    //}

    //class RectangelShape : Drawable
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
        
    //    public RectangelShape(Square rectangel, Square source, Texture texture, Color color)
    //    {
    //        this.rectangel = rectangel;
    //        this.source = source;
    //        this.texture = texture;
    //        this.color = color;
    //        ReloadVertices();
    //    }
    //    public RectangelShape(Square rectangel, Square source, Texture texture) : this (rectangel, source, texture, Color.White){ }
    //    public RectangelShape(Square rectangel, Texture texture, Color color) : this(rectangel, texture.GetSqaure(), texture, color) { }
    //    public RectangelShape(Square rectangel, Texture texture) : this(rectangel, texture, Color.White) { }

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

    //class AnimationComponent : DrawComponent
    //{
    //    RectangelShape shape;
    //    public AnimationComponent(string ID, int layer) : base(ID, layer)
    //    {
            
    //    }
    //}

    //class StaticRectangel : Drawable
    //{
    //    private Texture texture;
    //    private Vertex[] vertices;

    //    public StaticRectangel(Square rectangel, Square sourceRectangel, Texture texture)
    //    {
    //        this.vertices = rectangel.ToVertexArray(sourceRectangel);
    //        this.texture = texture;
    //    }

    //    public StaticRectangel(Square rectangel, Texture texture)
    //    {
    //        this.vertices = rectangel.ToVertexArray();
    //        this.texture = texture;
    //    }

    //    public void Draw(RenderTarget target, RenderStates states)
    //    {
    //        states.Texture = texture;
    //        target.Draw(vertices, PrimitiveType.Quads, states);
    //    }
    //}
    


    
    
}
