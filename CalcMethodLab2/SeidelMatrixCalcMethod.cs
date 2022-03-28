using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Linq;
using System.Windows;

namespace CalcMethodLab2
{
    class SeidelMatrixCalcMethod : IMatrixCalculationMethod
    {
        private const int MAX_ITTERS = 1000;
        readonly double epsilon;
        public SeidelMatrixCalcMethod(double epsilon)
        {
            this.epsilon = epsilon;
        }

        public double[] FindRoots(double[][] matrix) => Itterations(matrix, matrix.Length);

        private double[] Itterations(double[][] A, int size)
        {
            bool with_norm = false;
            goto FindRootsWithoutNorm;
        FindRootsWithNorm:
            A = GetMatrixNorm(A, size);
            with_norm = true;
        FindRootsWithoutNorm:

            var prev = new double[size];
            int itter = 0;
            while (itter < MAX_ITTERS)
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
                if (itter is MAX_ITTERS && !with_norm) goto FindRootsWithNorm;
            }

            return prev;
        }

        private double[][] GetMatrixNorm(double[][] matrix, int n)
        {
            var A = matrix.Select(x => x.Take(n).ToArray()).ToArray();
            var B = matrix.Select(x => x.Skip(n).ToArray()).ToArray();
            var T = GetTransponentMatrix(A, n);
            var mult_A = MultiplyMatrix(T, A);
            var mult_B = MultiplyMatrix(T, B);
            var res = mult_A.Select((item, index) => item.ToList().Concat(mult_B[index]).ToArray()).ToArray();
            //MessageBox.Show(string.Join("\n", res.Select(x => string.Join("\t", x)))); debug log
            return res;
        }

        private double[][] GetTransponentMatrix(double[][] A, int n)
        {
            double[][] aT = new double[n][];
            for (int i = 0; i < n; i++)
            {
                double[] b = new double[n];
                for (int j = 0; j < n; j++)
                {
                    b[j] = A[j][i];
                }
                aT[i] = b;
            }
            return aT;
        }
        private double[][] MultiplyMatrix(double[][] A, double[][] B)
        {
            Matrix<double> a = DenseMatrix.OfArray(ConvertArray(A));
            Matrix<double> b = DenseMatrix.OfArray(ConvertArray(B));
            Matrix<double> result = a * b;
            return ConvertArray(result.ToArray());
        }
        private static double[,] ConvertArray(double[][] A)
        {
            var arr_a = new double[A.Length, A[0].Length];
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < A[0].Length; j++)
                {
                    arr_a[i, j] = A[i][j];
                }
            }
            return arr_a;
        }
        private static double[][] ConvertArray(double[,] A)
        {
            var arr_a = new double[A.GetLength(0)][];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                var arr = new double[A.GetLength(1)];
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    arr[j] = A[i, j];
                }
                arr_a[i] = arr;
            }
            return arr_a;
        }
    }
}