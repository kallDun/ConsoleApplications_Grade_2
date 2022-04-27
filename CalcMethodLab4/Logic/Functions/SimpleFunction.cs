using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;
using System.Windows;

namespace CalcMethodLab4.Logic.Functions
{
    class SimpleFunction : Abstract.Function
    {
        string function;
        Function func;
        public SimpleFunction(string function)
        {
            this.function = function;
            func = new Function("f(x) = " + function);
        }

        public override List<Point> GetPointsRow(double min_x, double max_x, int count)
        {
            List<Point> points = new List<Point>();
            var step = (max_x - min_x) / count;
            for (double x = min_x; x < +max_x; x += step)
            {
                points.Add(new Point(x, func.calculate(x)));
            }
            return points;
        }

        public override string ToString()
        {
            return $"f(x) = {function}";
        }
    }
}