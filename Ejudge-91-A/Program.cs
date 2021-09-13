using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_A
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Join("\n", File.ReadAllLines("input.txt").Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Split().Select(int.Parse).Sum())));
        }
    }
}
