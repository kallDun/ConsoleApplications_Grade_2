using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_G
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            var data = File.ReadAllLines("input.txt").Where(x => !string.IsNullOrEmpty(x)).Skip(1).Select(x => x.Split());
            foreach (var item in data)
            {
                if (item[0] == "1")
                {
                    if (dict.ContainsKey(item[1])) dict[item[1]] += int.Parse(item[2]);
                    else dict.Add(item[1], int.Parse(item[2]));
                }
                else
                {
                    int sum;
                    if (dict.TryGetValue(item[1], out sum))
                    {
                        Console.WriteLine(sum);
                    }
                    else
                        Console.WriteLine("ERROR");
                }
            }
        }
    }
}
