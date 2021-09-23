using Lab_2_Matrix.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMatrix<int> A = new MyMatrix<int>(new int[,] 
            { 
                { 1, 2, 3 }, 
                { 4, 5, 6 },
            });
            MyMatrix<int> B = new MyMatrix<int>(new int[,] 
            { 
                { 7, 8 }, 
                { 9, 1 },
                { 2, 3 },
            });
            MyMatrix<int> C = new MyMatrix<int>(new int[,]
            {
                { 7, 8, 9 },
                { 1, 2, 3 },
            });
            Console.WriteLine($"Matrix A: \n{A}\nMatrix B: \n{B}\nMatrix C: \n{C}\n");

            var plus = A + C;
            var multiply = A * B;
            var transponed_A = A.GetTransopedCopy();
            
            Console.WriteLine("A + C = \n" + plus);
            Console.WriteLine("A * B = \n" + multiply);
            Console.WriteLine("A transponed = \n" + transponed_A);
        }
    }
}
