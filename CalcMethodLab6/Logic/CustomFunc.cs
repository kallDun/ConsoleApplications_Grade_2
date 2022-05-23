using org.mariuszgromada.math.mxparser;

namespace CalcMethodLab6.Logic
{
    class CustomFunc
    {
        Function function;

        public CustomFunc(string function)
        {
            this.function = new Function("f(t,y) = " + function);
        }

        public double Calc(double t, double y) => function.calculate(t, y);
    }
}