using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Casings
{
    public class SynchronizedObject<T>
    {
        private T value;
        public T Value
        {
            get
            {
                lock (value)
                {
                    return value;
                }
            }
            set
            {
                lock (this.value)
                {
                    this.value = value;
                }
            }
        }
        public SynchronizedObject(T value)
        {
            this.value = value;
        }


        public static explicit operator T(SynchronizedObject<T> so)
        {
            return so.Value;
        }
        public static explicit operator SynchronizedObject<T>(T t)
        {
            return new SynchronizedObject<T>(t);
        }
    }
}
