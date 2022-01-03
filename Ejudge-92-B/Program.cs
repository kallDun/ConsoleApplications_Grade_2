using System;
using System.IO;
using System.Linq;

namespace Ejudge_92_B
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var n = int.Parse(input.First());
            var arr = input.Skip(1).Select(x => x.Split()).Select(x => new Point(double.Parse(x[0]), double.Parse(x[1]))).ToArray();
            var dist = new MinDistance().FindClosest(arr, n);
            Console.WriteLine(dist);
        }
    }

    class MinDistance
    {
        public double BruteForceMethod(Point[] P, int n)
        {
            var min = double.MaxValue;
            for (int i = 0; i < n; ++i)
                for (int j = i + 1; j < n; ++j)
                    if (Distance(P[i], P[j]) < min)
                        min = Distance(P[i], P[j]);
            return min;
        }

        double Distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) +
                        (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        double Min(double x, double y)
        {
            return x < y ? x : y;
        }
        double StripClosest(Point[] strip, int size, double d)
        {
            var min = d;
            strip = strip.OrderBy(x => x.Y).ToArray();
            for (int i = 0; i < size; ++i)
                for (int j = i + 1; j < size && (strip[j].Y - strip[i].Y) < min; ++j)
                    if (Distance(strip[i], strip[j]) < min)
                        min = Distance(strip[i], strip[j]);

            return min;
        }

        double ClosestUtil(Point[] P, int n)
        {
            // If there are 2 or 3 points, then use brute force
            if (n <= 3) return BruteForceMethod(P, n);

            // Find the middle point
            int mid = n / 2;
            Point midPoint = P[mid];

            // Consider the vertical line passing
            // through the middle point calculate
            // the smallest distance dl on left
            // of middle point and dr on right side
            var dl = ClosestUtil(P, mid);
            var dr = ClosestUtil(P.Skip(mid).ToArray(), n - mid);

            var d = Min(dl, dr);

            Point[] strip = new Point[n];
            int j = 0;
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(P[i].X - midPoint.X) < d)
                {
                    strip[j] = P[i];
                    j++;
                } 
            }
            return Min(d, StripClosest(strip, j, d));
        }
        public double FindClosest(Point[] P, int n)
        {
            P = P.OrderBy(s => s.X).ToArray();
            return ClosestUtil(P, n);
        }
    }

    class Point
    {
        public double X, Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

}
