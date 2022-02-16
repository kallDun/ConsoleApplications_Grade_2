using System.Collections.Generic;
using System.Linq;

namespace CalcMethodLab1.Logic
{
    class EquationCalculator
    {
        IEquationMethod method;
        public EquationCalculator()
        {
            method = new SimpleItterationMethod();
        }

        public IEnumerable<double> GetEquationResults(Equation equation)
        {
            return DivideIntoSegments(equation).Select(x => method.FindX(x));
        }

        private IEnumerable<Equation> DivideIntoSegments(Equation equation)
        {

        }
    }
}
