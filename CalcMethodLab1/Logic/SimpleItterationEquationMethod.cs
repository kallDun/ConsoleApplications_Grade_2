using System;

namespace CalcMethodLab1.Logic
{
    class SimpleItterationEquationMethod : IEquationMethod
    {
        const double eps = 1e-5;
        const int itters = 2000;
        public double FindX(Equation eq)
        {
            double x = (eq.Max - eq.Min) / 2 + eq.Min, x_last = 0;
            bool err = true;
            for (int i = 0; i < itters; i++)
            {
                x_last = x;
                x = eq.GetItterationViewX(x_last);
                if (Math.Abs(x - x_last) < eps)
                {
                    err = false;
                    break;
                }
            }
            return err ? double.NaN : x;
        }
    }
}