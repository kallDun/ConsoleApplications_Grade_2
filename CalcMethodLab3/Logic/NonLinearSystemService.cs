using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethodLab3.Logic
{
    class NonLinearSystemService
    {

        public double SolveSystem(EquationGroup group, double x_start, double step)
        {

        }

        private double GetInitialValue(EquationGroup group, double x_start, double step)
        {
            while (x_start >= group.Min && x_start <= group.Max)
            {
                var x = x_start;
                var y1 = group.EquationY.FuncItterative(x_start);
                var x1 = group.EquationX.FuncItterative(y1);


                return x_start;
            }            
        }

    }
}
