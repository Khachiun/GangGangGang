using Czaplicki.Universal.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.CCCP
{
    public static class Define
    {
        public static void Attatch(int CallIndexDefinitions, int CallIndexNewDefinition)
        {
            Console.CConsole.CommandHandler.Add(CallIndexDefinitions, Act);
            Console.CConsole.CommandHandler.Add(CallIndexNewDefinition, NewDefinition);
        }
        public static void Detatch()
        {
            Console.CConsole.CommandHandler.Remove(Act);
            Console.CConsole.CommandHandler.Remove(NewDefinition);
        }

        static Func<CommandEventArgs, bool> NewDefinition;
        static Func<CommandEventArgs, bool> Act;

        static Dictionary<string, string> Definitions = new Dictionary<string, string>();

        static Define()
        {
            Act = (e) =>
            {
                string[] args = e.Commands;

                for (int i = 0; i < args.Length; i++)
                {
                    if (Definitions.ContainsKey(args[i]))
                    {
                        args[i] = Definitions[args[i]];
                        e.Commands = args;
                    }
                }
                return false;
            };
            NewDefinition = (e) =>
            {
                string[] args = e.Commands;
                if (args[0] == "define")
                    if (args.Length == 3)
                    {
                        if (Definitions.ContainsKey(args[1]))
                        {
                            Definitions[args[1]] = args[2];
                        }
                        Definitions.Add(args[1], args[2]);
                        return true;
                    }
                    else if (args.Length == 2)
                    {
                        if (args[1] == "help")
                        {
                            Console.CConsole.Logg("Format: Define OldString NewString");
                            return true;
                        }
                    }
                    else
                    {
                        Console.CConsole.Logg("Invaild Args for Define Command");
                        return true;
                    }

                if (args[0] == "defined")
                {
                    foreach (var item in Definitions)
                    {
                        Console.CConsole.Logg(item.ToString());
                    }
                }
                return false;
            };
        }
    }
}
