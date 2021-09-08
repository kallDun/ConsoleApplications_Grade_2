using System;
using System.Linq;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var (height, width) = (data[0], data[1]);
            bool[][] B = new bool[height][];

            for (int i = 0; i < height; i++)
            {
                var arr_data = Console.ReadLine().Split(' ').Select(byte.Parse).ToList();
                if (arr_data.Count != width) throw new Exception("Invalid input data!");
                B[i] = arr_data.Select(x => x == 1).ToArray();
            }

            int[] P = B.Select(x => x.Where(y => !y).Count()).ToArray();
            Console.WriteLine("Count of zeros:");
            P.Print();

            int[][] Z = new int[height][];
            for (int i = 0; i < height; i++)
            {
                Z[i] = new int[P[i]];
                for (int j = 0; j < P[i]; j++)
                {
                    Z[i][j] = B[i].ToList().FindIndex(y => !y);
                    B[i][Z[i][j]] = true;
                }
            }
            Console.WriteLine("Z Matrix:");
            Z.Print();

            Z = Z.Select(x => x.Reverse().ToArray()).ToArray();
            Console.WriteLine("Z Matrix inverted:");
            Z.Print();
        }
    }
}
