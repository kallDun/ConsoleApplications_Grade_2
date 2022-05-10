namespace CalcMethodLab5.Logic.Derivatives
{
    class FirstDerivativeCalculator : DerivativeCalculator
    {
        public FirstDerivativeCalculator(Func func) : base(func) { }

        public override double GetDerivative(double x)
        {
            return (-3 * GetFuncValueOnXGrid(x, 0) + 4 * GetFuncValueOnXGrid(x, 1) 
                - GetFuncValueOnXGrid(x, 2)) / (2 * step);
        }
    }
}