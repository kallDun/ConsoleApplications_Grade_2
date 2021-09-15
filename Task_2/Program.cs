using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = File.ReadAllLines("input.txt")[1].Split().Select(int.Parse).ToArray();
            var flag = true;
            for (int i = arr.Length - 2; flag && i >= 0; i--)
            {
                if (arr[i] - 1 == arr[i + 1])
                {
                    arr[i]--;
                    continue;
                }
                else
                if (arr[i] - 1 > arr[i + 1]) flag = false;            
            }
            Console.WriteLine(flag ? "Yes" : "No");
        }
    }
}
