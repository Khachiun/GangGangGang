using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE
{
    public class Line
    {
        Vertex[] verts = new Vertex[2];
        public Line(Vector2f point1, Vector2f point2)
        {
            verts[0] = new Vertex(point1);
            verts[1] = new Vertex(point2);
        }
        public Line(Vector2f point1, Vector2f point2, Color color)
        {
            verts[0] = new Vertex(point1, color);
            verts[1] = new Vertex(point2, color);
        }
        public static implicit operator Vertex[](Line l)
        {
            return l.verts;
        }
    }
}
