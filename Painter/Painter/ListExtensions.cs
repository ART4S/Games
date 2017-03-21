using System.Collections.Generic;
using System.Linq;

namespace Painter
{
    public static class ListExtensions
    {
        public static void RemoveLast<Type>(this List<Type> list)
        {
            if (!list.Any())
                return;

            list.RemoveAt(list.Count - 1);
        }
    }
}