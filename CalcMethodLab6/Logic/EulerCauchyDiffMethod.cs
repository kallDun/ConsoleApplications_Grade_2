using System;
using System.Linq;

namespace CalcMethodLab6.Logic
{
    class EulerCauchyDiffMethod : IDiffMethod
    {

        public ValuePair[] CalcDiffOn(CustomFunc func, double step, double range_from, double range_to, double init_value)
        {
            double a = range_from; double b = range_to; double h = step;
            int n = (int)Math.Floor((b - a) / h) + 1;
            double[] X = new double[n];
            double[] Y = new double[n];
            double[] Y1 = new double[n];
            X[0] = a; Y[0] = init_value;

            for (int i = 1; i < n; i++)
            {
                X[i] = a + i * h;
                Y1[i] = Y[i - 1] + h * func.Calc(X[i - 1], Y[i - 1]);
                Y[i] = Y[i - 1] + h * (func.Calc(X[i - 1], Y[i - 1]) + func.Calc(X[i], Y1[i])) / 2.0;
            }

            return X.Select((x, i) => new ValuePair(x, Y[i], i)).ToArray();
        }
    }
}