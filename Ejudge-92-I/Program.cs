using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ejudge_92_I
{
    class Program
    {
        static int INFINITY => 1000000000;
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var arr = input[1].Split().Select(int.Parse).ToArray();
            var N = int.Parse(input[2]);
            Console.WriteLine(GetMinCountOfCash(arr, N));
        }
        private static string GetMinCountOfCash(int[] arr, int cash)
        {
            int[] F = new int[cash + 1];
            F[0] = 0;
            for (int i = 1; i <= cash; i++)
            {
                F[i] = INFINITY;
                for (int j = 0; j < arr.Length; j++)
                {
                    if (i >= arr[j] && F[i - arr[j]] + 1 < F[i])
                    {
                        F[i] = F[i - arr[j]] + 1;
                    }
                }
            }
            var impossibility = F[cash] >= INFINITY;
            if (impossibility) return "No solution";

            List<int> result = new List<int>();
            while (cash > 0)
            {
                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    if (cash >= arr[i] && F[cash - arr[i]] == F[cash] - 1)
                    {
                        result.Add(arr[i]);
                        cash -= arr[i];
                        break;
                    }
                }
            }
            return string.Join(" ", result);
        }
    }
}