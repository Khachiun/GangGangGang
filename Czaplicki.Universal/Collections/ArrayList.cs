using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Collections
{
    public class Arraylist<T>
    {
        List<T> list = new List<T>();
        int currentIndex = -1;
        public T Current { get { return list[currentIndex]; } set { list[currentIndex] = value; } }
        //public T First { get; private set; }
        public T Next(T item)
        {
            list.Add(item);
            currentIndex++;
            Current = item;
            return item;

        }
        public T[] ToArray()
        {
            return list.ToArray();
        }
        public T this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }
    }
}
