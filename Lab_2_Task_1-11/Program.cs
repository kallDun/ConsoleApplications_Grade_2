using Lab__Task_1_11.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab__Task_1_11
{
    class Program
    {
        static void Main(string[] args)
        {
            Equation equation_1 = new Equation(8, 4, 4, 5, 5, 5);

            Console.WriteLine(equation_1.solutions);
            Console.WriteLine(equation_1.IsSolution(0.5));
            Console.WriteLine(equation_1.IsSolution(3));
        }
    }
}
