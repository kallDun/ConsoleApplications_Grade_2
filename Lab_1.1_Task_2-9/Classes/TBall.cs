using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_1._1_Task_2_9.Classes
{
    class TBall : TDisk
    {
        public TBall(TBall ball) : this(ball.radius, ball.center, ball.Z) { }
        public TBall(double radius, Point center, double z) : base(radius, center)
        {
            Z = z;
        }
        public TBall() : base()
        {
            Z = 0;
        }
        public double Z { get; private set; }
        public double Volume { get => radius * 4 / 3 * Square; }
        public override bool BelongsTo(Point point, double Z) 
            => Math.Pow(center.X - point.X, 2) + Math.Pow(center.Y - point.Y, 2) + Math.Pow(this.Z - Z, 2) < Math.Pow(radius, 2);
        public override string ToString() => base.ToString().Insert(base.ToString().Length - 1, $";{Z}");
    }
}
