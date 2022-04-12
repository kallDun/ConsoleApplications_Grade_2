using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethodLab3.Logic
{
    class NonLinearSystemSolverService
    {
        NewtonCalcMethod calcMethod;
        public NonLinearSystemSolverService(double eps)
        {
            calcMethod = new NewtonCalcMethod(eps);
        }

        public SystemSolveValue SolveSystem(EquationGroup group, double x, double y)
        {
            if (!calcMethod.Check(group, x, y)) throw new Exception("Itterational process is not convergent!");
            return calcMethod.CalcFunc(group, x, y);
        }

    }
}