using System;

namespace CalcMethodLab1.Logic
{
    class SimpleItterationEquationMethod : IEquationMethod
    {
        public readonly double eps;
        public readonly int itters;
        public SimpleItterationEquationMethod(double eps, int itters)
        {
            this.eps = eps;
            this.itters = itters;
        }

        public double FindX(Equation eq)
        {
            Func<double, double> func = eq.GetItterationViewFunc(itters);
            double x = (eq.Max + eq.Min) / 2;
            for (int i = 0; i < itters; i++)
            {
                if (x < eq.Min || x > eq.Max) return (eq.Max + eq.Min) / 2;
                double x_last = x;
                x = func(x);
                if (Math.Abs(x - x_last) < eps) break;
            }
            return x;
        }
    }
}