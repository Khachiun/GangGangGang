using System;
using System.Collections;
using System.Collections.Generic;

namespace Czaplicki.Universal.Chain
{
    //class MyClass<T> : IChain<T> where T : MyClass<T> , IChain<T>
    //{
    //    T next;

    //    public T GetNext()
    //    {
    //        return next;
    //    }

    //    public T InsertElement(T item)
    //    {
    //        var mynext = next;
    //        next = item;
    //        item.next = mynext;
    //        return item;
    //    }

    //    public bool LeaveChain()
    //    {
    //        var temp = this.next;
    //        while (temp.next != this)
    //        {
    //            temp = temp.next;
    //        }
    //        if (temp == this)
    //        {
    //            return true;
    //        }
    //        temp.next = this.next;
    //        this.next = this;
    //        return false;
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }
    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}



    public abstract class Chain<T> : IChain<T>, IEnumerable<Chain<T>> where T : Chain<T>
    {
        private T nextChainPiece;

        public Chain()
        {
            nextChainPiece = (T)this;
        }

        public T GetNext()
        {
            return nextChainPiece;
        }

        public T InsertElement(T item)
        {
            item.nextChainPiece = nextChainPiece;
            nextChainPiece = item;
            return item;
        }

        public bool LeaveChain()
        {
            Chain<T> temp = this.nextChainPiece;
            while (temp.nextChainPiece != this)
            {
                temp = temp.nextChainPiece;
            }
            if (temp == this)
            {
                return false;
            }
            temp.nextChainPiece = this.nextChainPiece;
            this.nextChainPiece = (T)this;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ChainEnumerator<T>(this);
        }

        IEnumerator<Chain<T>> IEnumerable<Chain<T>>.GetEnumerator()
        {
            return new ChainEnumerator<T>(this);
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return new ChainEnumerator<T>(this);
        //}

        //public IEnumerator<T> GetEnumerator()
        //{
        //    return new ChainEnumerator<T>(this);
        //}

        //public static Chain<T> operator +(Chain<T> left, Chain<T> right)
        //{
        //    left.InsertElement(right);
        //    return left;
        //}

        //public static explicit operator T(Chain<T> chain)
        //{
        //    return chain;
        //}
    }
}
