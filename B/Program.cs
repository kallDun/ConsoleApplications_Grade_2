using B.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            var triangle_1 = new TTriangle(5, 6, 7);
            Console.WriteLine(triangle_1.GetPerimeter());
            Console.WriteLine(triangle_1.GetSquare());
        }
    }
}
