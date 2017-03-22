using Czaplicki.Universal.Console;
using Czaplicki.Universal.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Console
{

    public class CommandPackage : ReflectedPackage
    {
        private bool global;

        private object Object;
        private string ObjectName;

        private bool Static;
        private string StaticTag;

        private List<MethodInfo> methods;

        private CommandPackage() { }

        private bool Check(CommandEventArgs e, out CommandEventArgs owne)
        {
            if (global)
            {
                owne = new CommandEventArgs { Command = e.Command };
                return true;
            }

            if (Static && e.Command.StartsWith(StaticTag + ":"))
            {
                owne = new CommandEventArgs() { Command = e.Command.Substring(StaticTag.Length + 1) };
                return true;
            }

            if (!Static && e.Command.StartsWith(ObjectName + "."))
            {
                owne = new CommandEventArgs() { Command = e.Command.Substring(ObjectName.Length + 1) };
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
                return Iterate(owne);
            }
            return false;
        }
        private bool Iterate(CommandEventArgs e)
        {
            string Command = e.Commands[0];
            string[] args = e.Commands.SubArray(1);

            foreach (var method in methods)
            {
                //gets Attribute
                var attribute = (CCommandAttribute)method.GetCustomAttribute(typeof(CCommandAttribute));

                //Looks if method name is right

                if (attribute.CommandName != Command)
                    continue;

                var parameters = method.GetParameters();
                //Looks so that the Argument in Matches in length
                if (!(parameters.Length == args.Length))
                    continue;
                //Container for args to invoke
                object[] InvokeArgs = new object[parameters.Length];

                //Valid check
                bool ValidParameters = true;

                for (int i = 0; i < parameters.Length && ValidParameters; i++)
                {
                    object obj;
                    if (!(ValidParameters = TryConvert(parameters[i].ParameterType, args[i], out obj)))
                        break;
                    InvokeArgs[i] = obj;
                    
                }
                if (ValidParameters)
                {
                    object rValue;
                    if (Static)
                    {
                        //invokes method
                        rValue = method.Invoke(null, InvokeArgs);
                    }
                    else
                    {
                        //invokes method
                        rValue = method.Invoke(Object, InvokeArgs);
                    }

                    if (rValue != null) // AND Valuse is of accepted types 
                    {
                        CConsole.Out = rValue.ToString();
                    }
                    return true;
                }
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


        private static List<MethodInfo> GetMethods(Type type, BindingFlags flags)
        {
            List<MethodInfo> unSortedList = new List<MethodInfo>();
            Type[] acceptedTypes = ReflectedPackage.acceptedTypes;
            var methods = type.GetMethods(flags);
            foreach (var method in methods)
            {
                if (!method.GetCustomAttributes().Any((a) => a is CCommandAttribute))
                    continue;

                var parameters = method.GetParameters();
                IEnumerable<Type> parameterTypes = parameters.Select((p) => p.ParameterType);

                if (parameterTypes.AllOfAny(acceptedTypes))
                {
                    unSortedList.Add(method);
                }

                unSortedList.Sort((item1, item2) =>
                {

                    var parameters1 = item1.GetParameters();
                    var parameters2 = item2.GetParameters();


                    if (parameters1.Length != parameters2.Length)
                        return parameters1.Length - parameters2.Length;

                    int points1 = 0, points2 = 0;

                    for (int i = 0; i < parameters1.Length; i++)
                    {
                        for (int p = 0; p < acceptedTypes.Length; p++)
                        {
                            if (parameters1[i].ParameterType == acceptedTypes[p])
                                points1 += p;

                            if (parameters2[i].ParameterType == acceptedTypes[p])
                                points2 += p;
                        }
                    }

                    return points1 - points2;
                });
            }
            return unSortedList;
        }

        public static CommandPackage GetObjectCommands(object Object, string objectName, bool Global = false)
        {
            CommandPackage cp = new CommandPackage();
            cp.global = Global;
            cp.methods = GetMethods(Object.GetType(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            cp.ObjectName = objectName;
            cp.Object = Object;
            return cp;
        }
        public static CommandPackage GetStaticCommands(Type t, string tag, bool Global = false)
        {
            CommandPackage cp = new CommandPackage();
            cp.global = Global;
            cp.methods = GetMethods(t, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            cp.Static = true;
            cp.StaticTag = tag;
            return cp;
        }
    }



}
