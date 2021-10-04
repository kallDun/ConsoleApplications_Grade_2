using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_O
{
    class Program
    {
        private const int INFINITY = 2009000999;

        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");
            int counter = 0;
            var N = int.Parse(data[counter++]);

            for (int i = 0; i < N; i++)
            {
                var N_M = data[counter++].Split().Select(int.Parse).ToArray();
                var graph = new Dictionary<int, int>[N_M[0]];
                for (int j = 0; j < graph.Length; j++)
                    graph[j] = new Dictionary<int, int>();

                for (int j = 0; j < N_M[1]; j++)
                {
                    var edge = data[counter++].Split().Select(int.Parse).ToArray();
                    graph[edge[0]].Add(edge[1], edge[2]);
                    graph[edge[1]].Add(edge[0], edge[2]);
                }

                var start = int.Parse(data[counter++]);
                var output = DijkstreesAlgorithm(graph, N_M[0], start);

                Console.WriteLine(string.Join(" ", output));
            }
        }

        private static int[] DijkstreesAlgorithm(Dictionary<int, int>[] graph, int count, int start)
        {
            var dist = new int[count];
            for (int i = 0; i < dist.Length; i++) dist[i] = INFINITY;
            dist[start] = 0;

            var used = new bool[count];

            var piramide = new SortedSet<Edge>();

            var queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();
                used[curr] = true;

                foreach (var neighbor in graph[curr])
                {
                    if (!used[neighbor.Key])
                    {
                        bool? flag = null;
                        int old_length = -1;

                        var d = dist[curr] + neighbor.Value;
                        if (dist[neighbor.Key] > d)
                        {
                            if (dist[neighbor.Key] == INFINITY)
                            {
                                flag = true;
                            }
                            else 
                            {
                                old_length = dist[neighbor.Key];
                                flag = false; 
                            }

                            dist[neighbor.Key] = d;                            
                        }

                        if (flag != null)
                        {
                            var edge = new Edge(neighbor.Key, dist[neighbor.Key]);

                            if ((bool)flag)
                            {
                                piramide.Add(edge);
                            }
                            else
                            {
                                piramide.Remove(new Edge(neighbor.Key, old_length));
                                piramide.Add(edge);
                            }
                        }                   
                    }
                }

                if (piramide.Count > 0)
                {
                    var elem = piramide.First();
                    queue.Enqueue(elem.vertex);
                    piramide.Remove(elem);
                }
            }

            return dist;
        }
    }

    class Edge : IComparable<Edge>
    {        
        public int vertex, length;

        public Edge(int vertex, int length)
        {
            this.vertex = vertex;
            this.length = length;
        }

        public override int GetHashCode() => vertex * 17 + length;
        public int CompareTo(Edge other)
        {
            var compare = length.CompareTo(other.length);
            if (compare != 0) return compare;
            return vertex.CompareTo(other.vertex);
        }
    }
}
