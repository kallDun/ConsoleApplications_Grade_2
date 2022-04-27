using CalcMethodLab4.Logic.Functions;
using CalcMethodLab4.Logic.Functions.Abstract;
using System.Linq;
using System.Text;
using System.Windows;

namespace CalcMethodLab4.Logic
{
    class LagrangePolynomBuilder : ISplineFunctionBuilder
    {
        public Function Interpolate(Point[] points)
        {
            var count = points.Length;
            double[][] selection_coeffs = new double[count][];
            for (int i = 0; i < count; i++)
            {
                var point = points[i];
                var list = points.Take(i).Concat(points.Skip(i + 1)).Select(s => s.X).ToArray();
                selection_coeffs[i] = GetCoefficients(point.Y, point.X, list);
            }
            var coeffs = selection_coeffs.Sum();

            StringBuilder string_result = new StringBuilder();
            for (int i = coeffs.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    string_result.Append(coeffs[i].ToString());
                }
                else
                {
                    string_result.Append($"{coeffs[i]}*x{(i == 1 ? "" : $"^{i}")} +");
                }                
            }
            return new SimpleFunction(string_result.ToString());
        }

        private double[] GetCoefficients(double coeff_before, double coeff_down, double[] other_coeffs)
        {
            int count = other_coeffs.Length + 1;
            double[] coeff_results = new double[count];
            double mult_coeff = coeff_before / other_coeffs.Select(x => coeff_down - x).Mult();

            coeff_results[1] = 1;
            coeff_results[0] = -other_coeffs[0];
            for (int i = 1; i < other_coeffs.Length; i++)
            {                
                for (int j = count - 1; j >= 0; j--)
                {
                    coeff_results[j] = j == 0 ? coeff_results[j] * -other_coeffs[i]
                        : coeff_results[j] * -other_coeffs[i] + coeff_results[j - 1];
                }
            }
            return coeff_results.Select(x => x * mult_coeff).ToArray();
        }        
    }
}