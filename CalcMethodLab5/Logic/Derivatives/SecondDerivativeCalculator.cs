using System;

namespace CalcMethodLab5.Logic.Derivatives
{
    class SecondDerivativeCalculator : DerivativeCalculator
    {
        public SecondDerivativeCalculator(Func func) : base(func) { }
        public override double GetDerivative(double x)
        {
            return
                (-GetFuncValueOnXGrid(x, -2) + (16 * GetFuncValueOnXGrid(x, -1))
                - (30 * GetFuncValueOnXGrid(x, 0))
                + (16 * GetFuncValueOnXGrid(x, 1)) - GetFuncValueOnXGrid(x, 2))
                / (12 * Math.Pow(step, 2));
            /*return (2 * GetFuncValueOnXGrid(x, 0) - 5 * GetFuncValueOnXGrid(x, 1) +
                4 * GetFuncValueOnXGrid(x, 2) - GetFuncValueOnXGrid(x, 3)) / 
                Math.Pow(step, 2);*/
        }
    }
}