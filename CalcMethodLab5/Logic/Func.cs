using org.mariuszgromada.math.mxparser;

namespace CalcMethodLab5.Logic
{
    class Func
    {
        private Function function;

        public Func(string expression)
        {
            function = new Function("f(x) = " + expression);
        }

        public double GetValue(double x)
        {
            return function.calculate(x);
        }
    }
}