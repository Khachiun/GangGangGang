using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extentions
{
    public static class Strings
    {
        public static string Format(this string s, params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(s, args);
            return sb.ToString();
        }
    }
}   
