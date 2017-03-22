using Czaplicki.Universal.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Czaplicki.Universal.Console
{
    public class CommandEventArgs : EventArgs
    {
        public string Command;
        public string[] Commands
        {
            get
            {
                return Split(Command);
            }
            set
            {
                Command = Concatinate(value);
            }
        }

        private string Concatinate(string[] value)
        {
            string s = "";
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].Contains(" "))
                {
                    s += "\"" + value[i] + "\" ";
                }
                else
                {
                    s += value[i] + " ";
                }
            }
            s = s.Substring(0, s.Length - 1);
            return s;
        }

        private string[] Split(string command)
        {
            List<string> list = new List<string>();
            string word = "";
            bool inString = false;
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == ' ' && !inString)
                {
                    list.Add(word);
                    word = "";
                    continue;
                }
                if (Command[i] == '\\' && i + 1 < command.Length)
                {
                    if (command[i + 1] == '"')
                    {
                        word += '"';
                        i++;
                        continue;
                    }
                }
                if (command[i] == '"')
                {
                    inString = inString ? false : true;
                    continue;
                }
                word += command[i];
            }
            list.Add(word);
            return list.ToArray();
        }
    }
    public static class CConsole
    {
        public static readonly Collections.SortedList<int, Func<CommandEventArgs, bool>> CommandHandler = new Collections.SortedList<int, Func<CommandEventArgs, bool>>();
        public static void Execute(string command)
        {
            CommandEventArgs args = new CommandEventArgs() { Command = command };
            foreach (var handler in CommandHandler)
            {
                if (handler.Invoke(args))
                    break;
            }
        }

        public static void Logg(string text)
        {
            LoggEvent?.Invoke(text);
        }
        public static event Action<string> LoggEvent;

        public static void Error(string text)
        {
            ErrorEvent?.Invoke(text);
        }
        public static event Action<string> ErrorEvent;

        private static string @out;
        public static string Out
        {
            get
            {
                return @out;
            }
            set
            {
                NewOutEvent?.Invoke(@out); @out = value;
            }
        }
        public static event Action<string> NewOutEvent;
    }
}
