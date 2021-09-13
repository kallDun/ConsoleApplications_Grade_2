using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_B
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine(string.Join("\n", File.ReadAllLines("input.txt").Where(x => !string.IsNullOrEmpty(x)).Skip(1).Select(x => x.Split().Select(int.Parse).ToArray()).Select(x => new Edge(x[0], x[1], x[2])).OrderBy(x => x)));
        }
    }

    struct Edge : IComparable<Edge>
    {
        private static int global_idx = 1;
        private int start, end, arc, idx;

        public Edge(int start, int end, int arc)
        {
            this.start = start;
            this.end = end;
            this.arc = arc;
            idx = global_idx++;
        }

        public override string ToString() => $"{start} {end} {arc} {idx}";

        public int CompareTo(Edge other) => arc.CompareTo(other.arc);
    }
}
