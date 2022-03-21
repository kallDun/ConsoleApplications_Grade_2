using System;

namespace CalcMethodLab2
{
    class SeidelMatrixCalcMethod : IMatrixCalculationMethod
    {
        readonly double epsilon;
        public SeidelMatrixCalcMethod(double epsilon)
        {
            this.epsilon = epsilon;
        }

        public double[] FindRoots(double[][] matrix)
        {
            return Itterations(matrix, matrix.Length);
        }

        private double[] Itterations(double[][] A, int size)
        {
            var prev = new double[size];
            int itter = 0;
            while (itter < 1000)
            {
                var curr = new double[size];

                for (int i = 0; i < size; i++)
                {
                    curr[i] = A[i][size];

                    for (int j = 0; j < size; j++)
                    {
                        if (j < i) curr[i] -= A[i][j] * curr[j];
                        if (j > i) curr[i] -= A[i][j] * prev[j];
                    }
                    curr[i] /= A[i][i];
                }

                double err = 0;
                for (int i = 0; i < size; i++)
                {
                    err += Math.Abs(curr[i] - prev[i]);
                }
                if (err < epsilon) break;
                prev = curr;
                itter++;
            }

            return prev;
        }
    }
}