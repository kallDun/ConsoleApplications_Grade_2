using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethodLab2
{
    class SeidelMatrixCalcMethod : IMatrixCalculationMethod
    {
        public double[] FindRoots(double[][] matrix)
        {
            return matrix[0];
        }
    }
}
