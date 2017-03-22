using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Collections
{
    class Que<T> : IEnumerable<T>
    {
        List<T> list = new List<T>();

        public void Append(T t)  => list.Add(t);
        public void Prepend(T t) => list.Insert(0, t);
        public void Insert(int index, T t) => list.Insert(index, t);
        public void Remove(T t) => list.Remove(t);
        public void RemoveAt(int index) => list.RemoveAt(index);
        public void Clear() => list.Clear();
        public T[] ToArray() => list.ToArray();

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
