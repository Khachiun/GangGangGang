using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Collections
{
    public class TableArray<T> : IEnumerator<T>, IEnumerable<T>
    {
        public T[] table { get; private set; }
        private int width, height, count = -1;


        public int Width { get { return width; } }
        public int Height { get { return height; } }


        public TableArray(int width, int height)
        {
            table = new T[width * height];
            this.width = width; this.height = height;
        }
        public T this[int x, int y]
        {
            get { return table[(height * x) + y]; }
            set { table[(height * x) + y] = value; }
        }
        public T this[int index]
        {
            get { return table[index]; }
            set { table[index] = value; }
        }
        public int XOfIndex(int index)
        {
            return (int)(index / height);
        }
        public int YOfIndex(int index)
        {
            return index - (XOfIndex(index) * height);
        }

        //public void Add(T value)
        //{
        //    table[count] = value;
        //    MoveNext();

        //}

        public T Current
        {
            get
            {
                return table[count];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return table[count];
            }
        }

        void IDisposable.Dispose()
        {
            count = -1;
        }

        public bool MoveNext()
        {
            count++;
            return count < table.Length;
        }

        public void Reset()
        {
            count = -1;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }



        public static implicit operator T[] (TableArray<T> table)
        {
            return table.table;
        }
        public static implicit operator T[,] (TableArray<T> table)
        {
            T[,] array = new T[table.width, table.height];
            for (int i = 0; i < table.Count(); i++)
            {
                array[table.XOfIndex(i), table.YOfIndex(i)] = table[i];
            }
            return array;
        }
        public static implicit operator TableArray<T>(T[,] table)
        {
            TableArray<T> t = new TableArray<T>(table.GetLength(0), table.GetLength(1));
            for (int x = 0; x < table.GetLength(0); x++)
                for (int y = 0; y < table.GetLength(1); y++)
                    t[x, y] = table[x, y];
            return t;
        }

        //public static implicit operator MapTable<T> (T[] table)
        //{
        //    return 
        //}


    }
}
