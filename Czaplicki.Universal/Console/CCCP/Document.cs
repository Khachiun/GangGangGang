using Czaplicki.Universal.Collections;
using Czaplicki.Universal.Console;
using Czaplicki.Universal.Extentions;
using Czaplicki.Universal.IO;
using System;

namespace Czaplicki.Universal.CCCP
{
    public static class Document
    {
        public static void Attatch()
        {
            Console.CConsole.CommandHandler.Add(int.MaxValue, Del);
        }
        public static void Detatch()
        {
            Console.CConsole.CommandHandler.Remove(Del);
        }

        private static Func<CommandEventArgs, bool> Del;

        private static Que<string> doc = new Que<string>();
        private static string multiLineEnding;
        private static bool multiLineReadingActive;

        static Document()
        {
            Del = (e) =>
            {
                if (multiLineReadingActive)
                {
                    if (e.Command == multiLineEnding)
                    {
                        multiLineReadingActive = false;
                    }
                    else
                    {
                        doc.Append(e.Command);
                    }
                    return true;
                }

                string[] args = e.Commands;
                if (args[0].StartsWith("doc:"))
                {
                    args[0] = args[0].Substring(4);

                    if (args.Length == 3 && args[0] == "write" && args[1] == ">>")
                    {
                        multiLineEnding = args[2];
                        multiLineReadingActive = true;
                        return true;
                    }


                    if (args.Length == 2)
                    {
                        if (args[0] == "load")
                        {
                            string[] lines;
                            if (TXT.LoadLines(args[1], out lines))
                            {
                                for (int i = 0; i < lines.Length; i++)
                                    doc.Append(lines[i]);
                                return true;
                            }
                            Console.CConsole.Logg("Invalid Path");
                            return true;
                        }
                        if (args[0] == "save")
                        {
                            TXT.SaveLines(args[1], doc.ToArray());
                            return true;
                        }
                    }
                    if (args[0] == "execute")
                    {
                        foreach (var line in doc)
                        {
                            Console.CConsole.Execute(line);
                        }
                        return true;
                    }
                    if (args[0] == "clear")
                    {
                        doc.Clear();
                        return true;
                    }
                    if (args[0] == "echo")
                    {
                        foreach (var item in doc)
                        {
                            Console.CConsole.Logg(item);
                        }
                        return true;
                    }
                }
                return false;
            };
        }
    }
}
