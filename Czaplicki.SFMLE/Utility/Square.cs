using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Czaplicki.Universal.Vector;

namespace Czaplicki.SFMLE
{
    public class Square
    {
        public float Width => Size.X;
        public float Height => Size.Y;

        public Vector2f Size
        {
            get
            {
                return new Vector2f(vecs[2].X - vecs[0].X, vecs[2].Y - vecs[0].Y);
            }
        }
        public Vector2f Position { get { return vecs[0]; } }
        public Vector2f BottomRight { get { return vecs[2]; } }
        /// <summary>
        ///   0___1
        ///   |___|   
        ///   3   2
        /// </summary>
        public Vector2f[] VectorArray { get { return vecs; } set { vecs = value; } }
        private Vector2f[] vecs = new Vector2f[4];

        public Square()
        {
            VectorArray[0] = new Vector2f(0, 0);
            VectorArray[1] = new Vector2f(1, 0);
            VectorArray[2] = new Vector2f(1, 1);
            VectorArray[3] = new Vector2f(0, 1);
        }
        public Square(Vector2f x, Vector2f y, Vector2f z, Vector2f w)
        {
            this.vecs[0] = x;
            this.vecs[1] = y;
            this.vecs[2] = z;
            this.vecs[3] = w;
        }
        public Square(float X, float Y, float width, float height)
        {
            VectorArray[0] = new Vector2f(X, Y);
            VectorArray[1] = new Vector2f(X + width, Y);
            VectorArray[2] = new Vector2f(X + width, Y + height);
            VectorArray[3] = new Vector2f(X, Y + height);
        }
        public Square(Vector2f TopLeft, Vector2f DownRight)
        {
            VectorArray[0] = TopLeft;
            VectorArray[1] = new Vector2f(DownRight.X, TopLeft.Y);
            VectorArray[2] = DownRight;
            VectorArray[3] = new Vector2f(TopLeft.X, DownRight.Y);
        }
        public Square(Vector2f position, float width, float height) : this(position.X, position.Y, width, height)
        {

        }
        public Square(int x, int y, Vector2f size) : this(x, y, size.X, size.Y)
        {

        }
        public Square(Vector2f size) : this(0, 0, size)
        {

        }
        public Square(int width, int height) : this(0, 0, width, height)
        {

        }
        public Square(float width, float height) : this(new Vector2f(), new Vector2f(width, height))
        {

        }
        public Square(Vec4 vec) : this(vec.X,vec.Y, vec.Z, vec.Y)
        {

        }


        public Square Clone()
        {
            return new Square(VectorArray[0], VectorArray[1], VectorArray[2], VectorArray[3]);
        }
        public SquareModifyer Modify()
        {
            return new SquareModifyer(this);
        }
        public SquareBoundingBox BoundingBox()
        {
            return new SquareBoundingBox(this);
        }
        public Vertex[] ToVertexArray()
        {
            Vertex[] v = new Vertex[4];
            for (int i = 0; i < 4; i++)
                v[i] = new Vertex(vecs[i]);
            return v;
        }
        public Vertex[] ToVertexArray(Color c)
        {
            Vertex[] v = new Vertex[4];
            for (int i = 0; i < 4; i++)
                v[i] = new Vertex(vecs[i], c);
            return v;
        }
        public Vertex[] ToVertexArray(Texture texture)
        {
            return this.ToVertexArray(new Square(0, 0, texture.Size.X, texture.Size.Y));
        }
        public Vertex[] ToVertexArray(Texture texture, Color c)
        {
            return this.ToVertexArray(new Square(0, 0, texture.Size.X, texture.Size.Y), c);
        }
        public Vertex[] ToVertexArray(Square TextureCoords)
        {
            return new Vertex[]
            {
                new Vertex(this.vecs[0], TextureCoords.vecs[0]),
                new Vertex(this.vecs[1], TextureCoords.vecs[1]),
                new Vertex(this.vecs[2], TextureCoords.vecs[2]),
                new Vertex(this.vecs[3], TextureCoords.vecs[3]),
            };
        }
        public Vertex[] ToVertexArray(Square TextureCoords, Color c)
        {
            return new Vertex[]
            {
                new Vertex(this.vecs[0], c, TextureCoords.vecs[0]),
                new Vertex(this.vecs[1], c, TextureCoords.vecs[1]),
                new Vertex(this.vecs[2], c, TextureCoords.vecs[2]),
                new Vertex(this.vecs[3], c, TextureCoords.vecs[3]),
            };
        }

        public Vertex[] ToVertexArrayLine()
        {
            return new Vertex[]
            {
                new Vertex(this.vecs[0]),
                new Vertex(this.vecs[1]),

                new Vertex(this.vecs[1]),
                new Vertex(this.vecs[2]),

                new Vertex(this.vecs[2]),
                new Vertex(this.vecs[3]),

                new Vertex(this.vecs[3]),
                new Vertex(this.vecs[0]),

            };
        }
        public Vertex[] ToVertexArrayLine(Color c)
        {
            return new Vertex[]
            {
                new Vertex(this.vecs[0], c),
                new Vertex(this.vecs[1], c),

                new Vertex(this.vecs[1], c),
                new Vertex(this.vecs[2], c),

                new Vertex(this.vecs[2], c),
                new Vertex(this.vecs[3], c),

                new Vertex(this.vecs[3], c),
                new Vertex(this.vecs[0], c),
            };
        }
        public Vertex[] ToVertexArrayLineStrip(Color c)
        {
            return new Vertex[]
            {
                new Vertex(this.vecs[0], c),
                new Vertex(this.vecs[1], c),
                new Vertex(this.vecs[2], c),
                new Vertex(this.vecs[3], c),
                new Vertex(this.vecs[0], c),
            };
        }
        /// <summary>
        /// Multiplies the Square's Vectors
        /// </summary>
        /// <param name="s"></param>
        /// <param name="f"></param>
        /// <returns>Multiplied Square</returns>
        public static Square operator *(Square square, float f)
        {
            Square s = square.Clone();
            for (int i = 0; i < 4; i++)
                s.vecs[i] *= f;
            return s;
        }
        public static Square operator *(Square square, Vector2f f)
        {
            Square s = square.Clone();
            for (int i = 0; i < 4; i++)
            {
                s.vecs[i].X *= f.X;
                s.vecs[i].Y *= f.Y;
            }
            return s;
        }
        public static Square operator +(Square square, Vector2f vec)
        {
            Square s = square.Clone();
            s.vecs[0] += vec;
            s.vecs[1] += vec;
            s.vecs[2] += vec;
            s.vecs[3] += vec;
            return s;
        }
        public static Square operator -(Square square, Vector2f vec)
        {
            Square s = square;
            s.vecs[0] -= vec;
            s.vecs[1] -= vec;
            s.vecs[2] -= vec;
            s.vecs[3] -= vec;
            return s;
        }
        public static Square operator +(Square left, Square right)
        {
            return new Square(
                left.vecs[0] + right.vecs[0],
                left.vecs[1] + right.vecs[1],
                left.vecs[2] + right.vecs[2],
                left.vecs[3] + right.vecs[3]);
        }
        public static Square operator -(Square left, Square right)
        {
            return new Square(
                left.vecs[0] - right.vecs[0],
                left.vecs[1] - right.vecs[1],
                left.vecs[2] - right.vecs[2],
                left.vecs[3] - right.vecs[3]);
        }



        public static implicit operator Vertex[] (Square s)
        {
            Vertex[] v = new Vertex[4];
            for (int i = 0; i < 4; i++)
                v[i] = new Vertex(s.vecs[i]);
            return v;
        }
        public static implicit operator FloatRect(Square s)
        {
            return new FloatRect(s.Position, s.Size);
        }
        public static implicit operator IntRect(Square s)
        {
            return new IntRect((Vector2i)s.Position, (Vector2i)s.Size);
        }

        public static explicit operator Square(FloatRect s)
        {
            return new Square(s.Left, s.Top, s.Width, s.Height);
        }
        public static explicit operator Square(IntRect s)
        {
            return new Square(s.Left, s.Top, s.Width, s.Height);
        }
        public static explicit operator VideoMode(Square s)
        {
            return new VideoMode((uint)s.Size.X, (uint)s.Size.Y);
        }
        public string ToStringAdvance()
        {
            var s = new StringBuilder();
            s.AppendFormat(
                "[Square] | V1/VX: {0}, {1} | V2/VY: {2}, {3} | V3/VZ: {4}, {5} | V4/VW: {6}, {7}"
                , VectorArray[0].X, VectorArray[0].Y, VectorArray[1].X, VectorArray[1].Y, VectorArray[2].X, VectorArray[2].Y, VectorArray[3].X, VectorArray[3].Y);
            return s.ToString();
        }
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendFormat(
                "[Square] {0}, {1} | {2}, {3}"
                , Position.X, Position.Y, Size.X, Size.Y);
            return s.ToString();
        }


        public static Square Interpolate(Square a, Square b, float f)
        {
            return a + (( b - a ) * f);

        }
        public static Square Interpolate(Square a, Square b, float  time, float duration)
        {
            return a + (b - a) * (time / duration);

        }

        
    }
    public class SquareBoundingBox
    {
        public Square square;

        public SquareBoundingBox(Square square)
        {
            var min = square.VectorArray[0];
            var max = square.VectorArray[0];
            // X
            for (int i = 1; i < 4; i++)
            {
                if (min.X > square.VectorArray[i].X)
                    min.X = square.VectorArray[i].X;

                if (max.X < square.VectorArray[i].X)
                    max.X = square.VectorArray[i].X;

                if (min.Y > square.VectorArray[i].Y)
                    min.Y = square.VectorArray[i].Y;

                if (max.Y < square.VectorArray[i].Y)
                    max.Y = square.VectorArray[i].Y;
            }

            this.square = new Square(min, max);
        }
        /// <summary>
        /// Looks if the twos square.BoundingBox() intersects 
        /// </summary>
        /// <param name="otherSquare">Useses Square.BoundingBox()</param>
        public bool Intersect(SquareBoundingBox otherSquare)
        {
            return !(square.VectorArray[2].X < ((Square)otherSquare).VectorArray[0].X ||
                ((Square)otherSquare).VectorArray[2].X < square.VectorArray[0].X ||
                square.VectorArray[2].Y < ((Square)otherSquare).VectorArray[0].Y ||
                ((Square)otherSquare).VectorArray[2].Y < square.VectorArray[0].Y);
        }
        public bool Intersect(Vector2f point)
        {
            return point.X > square.Position.X && point.X < square.Position.X + square.Size.X &&
                point.Y > square.Position.Y && point.Y < square.Position.Y + square.Size.Y;
        }
        public static explicit operator Square(SquareBoundingBox sm)
        {
            return sm.square;
        }
    }
    public class SquareModifyer
    {
        public enum Edge
        {
            Top = 0, Right = 1, Buttom = 2, Left = 3

            ///   0___1
            ///   |___|   
            ///   3   2
            ///
        }
        public Square square;
        public SquareModifyer(Square s)
        {
            square = s;
        }
        public SquareModifyer TranslateEdge(Edge edge, Vector2f ammount)
        {
            int i = (int)edge;

            int ii = i > 2 ? 0 : i + 1;

            square.VectorArray[i] += ammount;

            square.VectorArray[ii] += ammount;

            return this;
        }
        /// <summary>
        /// Sets The edge at position
        /// origin : Topleft
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public SquareModifyer SetEdge(Edge edge, Vector2f position)
        {
            int i = (int)edge;

            int ii = i > 2 ? 0 : i + 1;

            if (i > 1)
            {
                int temp = i;
                i = ii;
                ii = temp;
            }

            Vector2f diff = square.VectorArray[i] - square.VectorArray[ii];

            square.VectorArray[i] = position;

            square.VectorArray[ii] = position - diff;

            return this;
        }
        public SquareModifyer SetTopleft(Vector2f newposition)
        {
            Vector2f diff = square.VectorArray[0] - newposition;
            for (int i = 0; i < 4; i++)
                square.VectorArray[i] += diff;
            return this;
        }

        public SquareModifyer ScaleFromCenter(int ammout)
        {
            square.VectorArray[0] += new Vector2f(ammout, ammout);
            square.VectorArray[1] += new Vector2f(-ammout, ammout);
            square.VectorArray[2] += new Vector2f(-ammout, -ammout);
            square.VectorArray[3] += new Vector2f(ammout, -ammout);
            return this;
        }

        public static implicit operator Square(SquareModifyer sm)
        {
            return sm.square;
        }
    }

    public class SquareArray
    {
        Square[] Array;
        public int Length { get { return Array.Length; } }

        public SquareArray(params Square[] squares)
        {
            Array = squares;
        }
        
        public Square this[int index]
        {
            get { return Array[index]; }
            set { Array[index] = value; }
        }

        public Vertex[] ToVertexArray()
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray());
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Color c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(c));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Texture texture)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(texture));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Texture texture, Color c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(texture, c));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Square TextureCoords)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(TextureCoords));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Square TextureCoords, Color c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(TextureCoords, c));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Texture[] textures)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(textures[i]));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Texture[] textures, Color c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(textures[i], c));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Texture[] textures, Color[] c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(textures[i], c[i]));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Square[] TextureCoords)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(TextureCoords[i]));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Square[] TextureCoords, Color c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(TextureCoords[i], c));
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArray(Square[] TextureCoords, Color[] c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i++)
            {
                fa.AddRange(Array[i].ToVertexArray(TextureCoords[i], c[i]));
            }
            return fa.ToArray();
        }

        public Vertex[] ToVertexArrayLine()
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i += 4)
            {
                fa.AddRange(Array[i].ToVertexArray());
                fa.AddRange(Array[i + 1].ToVertexArray());
                fa.AddRange(Array[i + 2].ToVertexArray());
                fa.AddRange(Array[i + 3].ToVertexArray());
                fa.AddRange(Array[i].ToVertexArray());
            }
            return fa.ToArray();
        }
        public Vertex[] ToVertexArrayLine(Color c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; ++i)
            {
                Vertex[] verts = Array[i].ToVertexArray(c);

                fa.Add(verts[0]);
                fa.Add(verts[1]);

                fa.Add(verts[1]);
                fa.Add(verts[2]);

                fa.Add(verts[2]);
                fa.Add(verts[3]);

                fa.Add(verts[3]);
                fa.Add(verts[0]);

                //fa.AddRange(Array[i + 1].ToVertexArray(c));
                //fa.AddRange(Array[i + 2].ToVertexArray(c));
                //fa.AddRange(Array[i + 3].ToVertexArray(c));
                //fa.AddRange(Array[i].ToVertexArray(c));
                //fa.AddRange(Array[i].ToVertexArray(c));
            }
            return fa.ToArray();
        }

        public Vertex[] ToVertexArrayLine(Color[] c)
        {
            List<Vertex> fa = new List<Vertex>();
            for (int i = 0; i < Array.Length; i += 4)
            {
                fa.AddRange(Array[i].ToVertexArray(c[i / 4]));
                fa.AddRange(Array[i + 1].ToVertexArray(c[i / 4]));
                fa.AddRange(Array[i + 2].ToVertexArray(c[i / 4]));
                fa.AddRange(Array[i + 3].ToVertexArray(c[i / 4]));
                fa.AddRange(Array[i].ToVertexArray(c[i / 4]));
            }
            return fa.ToArray();
        }

        public bool Hit(Vector2f point, out int index)
        {
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i].BoundingBox().Intersect(point))
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }

        public static implicit operator Square[] (SquareArray sa)
        {
            return sa.Array;
        }
        public static implicit operator SquareArray(Square[] sa)
        {
            return new SquareArray(sa);
        }
        public static SquareArray operator +(SquareArray left, Square right)
        {
            var tempArray = left.Array;
            left.Array = new Square[left.Array.Length + 1];
            tempArray[left.Array.Length] = right;
            return left;
        }
        public static SquareArray operator +(Square left, SquareArray right)
        {
            var tempArray = right.Array;
            right.Array = new Square[right.Array.Length + 1];
            tempArray[right.Array.Length] = left;
            return right;
        }
        public static SquareArray operator +(SquareArray left, SquareArray right)
        {
            var tempArray = left.Array;
            left.Array = new Square[left.Array.Length + right.Array.Length];
            for (int i = 0; i < right.Array.Length; i++)
            {
                tempArray[left.Array.Length + i] = right[i];
            }
            return left;
        }
    }
}
