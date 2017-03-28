using System.Collections.Generic;
using System.Linq;

namespace Paint
{
    public static class ListExtensions
    {
        public static void RemoveLast<T>(this List<T> list)
        {
            if (list.Any())
                list.RemoveAt(list.Count - 1);
        }
    }
}