using System;

namespace CalcMethodLab3.Logic
{
    struct SystemSolveValue
    {
        public double X, Y;
        public TimeSpan Time;
        public int Itterations;
        public SystemSolveValue(double x, double y, TimeSpan time, int itterations)
        {
            X = x;
            Y = y;
            this.Time = time;
            this.Itterations = itterations;
        }
    }
}