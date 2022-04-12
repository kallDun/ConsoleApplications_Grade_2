using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethodLab3.Logic
{
    class NewtonCalcMethod
    {
        private const int MAX_ITTERATIONS = 1000;
        readonly double epsilon;
        public NewtonCalcMethod(double epsilon)
        {
            this.epsilon = epsilon;
        }

        private double[,] Inverse(double [,] a)
        {
            double[,] matrix = new double[2, 2];
            double det, aa;

            det = a[0, 0] * a[1, 1] - a[0, 1] * a[1, 0];
            aa = a[0, 0];
            matrix[0, 0] = a[1, 1] / det;
            matrix[1, 1] = aa / det;
            //aa = a[0, 1];
            matrix[0, 1] = -a[0, 1] / det;
            matrix[1, 0] = -a[1, 0] / det;
            return matrix;
        }

        public SystemSolveValue CalcFunc(EquationGroup equations, double x, double y)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int itters = 1;
            double[,] matrix = new double[2, 2];
            double[] b = new double[2];
            double dx, dy, norm;
            do
            {
                matrix[0,0] = equations.EquationX.GetDerivativeX(x, y);
                matrix[0,1] = equations.EquationX.GetDerivativeY(x, y);
                matrix[1,0] = equations.EquationY.GetDerivativeX(x, y);
                matrix[1,1] = equations.EquationY.GetDerivativeY(x, y);
                matrix = Inverse(matrix);
                dx = -matrix[0, 0] * equations.EquationX.GetValue(x, y) + -matrix[0, 1] * equations.EquationY.GetValue(x, y);
                dy = -matrix[1, 0] * equations.EquationX.GetValue(x, y) + -matrix[1, 1] * equations.EquationY.GetValue(x, y);
                x += dx;
                y += dy;
                b[0] = equations.EquationX.GetValue(x, y);
                b[1] = equations.EquationY.GetValue(x, y);
                norm = Math.Sqrt(b[0] * b[0] + b[1] * b[1]);

                itters++;
            } 
            while (norm >= epsilon && itters < MAX_ITTERATIONS);

            timer.Stop();
            return new SystemSolveValue(x, y, timer.Elapsed, itters);
        }

        public bool Check(EquationGroup equations, double x, double y)
        {
            double[,] matrix = new double[2, 2];
            double dx, dy;

            matrix[0, 0] = equations.EquationX.GetDerivativeX(x, y);
            matrix[0, 1] = equations.EquationX.GetDerivativeY(x, y);
            matrix[1, 0] = equations.EquationY.GetDerivativeX(x, y);
            matrix[1, 1] = equations.EquationY.GetDerivativeY(x, y);
            dx = Math.Abs(equations.EquationX.GetValue(x, y) / matrix[0, 0]) + Math.Abs(equations.EquationY.GetValue(x, y) / matrix[0, 1]);
            dy = Math.Abs(equations.EquationX.GetValue(x, y) / matrix[1, 0]) + Math.Abs(equations.EquationY.GetValue(x, y) / matrix[1, 1]);

            return dx <= 1 && dy <= 1;
        }
    }
}