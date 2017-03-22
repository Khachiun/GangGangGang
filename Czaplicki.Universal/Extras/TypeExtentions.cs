using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extras
{
    public static class TypeExtentions
    {
        public static bool IsOrDerived(this System.Type left, System.Type right)
        {
            return left == right || left.IsSubclassOf(right);
        }
    }
}
