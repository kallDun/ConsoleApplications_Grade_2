using MathNet.Numerics.LinearAlgebra.Double;
using CalcMethodLab4.Logic.Functions;
using CalcMethodLab4.Logic.Functions.Abstract;
using System.Collections.Generic;
using System.Windows;

namespace CalcMethodLab4.Logic
{
    class CubicSplineBuilder : ISplineFunctionBuilder
    {
        SplineType splineType;
        public CubicSplineBuilder(SplineType splineType)
        {
            this.splineType = splineType;
        }

        public Function Interpolate(Point[] points)
        {
            double Sx0 = 0.1;
            double SxN = -0.5;
            double[] h = CalcH(points);
            double[] d = CalcD(points, h);
            double[] u = CalcU(d);
            double[] m = CalcMatrixM(points, h, d, u, Sx0, SxN);
            m = ExpandM(m, h, d, Sx0, SxN);
            var funcs = GetFunctions(points, m, h, d);
            return new ConditionFunction(funcs);
        }

        private KeyValuePair<string, double>[] GetFunctions(Point[] points, double[] m, double[] h, double[] d)
        {
            var result = new KeyValuePair<string, double>[points.Length - 1];
            for (int i = 0; i < result.Length; i++)
            {
                var coeff_0 = points[i].Y;
                var coeff_1 = d[i] - (h[i] * (2 * m[i] + m [i + 1]) / 6.0);
                var coeff_2 = m[i] / 2;
                var coeff_3 = (m[i + 1] - m[i]) / (6 * h[i]);
                string GetCoeffString(double coeff) => coeff >= 0 ? $"+{coeff}" : $"{coeff}";

                var string_X = GetCoeffString(-points[i].X);
                var func = 
                    $"{coeff_3} * (x {string_X})^3 " +
                    $"{GetCoeffString(coeff_2)} * (x {string_X})^2 " +
                    $"{GetCoeffString(coeff_1)} * (x {string_X}) " +
                    $"{GetCoeffString(coeff_0)}";
                result[i] = new KeyValuePair<string, double>(func, points[i].X);
            }
            return result;
        }
        private double[] ExpandM(double[] m, double[] h, double[] d, double sx0, double sxN)
        {
            double[] M = new double[m.Length + 2];
            for (int i = 0; i < m.Length; i++) M[i + 1] = m[i];

            if (splineType is SplineType.Natural)
            {
                M[0] = 0;
                M[M.Length - 1] = 0;
            }
            else if (splineType is SplineType.Serried)
            {
                M[0] = (3.0 / h[0] * (d[0] - sx0)) - (M[1] / 2.0);
                M[M.Length - 1] = (3.0 / h[M.Length - 2] * (sxN - d[M.Length - 2])) - (M[M.Length - 2] / 2.0);
            }
            return M;
        }
        private double[] CalcMatrixM(Point[] points, double[] h, double[] d, double[] u, double Sx0, double SxN)
        {
            var count = points.Length - 2;
            double[,] matrix = new double[count, count];
            double[] b = new double[count];

            // for index 0
            matrix[0, 0] = (3.0 / 2.0 * h[0]) + 2 * h[1];
            matrix[0, 1] = h[1];
            b[0] = u[0] - (3 * (d[0] - Sx0));
            
            // for index 1..N-2
            for (int i = 1; i < count - 1; i++)
            {
                matrix[i, i - 1] = h[i];
                matrix[i, i] = 2 * (h[i] + h[i + 1]);
                matrix[i, i + 1] = h[i + 1];
                b[i] = u[i];
            }

            // for index N - 1
            matrix[count - 1, count - 2] = h[count - 1];
            matrix[count - 1, count - 1] = (2 * h[count - 1]) + (3.0 / 2.0 * h[count]);
            b[count - 1] = u[count - 1] - (3 * (SxN - d[count]));

            // calc matrix
            var mx = DenseMatrix.OfArray(matrix);
            var results = mx.Solve(DenseVector.OfArray(b));
            return results.ToArray();
        }
        private double[] CalcU(double[] d)
        {
            double[] u = new double[d.Length - 1];
            for (int i = 0; i < d.Length - 1; i++)
            {
                u[i] = 6 * (d[i + 1] - d[i]);
            }
            return u;
        }
        private double[] CalcD(Point[] points, double[] h)
        {
            double[] d = new double[h.Length];
            for (int i = 0; i < points.Length - 1; i++)
            {
                d[i] = (points[i + 1].Y - points[i].Y) / h[i];
            }
            return d;
        }
        private double[] CalcH(Point[] points)
        {
            double[] h = new double[points.Length - 1];
            for (int i = 0; i < points.Length - 1; i++)
            {
                h[i] = points[i + 1].X - points[i].X;
            }
            return h;
        }
    }
}