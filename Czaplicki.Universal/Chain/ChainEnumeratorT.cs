using System.Collections;
using System.Collections.Generic;

namespace Czaplicki.Universal.Chain
{
    public class ChainEnumerator<T> : IEnumerator<Chain<T>> where T : Chain<T>
    {
        public ChainEnumerator(Chain<T> chain)
        {
            chainStart = chain;
            current = chain;
            firstIteration = true;
        }
        Chain<T> chainStart;
        Chain<T> current;
        bool firstIteration;

        public Chain<T> Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return current;
            }
        }


        //really used so left not implemented
        public void Dispose()
        {
            chainStart = null;
            current = null;
        }

        public bool MoveNext()
        {
            if (firstIteration)
            {
                firstIteration = false;
                return true;
            }
            current = current.GetNext();
            return current != chainStart;
        }

        public void Reset()
        {
            var last = current;
            do
            {
                last = current;
                current = current.GetNext();
            }
            while (MoveNext());
            current = last;
        }
    }
}
