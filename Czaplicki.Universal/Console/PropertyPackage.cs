using Czaplicki.Universal.Debug;
using Czaplicki.Universal.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Console
{

    public class PropertyPackage : ReflectedPackage
    {
        bool global;

        string objectName;
        object obj;

        string staticTag;
        bool Static;


        Dictionary<string, PropertyInfo> PropertyInfos = new Dictionary<string, PropertyInfo>();

        private bool iterate(CommandEventArgs e)
        {
            var args = e.Commands;
            foreach (var kvp in PropertyInfos)
            {
                if (kvp.Key == args[0])
                {
                    if (args.Length == 3 && args[1] == "=")
                    {
                        var type = kvp.Value.PropertyType;
                        object Invokevalue = null;
                        bool validType = true;

                        validType = TryConvert(type, args[2], out Invokevalue);

                        if (validType)
                        {
                            //obj will be ignored if static field
                            kvp.Value.SetValue(obj, Invokevalue);
                        }
                        break;
                    }
                    else if (args.Length == 1)
                    {
                        CConsole.Logg(kvp.Value.GetValue(obj).ToString());
                    }
                }
            }
            return false;
        }

        private bool Check(CommandEventArgs e, out CommandEventArgs owne)
        {
            if (global)
            {
                owne = new CommandEventArgs { Command = e.Command };
                return true;
            }

            if (Static && e.Command.StartsWith(staticTag + ":"))
            {
                owne = new CommandEventArgs() { Command = e.Command.Substring(staticTag.Length + 1) };
                return true;
            }

            if (!Static && e.Command.StartsWith(objectName + "."))
            {
                owne = new CommandEventArgs() { Command = e.Command.Substring(objectName.Length + 1) };
                return true;
            }
            owne = null;
            return false;
        }


        public bool Run(CommandEventArgs e)
        {
            CommandEventArgs owne;
            if (Check(e, out owne))
            {
                return iterate(owne);
            }
            return false;
        }
        public void Attatch(int callindex)
        {
            CConsole.CommandHandler.Add(callindex, Run);
        }
        public void Detatch(int callindex)
        {
            CConsole.CommandHandler.Remove(Run);
        }

        private static Dictionary<string, PropertyInfo> getPropertys(Type type, BindingFlags flags)
        {
            Dictionary<string, PropertyInfo> FieldInfos = new Dictionary<string, PropertyInfo>();

            Type[] acceptedTypes = ReflectedPackage.acceptedTypes;
            
            var propertys = from property in type.GetProperties(flags)
                            where property.GetCustomAttributes().Any((a) => a is CPropertyAttribute) &&
                            acceptedTypes.Any((t) => t == property.PropertyType)
                            select property;

            //propertys.Print(); // DEGUG!!

            foreach (var p in propertys)
            {
                string name = ((CPropertyAttribute)p.GetCustomAttribute(typeof(CPropertyAttribute))).FieldName;

                if (name != null)
                {
                    FieldInfos.Add(name, p);
                }
                else
                {
                    FieldInfos.Add(p.Name, p);
                }
            }
            return FieldInfos;

        }

        public static PropertyPackage GetObjectPropertys(object Object, string objectName, bool Global = false)
        {
            PropertyPackage cp = new PropertyPackage();
            cp.global = Global;
            cp.PropertyInfos = getPropertys(Object.GetType(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            cp.objectName = objectName;
            cp.obj = Object;
            return cp;
        }
        public static PropertyPackage GetStaticPropertys(Type t, string tag, bool Global = false)
        {
            PropertyPackage cp = new PropertyPackage();
            cp.global = Global;
            cp.PropertyInfos = getPropertys(t, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            cp.Static = true;
            cp.staticTag = tag;
            return cp;
        }
    }
}
