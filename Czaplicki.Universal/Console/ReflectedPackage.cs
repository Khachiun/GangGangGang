using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Console
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CPropertyAttribute : Attribute
    {
        public string FieldName { get; set; }
        public CPropertyAttribute(string CommandName)
        {
            this.FieldName = CommandName;
        }
        public CPropertyAttribute()
        {
            this.FieldName = null;
        }
    }
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CFieldAttribute : Attribute
    {
        public string FieldName { get; set; }
        public CFieldAttribute(string CommandName)
        {
            this.FieldName = CommandName;
        }
        public CFieldAttribute()
        {
            this.FieldName = null;
        }
    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CCommandAttribute : Attribute
    {
        public string CommandName { get; set; }
        public CCommandAttribute(string CommandName)
        {
            this.CommandName = CommandName;
        }
    }

    public abstract class ReflectedPackage
    {
        public static Type[] acceptedTypes;

        static ReflectedPackage()
        {
            acceptedTypes = new Type[]
            { typeof(bool), typeof(int), typeof(float), typeof(char), typeof(string) , typeof(Vector.Vec2), typeof(Vector.Vec3),typeof(Vector.Vec4)};
        }

        protected bool TryConvert(Type type, string value, out object obj)
        {

            try
            {
                //trys to convert arg[i] to appropriet data type

                if (type == typeof(bool)) // bool
                {
                    obj = Convert.ToBoolean(value);
                    return true;
                }
                else if (type == typeof(int)) // int
                {
                    obj = Convert.ToInt32(value);
                    return true;
                }
                else if (type == typeof(float)) // float
                {
                    string s = value.Replace('.', ',');
                    obj = Convert.ToSingle(s);
                    return true;
                }
                else if (type == typeof(char) && value.Length == 1) // char
                {
                    obj = Convert.ToChar(value);
                    return true;
                }
                else if (type == typeof(string)) // string
                {
                    obj = value;
                    return true;
                }
                else if (type == typeof(Vector.Vec2))
                {
                    obj = Vector.Vec2.Parse(value);
                    return true;
                }
                else if (type == typeof(Vector.Vec3))
                {
                    obj = Vector.Vec3.Parse(value);
                    return true;
                }
                else if (type == typeof(Vector.Vec4))
                {
                    obj = Vector.Vec4.Parse(value);
                    return true;
                }
                else // notfound type
                {
                    obj = null;
                    return false;
                }
            }
            catch (Exception) { obj = null; return false; } // if fails : invalid indata or not correct overload method
        }
    }
}
