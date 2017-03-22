using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Debug
{
    public static class Extentions
    {
        public static void Print<T>(this T obj)
        {
            System.Console.WriteLine(obj.ToString());
        }
        public static void Print<T>(this IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
