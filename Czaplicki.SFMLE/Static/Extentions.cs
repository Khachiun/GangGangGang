using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE.Extentions
{
    public static class Extentions
    {

        // Convert an object to a byte array
        public static byte[] ToByteArray(this Object obj)
        {
            if (obj == null)
                return null;

            byte[] buffer;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                buffer = ms.ToArray();
            }
            return buffer;
        }

        // Convert a byte array to an Object
        public static Object ToObject(this byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }


        public static Color SetAlpha(this Color c, byte a)
        {
            c.A = a;
            return c;
        }

        public static bool PointCollition(this CircleShape c, Vector2f point)
        {
            Vector2f diff = (c.Position + new Vector2f(c.Radius, c.Radius) - (Vector2f)point).Pow2(); // fix

            return diff.X + diff.Y < c.Radius * c.Radius;
        }

        public static Square GetSqaure(this Texture texture)
        {
            return new Square(0, 0, texture.Size.X, texture.Size.Y);
        }

        public static float Length(this Vector2f left)
        {
            return Math.Abs(left.X) + Math.Abs(left.Y);
        }

        public static Vector2f Pow(this Vector2f left, int right)
        {
            return new Vector2f((float)Math.Pow(left.X, right), (float)Math.Pow(left.Y, right));
        }

        public static Vector2f Pow2(this Vector2f left)
        {
            return new Vector2f(left.X * left.X, left.Y * left.Y);
        }

        public static Vector2f Normalize(this Vector2f source)
        {
            float length = (float)Math.Sqrt((source.X * source.X) + (source.Y * source.Y));


            if (length != 0)
                return new Vector2f(source.X / length, source.Y / length);
            else
                return source;
        }

        public static Texture ToTexture(this Color c)
        {
            Texture t = new Texture(1, 1);
            t.Update(new byte[] { c.R, c.G, c.B, c.A });
            return t;
        }
        public static Vertex[] SetColor(this Vertex[] array, Color color)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Color = color;
            }
            return array;
        }
    }
}
