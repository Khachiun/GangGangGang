using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Czaplicki.Universal.IO
{
    public static class TXT
    {
        public static bool Load(string path, out string value)
        {
            if (File.Exists(path))
            {
                using (System.IO.StringReader sr = new StringReader(path))
                {
                    value = sr.ReadToEnd();
                    return true;
                }
            }
            value = null;
            return false;
        }
        public static bool LoadLines(string path, out string[] lines)
        {
            if (File.Exists(path))
            {
                IEnumerable<string> readLines =  File.ReadLines(path);
                lines = readLines.ToArray();
                return true;
            }
            lines = null;
            return false;
        }
        public static bool Save(string path, string value)
        {
            FileStream stream = File.Open(path, FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.Write(value);
                return true;
            }
        }
        public static bool SaveLines(string path, params string[] lines)
        {
            FileStream stream = File.Open(path, FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(stream))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sw.WriteLine(lines[i]);
                }
                return true;
            }
        }
    }
}
