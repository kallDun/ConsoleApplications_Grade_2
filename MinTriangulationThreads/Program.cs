using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MinTriangulationThreads
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Algorythm algorythm = new Algorythm();
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Polygon count:");
            var verticles = new Generator().GenerateRandomPolygon(int.Parse(Console.ReadLine()));

            stopwatch.Start();
            var weight = algorythm.mTCDP(verticles, verticles.Length);
            stopwatch.Stop();
            Console.WriteLine($"one-thread result weight is {weight}\ntime is {stopwatch.ElapsedMilliseconds}\n");

            Console.WriteLine("Threads count:");
            var threads = int.Parse(Console.ReadLine());
            stopwatch.Restart();
            weight = await algorythm.mTCDP_MultiThread(verticles, verticles.Length, threads);
            stopwatch.Stop();
            Console.WriteLine($"multi-thread result weight is {weight}\ntime is {stopwatch.ElapsedMilliseconds}");

            Console.ReadKey();
        }
    }


    class Generator
    {
        private Random rnd = new Random();

        public Point[] GenerateRandomPolygon(int count)
        {
            var length_123 = count / 4;

            List<Point> points = new List<Point>();
            var first = new Point(0, 0);
            points.Add(first);
            for (int i = 1; i < length_123; i++)
            {
                var new_point = new Point(rnd.NextDouble() * 40 + points[i - 1].x, rnd.NextDouble() * 5 + first.y);
                points.Add(new_point);
            }
            var second = points[length_123 - 1];
            for (int i = length_123; i < length_123 * 2; i++)
            {
                var new_point = new Point(rnd.NextDouble() * 5 + second.x, rnd.NextDouble() * 40 + points[i - 1].y);
                points.Add(new_point);
            }
            var third = points[length_123 * 2 - 1];
            for (int i = length_123 * 2; i < length_123 * 3; i++)
            {
                var new_point = new Point(rnd.NextDouble() * 40 - points[i - 1].x, rnd.NextDouble() * 5 - third.y);
                points.Add(new_point);
            }
            var forth = points[length_123 * 3 - 1];
            for (int i = length_123 * 3; i < count; i++)
            {
                var new_point = new Point(rnd.NextDouble() * 5 - forth.x, rnd.NextDouble() * 40 - points[i - 1].y);
                points.Add(new_point);
            }

            return points.ToArray();
        }
    }

    class Algorythm
    {

        public double mTCDP(Point[] points, int n)
        {
            if (n < 3) return 0;
            double[,] table = new double[n, n];

            for (int gap = 0; gap < n; gap++)
            {
                for (int i = 0, j = gap; j < n; i++, j++)
                {
                    if (j < i + 2)
                        table[i, j] = 0.0;
                    else
                    {
                        table[i, j] = double.MaxValue;
                        for (int k = i + 1; k < j; k++)
                        {
                            double val = table[i, k] + table[k, j] + GetCost(points, i, j, k);
                            if (table[i, j] > val) table[i, j] = val;
                        }
                    }
                }
            }
            return table[0, n - 1];
        }

        public async Task<double> mTCDP_MultiThread(Point[] points, int n, int threads)
        {
            if (n < 3) return 0;
            double[,] table = new double[n, n];

            for (int gap = 0; gap < n; gap++)
            {
                List<Task> tasks = new List<Task>();
                for (int thread = 0; thread < threads; thread++)
                {
                    var gp = n - gap / threads;
                    var start = gap + gp * thread;
                    var end = gap + gp * (thread + 1);
                    if (thread == threads - 1) end = n;
                    tasks.Add(Task.Run(async () => await Task.Run(() =>
                    {
                        for (int i = 0, j = gap; j < n; i++, j++)
                        {
                            if (j < i + 2) table[i, j] = 0.0;
                            else
                            {
                                table[i, j] = double.MaxValue;

                                for (int k = i + 1; k < j; k++)
                                {
                                    double val = table[i, k] + table[k, j] + GetCost(points, i, j, k);
                                    if (table[i, j] > val) table[i, j] = val;
                                }
                            }
                        }
                    })));
                }
                await Task.WhenAll(tasks);
            }
            return table[0, n - 1];
        }

        public async Task<double> mTC(Point[] points, int i, int j)
        {
            if (j < i + 2) return 0;
            double res = double.MaxValue;

            for (int k = i + 1; k < j; k++) 
            {
                res = Min(res, await mTC(points, i, k) + await mTC(points, k, j) + GetCost(points, i, k, j));
            }
            return res;
        }

        public async Task<double> mTC_multithread(Point[] points, int i, int j)
        {
            if (j < i + 2) return 0;
            double res = double.MaxValue;

            for (int k = i + 1; k < j; k++)
            {
                var res_a = mTC(points, i, k);
                var res_b = mTC(points, k, j);

                await Task.WhenAll(res_a, res_b);
                res = Min(res, res_a.Result + res_b.Result + GetCost(points, i, k, j));
            }
            return res;
        }

        private double Min(params double[] input) => input.Min();

        private double GetCost(Point[] points, int i, int j, int k)
        {
            Point p1 = points[i], p2 = points[j], p3 = points[k];
            return p1.Distance(p2) + p2.Distance(p3) + p3.Distance(p1);
        }
    }

    class Point
    {
        public double x, y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double Distance(Point p)
        {
            return Math.Pow((x - p.x) * (x - p.x) + (y - p.y) * (y - p.y), 2);
        }
    }
}
