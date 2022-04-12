using System;

namespace CalcMethodLab3.Logic
{
    class Equation
    {
        public Func<double, double, double> Func { get; private set; }
        public Func<double, double, double> FuncDerivative { get; private set; }
        public Func<double, double> FuncItterative { get; private set; }

        public Equation(Func<double, double, double> func, Func<double, double, double> funcDerivative, Func<double, double> funcItterative)
        {
            Min = min;
            Max = max;
            Func = func;
            FuncDerivative = funcDerivative;
            FuncItterative = funcItterative;
        }
    }
}