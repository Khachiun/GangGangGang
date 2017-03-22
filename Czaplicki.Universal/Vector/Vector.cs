using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Czaplicki.Universal.Vector.Vector;

namespace Czaplicki.Universal.Vector
{
    public class Vector
    {
        public Vector()
        {

        }
        public Vector(float x)
        {
            values = new float[] { x };
        }

        protected float[] values;

        public static Vec1 Vec(float x)
        {
            var v = new Vec1();
            v.values = new float[4] { x, 0, 0, 0 };
            return v;
        }
        public static Vec2 Vec(float x, float y)
        {
            var v = new Vec2();
            v.values = new float[] { x, y, 0, 0 };
            return v;
        }
        public static Vec3 Vec(float x, float y, float z)
        {
            var v = new Vec3();
            v.values = new float[] { x, y, z, 0 };
            return v;
        }
        public static Vec4 Vec(float x, float y, float z, float w)
        {
            var v = new Vec4();
            v.values = new float[] { x, y, z, w };
            return v;
        }

        //public static Vector operator +(Vector left, Vector right)
        //{
        //    Vector resault = new float[4];            
        //    for (int i = 0; i < 4; i++)
        //    {
        //        resault.values[i] = left.values[i] + right.values[i];
        //    }
        //    return resault;
        //}
        //public static Vector operator -(Vector left, Vector right)
        //{
        //    Vector resault = new float[4];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        resault.values[i] = left.values[i] - right.values[i];
        //    }
        //    return resault;
        //}
        //public static Vector operator *(Vector left, Vector right)
        //{
        //    Vector resault = new float[4];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        resault.values[i] = left.values[i] * right.values[i];
        //    }
        //    return resault;
        //}
        //public static Vector operator /(Vector left, Vector right)
        //{
        //    Vector resault = new float[4];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (right.values[i] != 0)
        //        resault.values[i] = left.values[i] / right.values[i];
        //    }
        //    return resault;
        //}
        //public static Vector operator &(Vector left, Vector right)
        //{
        //    Vector resault = new float[4];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        resault.values[i] = left.values[i] * right.values[i];
        //    }
        //    return resault;
        //}
        public static implicit operator Vector(float[] values)
        {
            Vector resault = new Vector();
            resault.values = values;
            return resault;
        }

    }
    public class Vec1 : Vector
    {
        public float X => values[0];


        public static Vec1 operator +(Vec1 left, Vector right)
        {
            Vec1 resault = new Vec1();
            resault.values = new float[1];
            for (int i = 0; i < 1; i++)
            {
                resault.values[i] = left.values[i] + ((Vec1)right).values[i];
            }
            return resault;
        }
        public static Vec1 operator -(Vec1 left, Vector right)
        {
            Vec1 resault = new Vec1();
            resault.values = new float[1];
            for (int i = 0; i < 1; i++)
            {
                resault.values[i] = left.values[i] - ((Vec1)right).values[i];
            }
            return resault;
        }
        public static Vec1 operator *(Vec1 left, Vector right)
        {
            Vec1 resault = new Vec1();
            resault.values = new float[1];
            for (int i = 0; i < 1; i++)
            {
                resault.values[i] = left.values[i] * ((Vec1)right).values[i];
            }
            return resault;
        }
        public static Vec1 operator /(Vec1 left, Vector rightin)
        {
            Vec1 resault = new Vec1();
            resault.values = new float[1];
            Vec1 right = (Vec1)rightin;
            for (int i = 0; i < 1; i++)
            {
                if (right.values[i] != 0)
                    resault.values[i] = left.values[i] / right.values[i];
            }
            return resault;
        }

        public static Vec1 Parse(string value) // x
        {
            float x = System.Convert.ToSingle(value);
            return new Vec1() { values = new float[] { x } };
        }
    }
    public class Vec2 : Vec1
    {

        public Vec2()
        {

        }
        public float Y => values[1];

        public static Vec2 operator +(Vec2 left, Vector right)
        {
            Vec2 resault = new Vec2();
            resault.values = new float[2];
            for (int i = 0; i < 2; i++)
            {
                resault.values[i] = left.values[i] + ((Vec2)right).values[i];
            }
            return resault;
        }
        public static Vec2 operator -(Vec2 left, Vector right)
        {
            Vec2 resault = new Vec2();
            resault.values = new float[2];
            for (int i = 0; i < 2; i++)
            {
                resault.values[i] = left.values[i] - ((Vec2)right).values[i];
            }
            return resault;
        }
        public static Vec2 operator *(Vec2 left, Vector right)
        {
            Vec2 resault = new Vec2();
            resault.values = new float[2];
            for (int i = 0; i < 2; i++)
            {
                resault.values[i] = left.values[i] * ((Vec2)right).values[i];
            }
            return resault;
        }
        public static Vec2 operator /(Vec2 left, Vector rightin)
        {
            Vec2 resault = new Vec2();
            resault.values = new float[2];
            Vec2 right = (Vec2)rightin;
            for (int i = 0; i < 2; i++)
            {
                if (right.values[i] != 0)
                    resault.values[i] = left.values[i] / right.values[i];
            }
            return resault;
        }

        public new static Vec2 Parse(string value) // x,x
        {
            string[] args = value.Split(',');
            float x = System.Convert.ToSingle(args[0]);
            float y = System.Convert.ToSingle(args[1]);
            return new Vec2() { values = new float[] { x, y } };
        }

    }


    public class Vec3 : Vec2
    {
        public float Z => values[2];

        public Vec2 XY => Vec(X, Y);

        public Vec2 XZ => Vec(X, Z);
        public Vec2 YZ => Vec(Y, Z);

        public static Vec3 operator +(Vec3 left, Vector right)
        {
            Vec3 resault = new Vec3();
            resault.values = new float[3];
            for (int i = 0; i < 3; i++)
            {
                resault.values[i] = left.values[i] + ((Vec3)right).values[i];
            }
            return resault;
        }
        public static Vec3 operator -(Vec3 left, Vector right)
        {
            Vec3 resault = new Vec3();
            resault.values = new float[3];
            for (int i = 0; i < 3; i++)
            {
                resault.values[i] = left.values[i] - ((Vec3)right).values[i];
            }
            return resault;
        }
        public static Vec3 operator *(Vec3 left, Vector right)
        {
            Vec3 resault = new Vec3();
            resault.values = new float[3];
            for (int i = 0; i < 3; i++)
            {
                resault.values[i] = left.values[i] * ((Vec3)right).values[i];
            }
            return resault;
        }
        public static Vec3 operator /(Vec3 left, Vector rightin)
        {
            Vec3 resault = new Vec3();
            resault.values = new float[3];
            Vec3 right = (Vec3)rightin;
            for (int i = 0; i < 3; i++)
            {
                if (right.values[i] != 0)
                    resault.values[i] = left.values[i] / right.values[i];
            }
            return resault;
        }

        public new static Vec3 Parse(string value) // x,x,x
        {
            string[] args = value.Split(',');
            float x = System.Convert.ToSingle(args[0]);
            float y = System.Convert.ToSingle(args[1]);
            float z = System.Convert.ToSingle(args[2]);
            return new Vec3() { values = new float[] { x, y, z } };
        }
    }
    public class Vec4 : Vec3
    {
        public float W => values[2];

        public Vec2 XW => Vec(X, W);
        public Vec2 YW => Vec(Y, W);
        public Vec2 ZW => Vec(Z, W);

        public Vec3 XYZ => Vec(X, Y, Z);

        public Vec3 XYW => Vec(X, Y, W);
        public Vec3 YZW => Vec(Y, Z, W);
        public Vec3 XZW => Vec(X, Z, W);


        public static Vec4 operator +(Vec4 left, Vector right)
        {
            Vec4 resault = new Vec4();
            resault.values = new float[4];
            for (int i = 0; i < 4; i++)
            {
                resault.values[i] = left.values[i] + ((Vec4)right).values[i];
            }
            return resault;
        }
        public static Vec4 operator -(Vec4 left, Vector right)
        {
            Vec4 resault = new Vec4();
            resault.values = new float[4];
            for (int i = 0; i < 4; i++)
            {
                resault.values[i] = left.values[i] - ((Vec4)right).values[i];
            }
            return resault;
        }
        public static Vec4 operator *(Vec4 left, Vector right)
        {
            Vec4 resault = new Vec4();
            resault.values = new float[4];
            for (int i = 0; i < 4; i++)
            {
                resault.values[i] = left.values[i] * ((Vec4)right).values[i];
            }
            return resault;
        }
        public static Vec4 operator /(Vec4 left, Vector rightin)
        {
            Vec4 resault = new Vec4();
            resault.values = new float[4];
            Vec4 right = (Vec4)rightin;
            for (int i = 0; i < 4; i++)
            {
                if (right.values[i] != 0)
                    resault.values[i] = left.values[i] / right.values[i];
            }
            return resault;
        }


        public new static Vec4 Parse(string value) // x,x,x,x
        {
            string[] args = value.Split(',');
            float x = System.Convert.ToSingle(args[0]);
            float y = System.Convert.ToSingle(args[1]);
            float z = System.Convert.ToSingle(args[2]);
            float w = System.Convert.ToSingle(args[3]);
            return new Vec4() { values = new float[] { x, y, z, w } };
        }
    }
}
