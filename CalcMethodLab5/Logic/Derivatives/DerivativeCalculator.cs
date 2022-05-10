namespace CalcMethodLab5.Logic.Derivatives
{
    abstract class DerivativeCalculator
    {
        protected Func func;
        protected double step = 1e-4;
        protected DerivativeCalculator(Func func)
        {
            this.func = func;
        }
        public abstract double GetDerivative(double x);

        protected double GetFuncValueOnXGrid(double x, int count)
        {
            return func.GetValue(count * step + x);
        }
    }
}