using System.Collections.Generic;
using System.Linq;

namespace CalcMethodLab4.Logic
{
    public static class LinqExtensions
    {
        public static double Mult(this IEnumerable<double> list)
        {
            double result = 1;
            foreach (var item in list)
            {
                result *= item;
            }
            return result;
        }
        public static double[] Sum(this IEnumerable<double[]> lists)
        {
            double[] result = new double[lists.Count()];
            foreach (var list in lists)
            {
                var arr = list.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    result[i] += arr[i];
                }
            }
            return result;
        }
    }
}