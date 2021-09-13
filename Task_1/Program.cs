using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var arr = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (arr_max, arr_max_index) = GetMaxOfArr(arr);

            if (arr.Length == 1) 
            {
                Console.WriteLine($"{arr[0]}\n{0}");
                return;
            }

            if (arr.Length == 2)
            {
                Console.WriteLine($"{arr_max}\n{1}\n{(arr_max_index == 0 ? 2 : 1)}");
                return;
            }

            GetMaxOfRow(arr, out int? max, out int? index_ofMax, out int itters);
            
            if (arr_max < max)
            {
                Console.WriteLine(max);
                RowCondition(arr, index_ofMax, itters);
            }
            else
            {
                Console.WriteLine(arr_max);
                PlainCondition(arr, arr_max_index);
            }
        }

        private static void RowCondition(int[] arr, int? index_ofMax, int itters)
        {
            List<int> operations = new List<int>();
            for (int i = 0; i < index_ofMax; i++) operations.Add(1);
            for (int i = arr.Length - operations.Count; i > itters * 2 + 1; i--) operations.Add(i);
            for (int i = 0; i < itters; i++) operations.Add(2);

            Console.WriteLine(operations.Count);
            Console.WriteLine(string.Join("\n", operations));
        }

        private static void PlainCondition(int[] arr, int arr_max_index)
        {
            List<int> operations = new List<int>();
            for (int i = 0; i < arr_max_index; i++) operations.Add(1);
            for (int i = arr.Length - operations.Count; i > 1; i++) operations.Add(i);

            Console.WriteLine(operations.Count);
            Console.WriteLine(string.Join("\n", operations));
        }

        static void GetMaxOfRow(int[] arr, out int? max, out int? index_ofMax, out int itters)
        {
            max = null;
            index_ofMax = null;
            itters = 1;

            for (int i = 1; i <= (arr.Length - 1) / 2; i++)
            {
                for (int j = 0; j < arr.Length - (i * 2); j++)
                {
                    int local_max = 0;
                    for (int k = 0; k < 1 + (2 * i); k += 2)
                    {
                        local_max += arr[k + j];
                    }
                    if (max is null || max < local_max)
                    {
                        max = local_max;
                        index_ofMax = j;
                        itters = i;
                    }
                }
            }
        }

        static (int, int) GetMaxOfArr(int[] arr)
        {
            int max = arr[0], index = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (max < arr[i])
                {
                    max = arr[i];
                    index = i;
                }
            }
            return (max, index);
        }

    }
}
