using System.Collections.Generic;
using System.Linq;

namespace CalcMethodLab1.Logic
{
    static class EquationExtension
    {
        static IEnumerable<double> DoubleRange(double min, double max, double step)
        {
            for (var value = min; value <= max; value += step)
            {
                yield return value;
            }
        }
        public static double GetItterationViewX(this Equation eq, double X)
        {
            double m = 1 / DoubleRange(eq.Min, eq.Max, (eq.Max - eq.Min) / 20).Select(x => eq.GetDerivativeX(x)).Max();
            return X - m * eq.Func(X);
        }
        public static double GetDerivativeX(this Equation eq, double x, double delta_x = 0.05) => (eq.Func(x + delta_x) - eq.Func(delta_x)) / delta_x;
    }
}