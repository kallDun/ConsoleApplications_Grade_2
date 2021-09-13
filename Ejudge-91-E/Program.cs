using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_E
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> set = new HashSet<int>();
            var data = File.ReadAllLines("input.txt").Where(x => !string.IsNullOrEmpty(x)).Skip(1).Select(x => x.Split());
            foreach (var item in data)
            {
                switch (item[0])
                {
                    case "ADD":
                        set.Add(int.Parse(item[1]));
                        break;
                    case "PRESENT":
                        var present = set.Contains(int.Parse(item[1]));
                        Console.WriteLine(present ? "YES" : "NO");
                        break;
                    case "COUNT":
                        Console.WriteLine(set.Count);
                        break;
                }
            }
        }
    }
}
