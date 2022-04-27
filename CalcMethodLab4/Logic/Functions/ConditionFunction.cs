using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CalcMethodLab4.Logic.Functions
{
    class ConditionFunction : Abstract.Function
    {
        KeyValuePair<string, double>[] condition_funcs;
        KeyValuePair<Function, double>[] functions;
        public ConditionFunction(params KeyValuePair<string, double>[] condition_funcs)
        {
            this.condition_funcs = condition_funcs;
            functions = condition_funcs.Select(x => new KeyValuePair<Function, double>(new Function("f(x) =" + x.Key), x.Value)).Reverse().ToArray();
        }

        public override List<Point> GetPointsRow(double min_x, double max_x, int count)
        {
            List<Point> points = new List<Point>();
            var step = (max_x - min_x) / count;
            for (double x = min_x; x < max_x; x += step)
            {
                var func = functions.First(s => x >= s.Value).Key;
                points.Add(new Point(x, func.calculate(x)));
            }
            return points;
        }

        public override string ToString()
        {
            return string.Join("\n", condition_funcs.Select(item => $"x>{item.Value} -> f(x) = {item.Key}"));
        }
    }
}