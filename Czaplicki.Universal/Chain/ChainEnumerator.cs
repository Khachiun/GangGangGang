using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Chain
{
    public class ChainEnumerator : IEnumerator<Chain>
    {
        public ChainEnumerator(Chain chain)
        {
            chainStart = chain;
            current = chain;
            firstIteration = true;
        }
        Chain chainStart;
        Chain current;
        bool firstIteration;

        public Chain Current
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
            Chain last = current;
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
