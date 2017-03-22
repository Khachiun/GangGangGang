using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extentions
{
    public static class IEnumerableExtentions
    {

        public static string AllToString<T>(this IEnumerable<T> enumerable)
        {
            string s = "";
            foreach (var item in enumerable)
            {
                s += item.ToString() + " ";
            }
            return s;
        }        
        public static T[] GetSegment<T>(this List<T> list, int startIndex, int length)
        {

            T[] newArray = new T[length];
            for (int i = startIndex; i < startIndex + length; i++)
            {
                if (i >= list.Count || i < 0)
                    newArray[i - startIndex] = default(T);
                else
                    newArray[i - startIndex]
                        = list[i];
            }
            return newArray;
        }
        public static void Print<T>(this IEnumerable<T> enumerable, ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
            foreach (var item in enumerable)
            {
                System.Console.WriteLine(item);
            }
            System.Console.ResetColor();
        }
        public static IEnumerable<T> InnerElements<T>(this IEnumerable<IEnumerable<T>> E)
        {
            List<T> Final = new List<T>();
            foreach (var itemCollection in E)
            {
                Final.AddRange(itemCollection);
            }
            return Final;
        }
        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> E, params IEnumerable<T>[] args)
        {
            List<T> Final = E.ToList();
            for (int i = 0; i < args.Length; i++)
            {
                Final.AddRange(args[i]);
            }
            return Final;
        }
        /// <summary>
        /// If any of first enmerable is any of the other enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool Any<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            foreach (var item in enumerable)
            {
                if (other.Any((e) => e.Equals(item)))
                    return true;

            }
            return false;
        }
        /// <summary>
        /// If all of the first enumerable is any of the other enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool AllOfAny<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            foreach (var item in enumerable)
            {
                if (other.Any((e) => e.Equals(item)))
                    continue;
                return false;
            }
            return true;
        }


        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            return list.ElementAt(new Random().Next(0, list.Count()));
        }
    }
}
