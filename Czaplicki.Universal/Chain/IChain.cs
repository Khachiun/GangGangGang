using System.Collections.Generic;

namespace Czaplicki.Universal.Chain
{
    public interface IChain<T>
    {
        T GetNext();
        bool LeaveChain();
        T InsertElement(T item);
    }
}
