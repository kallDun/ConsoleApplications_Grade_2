using System;
using System.Linq;

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
            double[][] A = matrix.Select(x => x.Take(matrix.Length).ToArray()).ToArray();
            double[] B = matrix.Select(x => x[matrix.Length]).ToArray();
            return Itterations(A, B);
        }

        private double[] Itterations(double[][] A, double[] B)
        {
            var count = A.Length;
            var X = new double[count];

            if (true)
            {
                for (var i = 0; i < count; i++)
                {
                    X[i] = B[i];
                }
                int itter = 0;
                double s = 0;
                do
                {
                    itter++;
                    for (var i = 0; i < count; i++)
                    {
                        var g = B[i];
                        for (var j = 0; j < count; j++)
                        {
                            g += A[i][j] * X[j];
                        }
                        s += (X[i] - g) * (X[i] - g);
                        X[i] = g;
                    }
                } while (Math.Sqrt(s) >= epsilon * (1 - GetThirdNorm(A)) / GetThirdNorm(A));
            }
            return X;
        }
        private double GetThirdNorm(double[][] matrix)
        {
            double sum = 0;
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix.Length; j++)
                {
                    sum += matrix[i][j] * matrix[i][j];
                }
            }
            sum = Math.Sqrt(sum);
            return sum;
        }
    }
}