using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Collections
{

    public sealed class SortedList<T, I> : IEnumerable<I> where T : IComparable
    {
        private List<T> prio = new List<T>();
        private List<I> values = new List<I>();
        public I this[int index]
        {
            get { return values[index]; }
        }

        public void Add(T key, I value)
        {
            prio.Add(key);
            values.Add(value);
            int count = prio.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i != j)
                    {
                        if (prio[i].CompareTo(prio[j]) > 0)
                        {
                            T temp = prio[i];
                            prio[i] = prio[j];
                            prio[j] = temp;

                            I tempv = values[i];
                            values[i] = values[j];
                            values[j] = tempv;
                        }

                    }
                }
            }
        }

        public void Remove(I value)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].Equals(value))
                {
                    RemoveAt(i);
                }
            }
        }
        public void RemoveAt(int index)
        {
            prio.RemoveAt(index);
            values.RemoveAt(index);
        }

        public T GreatestKey()
        {
            return prio[0];
        }

        public IEnumerator<I> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }


    }
}
