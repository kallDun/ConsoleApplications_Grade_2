using System.Collections.Generic;
using System.Linq;

namespace CalcMethodLab1.Logic
{
    class EquationCalculator
    {
        IEquationMethod method;
        public EquationCalculator()
        {
            method = new SimpleItterationEquationMethod();
        }

        public IEnumerable<double> GetEquationResults(Equation equation)
        {
            var (eqs, roots) = DivideIntoSegments(equation);
            return eqs.Select(x => method.FindX(x)).Concat(roots).OrderBy(x => x);
        }

        public (IEnumerable<Equation>, IEnumerable<double>) DivideIntoSegments(Equation equation)
        {
            List<Equation> equations = new List<Equation>();
            List<double> roots = new List<double>();
            RecursiveDivideIntoSegments(equation, ref equations, ref roots, 4000);
            return (equations, roots);
        }
        private void RecursiveDivideIntoSegments(Equation eq, ref List<Equation> output, ref List<double> roots, int segments_count, bool isAlright = false)
        {
            var divided = Enumerable.Range(0, segments_count + 1).Select(i => eq.Min + (eq.Max - eq.Min) / segments_count * i).Select(x => new { X = x, F = eq.Func(x), Fd = eq.GetDerivativeX(x)}).ToArray();
            bool enterRecursion = false;
            for (int i = 0; i < divided.Length - 1; i++)
            {
                if (divided[i].F is 0)
                {
                    roots.Add(divided[i].F);
                    continue;
                }
                if ((divided[i].F > 0 && divided[i + 1].F < 0) || (divided[i].F < 0 && divided[i + 1].F > 0))
                {
                    bool is_derivative_change = (divided[i].Fd > 0 && divided[i + 1].Fd < 0) || (divided[i].Fd < 0 && divided[i + 1].Fd > 0);
                    if (is_derivative_change)
                    {
                        RecursiveDivideIntoSegments(new Equation(divided[i].X, divided[i + 1].X, eq.Func), ref output, ref roots, segments_count, false);
                        enterRecursion = true;
                    }
                    else if (!isAlright)
                    {
                        RecursiveDivideIntoSegments(new Equation(divided[i].X, divided[i + 1].X, eq.Func), ref output, ref roots, segments_count, true);
                        enterRecursion = true;
                    }
                }
            }
            if (divided[divided.Length - 1].F is 0) roots.Add(divided[divided.Length - 1].F);
            else 
            if (!enterRecursion)
            {
                output.Add(eq);
            }
        }
    }
}