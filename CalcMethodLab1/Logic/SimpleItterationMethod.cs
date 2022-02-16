using System;

namespace CalcMethodLab1.Logic
{
    class SimpleItterationMethod : IEquationMethod
    {
        const double eps = 1e-6;
        const int itters = 1500;
        public double FindX(Equation equation)
        {
            double x = (equation.Max - equation.Min) / 2 + equation.Min, x_last = 0;
            bool err = true;
            for (int i = 0; i < itters; i++)
            {
                x_last = x;
                x = equation.Func(x_last);
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
