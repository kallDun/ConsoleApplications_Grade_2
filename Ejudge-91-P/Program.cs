using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_P
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = File.ReadAllLines("input.txt").Select(x => x.Split());
            IDisjointSet set = new DisjointSet();

            foreach (var task in tasks)
            {
                switch (task[0])
                {
                    case "RESET":
                        set.Reset(int.Parse(task[1]));
                        Console.WriteLine("RESET DONE");
                        break;
                    case "JOIN":
                        int p = int.Parse(task[1]), q = int.Parse(task[2]);
                        if (!set.Union(p, q))
                        {
                            Console.WriteLine($"ALREADY {p} {q}");
                        }
                        break;
                    case "CHECK":
                        Console.WriteLine(set.Connected(int.Parse(task[1]), int.Parse(task[2])) ? "YES" : "NO");
                        break;
                }
            }
        }
    }
}


interface IDisjointSet
{
    void Reset(int count);

    bool Union(int p, int q);

    bool Connected(int p, int q);
}

class DisjointSet : IDisjointSet
{
    private int[] nodes;
    private int[] count;

    public bool Connected(int p, int q) => Root(p) == Root(q);

    public void Reset(int count)
    {
        nodes = Enumerable.Range(0, count).ToArray();
        this.count = new int[count];
        for (int i = 0; i < count; i++) this.count[i] = 1;
    }

    public bool Union(int p, int q)
    {
        int pRoot = Root(p);
        int qRoot = Root(q);

        if (pRoot == qRoot) return false;

        if (count[pRoot] < count[qRoot])
        {
            nodes[pRoot] = qRoot;
            count[qRoot] += count[pRoot];
        }
        else
        {
            nodes[qRoot] = pRoot;
            count[pRoot] += count[qRoot];
        }
        return true;
    }

    private int Root(int p)
    {
        while (p != nodes[p])
        {
            p = nodes[p];
        }
        return p;
    }
}
