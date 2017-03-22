using Czaplicki.Universal.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Console
{

    public class FieldPackage
    {
        bool global;

        string objectName;
        object obj;

        string staticTag;
        bool Static;


        Dictionary<string, FieldInfo> FieldInfos = new Dictionary<string, FieldInfo>();

        private bool iterate(CommandEventArgs e)
        {
            var args = e.Commands;
            foreach (var kvp in FieldInfos)
            {
                if (kvp.Key == args[0])
                {
                    if (args.Length == 3 && args[1] == "=")
                    {
                        var type = kvp.Value.FieldType;
                        object Invokevalue = null;
                        bool validType = true;
                        #region region Arg convertion
                        try
                        {

                            if (type == typeof(bool)) { Invokevalue = Convert.ToBoolean(args[2]); }
                            else if (type == typeof(int)) { Invokevalue = Convert.ToInt32(args[2]); }
                            else if (type == typeof(float))
                            {
                                string s = args[2].Replace('.', ',');
                                Invokevalue = Convert.ToSingle(s);
                            }
                            else if (type == typeof(char) && args[2].Length == 1) { Invokevalue = Convert.ToChar(args[2]); }
                            else if (type == typeof(string)) { Invokevalue = args[2]; }
                            else
                            {
                                validType = false;
                                break;
                            }
                        }
                        #endregion
                        catch (Exception) { validType = false; break; } // if fails : invalid indata or not correct overload method
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


        private bool Run(CommandEventArgs e)
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

        private static Dictionary<string, FieldInfo> GetFields(Type type, BindingFlags flags)
        {
            Dictionary<string, FieldInfo> FieldInfos = new Dictionary<string, FieldInfo>();

            Type[] acceptedTypes = ReflectedPackage.acceptedTypes;

            var Fields = from field in type.GetFields(flags)
                         where field.GetCustomAttributes().Any((a) => a is CFieldAttribute) &&
                         acceptedTypes.Any((t) => t == field.FieldType)
                         select field;

            //Fields.Print(); // DEGUG!!

            foreach (var f in Fields)
            {
                string name = ((CFieldAttribute)f.GetCustomAttribute(typeof(CFieldAttribute))).FieldName;

                if (name != null)
                {
                    FieldInfos.Add(name, f);
                }
                else
                {
                    FieldInfos.Add(f.Name, f);
                }
            }
            return FieldInfos;

        }

        public static FieldPackage GetObjectFields(object Object, string objectName, bool Global = false)
        {
            FieldPackage cp = new FieldPackage();
            cp.global = Global;
            cp.FieldInfos = GetFields(Object.GetType(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            cp.objectName = objectName;
            cp.obj = Object;
            return cp;
        }
        public static FieldPackage GetStaticFields(Type t, string tag, bool Global = false)
        {
            FieldPackage cp = new FieldPackage();
            cp.global = Global;
            cp.FieldInfos = GetFields(t, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            cp.Static = true;
            cp.staticTag = tag;
            return cp;
        }

    }

}
