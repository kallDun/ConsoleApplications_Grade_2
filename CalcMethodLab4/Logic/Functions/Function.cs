using System.Collections.Generic;
using System.Windows;

namespace CalcMethodLab4.Logic.Functions.Abstract
{
    abstract class Function
    {
        public abstract List<Point> GetPointsRow(double min_x, double max_x, int count);
    }
}