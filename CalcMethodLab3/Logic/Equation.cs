using System;
using System.Linq;

namespace CalcMethodLab1.Logic
{
    class Equation
    {
        public double Min, Max;
        public Func<double, double> Func;

        public Equation(double min, double max, Func<double, double> func)
        {
            Min = min;
            Max = max;
            Func = func;
        }
        public Func<double, double> GetItterationViewFunc(int count = 50)
        {
            double m = 1 / Enumerable.Range(0, count + 1)
                .Select(i => Min + ((Max - Min) / count * i))
                .Select(x => GetDerivativeX(x)).Max();
            return x => x - (m * Func(x));
        }
        public double GetDerivativeX(double x, double delta_x = 0.01) 
            => (Func(x + delta_x) - Func(delta_x)) / delta_x;
    }
}