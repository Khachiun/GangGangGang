using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extentions
{
    public static class Array
    {
        public static T[] SubArray<T>(this T[] array, int startIndex)
        {
            T[] newArray = new T[array.Length - startIndex];
            for (int i = startIndex; i < array.Length; i++)
            {
                newArray[i - startIndex] = array[i];
            }
            return newArray;
        }
        public static T[] SubArray<T>(this T[] array, int startIndex, int length) // broken?
        {
            T[] newArray = new T[length];
            for (int i = startIndex; i < startIndex + length; i++)
            {
                newArray[i - startIndex] = array[i];
            }
            return newArray;
        }

        

        /// <summary>
        /// Adds Item to first avalible spot in array
        /// 
        /// Avalible spot: spot with value null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <returns>index where item was placed</returns>
        public static int Add<T>(this T[] array, T item) where T : class
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                    continue;
                array[i] = item;
                return i;
            }
            throw new Exception("No spot avalible!");
        }

        /// <summary>
        /// Adds Items to first avalible spots in array
        /// 
        /// Avalible spot: spot with value null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <returns>index where item was placed</returns>
        public static int[] AddRange<T>(this T[] array, params T[] items) where T : class
        {
            int itemsToAdd = items.Length;
            int nextToAdd = 0;

            List<int> indexes = new List<int>(itemsToAdd);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                    continue;
                indexes.Add(i);
                array[i] = items[nextToAdd];
                nextToAdd++;

                if (nextToAdd >= itemsToAdd)
                    return indexes.ToArray();
            }
            if (items.Length < 1)
                throw new Exception("No items in!");
            throw new Exception("Not enough avalible spots!");
        }

        /// <summary>
        /// Trys to add Item to first avalible spot in array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index">index where item was placed</param>
        /// <param name="item"></param>
        /// <returns>if successfully added item</returns>
        public static bool TryAdd<T>(this T[] array, T item, out int index) where T : class
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                    continue;
                array[i] = item;
                index = i;
                return true;
            }
            index = array.Length - 1;
            return false;
        }

        /// <summary>
        /// Trys to add Items to first avalible spots in array
        /// doesn't touch the array until done, aka if it fails no data will be changed
        /// Avalible spot: spot with value null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <returns>index where item was placed</returns>
        public static bool TryAddRange<T>(this T[] array, out int[] indexes, params T[] items) where T : class
        {
            int itemsToAdd = items.Length;
            int nextToAdd = 0;

            List<int> indexesBuffer = new List<int>(itemsToAdd);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                    continue;
                indexesBuffer.Add(i);
                nextToAdd++;
                if (nextToAdd >= itemsToAdd)
                {
                    indexes = indexesBuffer.ToArray();
                    for (int j = 0; j < itemsToAdd; j++)
                    {
                        array[indexesBuffer[j]] = items[j];
                    }
                    return true;
                }

            }
            indexes = null;
            return false;
        }

        public static T[] DynamicAdd<T>(this T[] Array, T item, out int index) where T : class
        {
            if (Array.TryAdd(item, out index))
                return Array;

            var newArray = new T[Array.Length + 1];
            for (int i = 0; i < Array.Length; i++)
            {
                newArray[i] = Array[i]; 
            }
            newArray[Array.Length] = item;
            index = Array.Length;
            return newArray;
        }

        public static bool TrySet<T>(this T[] array, int index, T item)
        {
            if (index < 0 || index >= array.GetLength(0))
                return false;
            array[index] = item;
            return true;
        }
        public static bool TryGet<T>(this T[] array, int index, out T item)
        {
            if (index < 0 || index > array.GetLength(0))
            {
                item = default(T);
                return false;
            }
            item = array[index];
            return true;
        }

        public static bool TrySet<T>(this T[,] array, int index0, int index1, T item)
        {
            if (index0 < 0 || index0 > array.GetLength(0) ||
                index1 < 0 || index1 > array.GetLength(1))
                return false;

            array[index0, index1] = item;
            return true;
        }
        public static bool TryGet<T>(this T[,] array, int index0, int index1, out T item)
        {
            if (index0 < 0 || index0 > array.GetLength(0) - 1 ||
                index1 < 0 || index1 > array.GetLength(1) - 1)
            {
                item = default(T);
                return false;
            }

            item = array[index0, index1];
            return true;
        }

       

    }
}
