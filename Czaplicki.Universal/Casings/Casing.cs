using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Casings
{
    public class Casing<T>
    {
        public T value;
        private Casing()
        {
            //for serilazation
        }
        public Casing(T t)
        {
            this.value = t;
        }

        public override string ToString()
        {
            return value.ToString();
        }
        public static explicit operator Casing<T>(T t)
        {
            return new Casing<T>(t);
        }
        public static Casing<T>[] Case(T[] ts)
        {
            var ca = new Casing<T>[ts.Length];
            for (int i = 0; i < ts.Length; i++)
            {
                ca[i] = new Casing<T>(ts[i]);
            }
            return ca;
        }
    }
}
