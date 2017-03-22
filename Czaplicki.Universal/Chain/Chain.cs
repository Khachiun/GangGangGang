using System.Collections.Generic;
using System.Collections;

namespace Czaplicki.Universal.Chain
{
    public class Chain : IChain<Chain>, IEnumerable<Chain>
    {
        public static Chain Previus(Chain chain)
        {
            Chain temp = chain.nextChainPiece;
            while (temp.nextChainPiece != chain)
            {
                temp = temp.nextChainPiece;
            }
            return temp;
        }

        public static void Join(Chain first, Chain secound)
        {
            Chain pre = Previus(secound);
            Chain fn = first.nextChainPiece;
            first.nextChainPiece = secound;
            pre.nextChainPiece = fn;

        }

        public Chain()
        {
            nextChainPiece = this;
        }

        private Chain nextChainPiece;

        
        public Chain InsertElement(Chain item)
        {
            item.nextChainPiece = nextChainPiece;
            nextChainPiece = item;
            return item;
        }


        public Chain GetNext()
        {
            return nextChainPiece;
        }

        /// <summary>
        /// Removes chain piece
        /// </summary>
        /// <returns>is last one in chain</returns>
        public bool LeaveChain()
        {
            Chain temp = this.nextChainPiece;
            while (temp.nextChainPiece != this)
            {
                temp = temp.nextChainPiece;
            }
            if (temp == this)
            {
                return true;
            }
            temp.nextChainPiece = this.nextChainPiece;
            this.nextChainPiece = this;
            return false;            
        }

        public IEnumerator<Chain> GetEnumerator()
        {
            return new ChainEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ChainEnumerator(this);
        }
    }
}
