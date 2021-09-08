using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A
{
    public static class Extension
    {
        public static void Print<T>(this T[][] arr)
        {
            Console.WriteLine(string.Join("\n", arr.Select(x => string.Join(" ", x))));
        }
        public static void Print<T>(this T[] arr)
        {
            Console.WriteLine(string.Join(" ", arr));
        }
    }
}
