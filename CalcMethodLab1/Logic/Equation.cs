using System;

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
            this.Func = func;
        }
    }
}