using System;

namespace CalcMethodLab1.Logic
{
    class SimpleItterationEquationMethod : IEquationMethod
    {
        public readonly double eps;
        public readonly int itters;
        public SimpleItterationEquationMethod(double eps, int itters = 2000)
        {
            this.eps = eps;
            this.itters = itters;
        }

        public double FindX(Equation eq)
        {
            double x = (eq.Max + eq.Min) / 2;
            for (int i = 0; i < itters; i++)
            {
                double x_last = x;
                x = eq.GetItterationViewX(x);
                if (Math.Abs(x - x_last) < eps) break;
            }
            return x;
        }
    }
}