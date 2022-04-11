using System;
using System.Linq;

namespace CalcMethodLab1.Logic
{
    class Equation
    {
        public double Min, Max;
        public Func<double, double, double> Func;

        public Equation(double min, double max, Func<double, double, double> func)
        {
            Min = min;
            Max = max;
            Func = func;
        }
        public Func<double, double, double> GetItterationViewFunc(int count = 50)
        {
            double m = 1 / Enumerable.Range(0, count + 1)
                .Select(i => Min + ((Max - Min) / count * i))
                .Select((x, y) => GetDerivativeX(x, y)).Max();
            return (x, y) => x - (m * Func(x, y));
        }
        public double GetDerivativeX(double x, double y, double delta = 0.01) 
            => (Func(x + delta, y + delta) - Func(delta, delta)) / delta;
    }
}