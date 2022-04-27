using CalcMethodLab4.Logic.Functions.Abstract;
using System.Windows;

namespace CalcMethodLab4.Logic
{
    interface ISplineFunctionBuilder
    {
        Function Interpolate(Point[] points);
    }
}