using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_1._1_Task_2_9.Classes
{
    class TDisk
    {
        public double radius { get; private set; }
        public Point center { get; private set; }
        public TDisk(double radius, Point center)
        {
            this.radius = radius;
            this.center = center;
        }
        public TDisk() : this(10, new Point(0, 0)) { }
        public TDisk(TDisk other) : this(other.radius, other.center) { }
        public double Square
        {
            get => Math.PI * Math.Pow(radius, 2);
        }
        public virtual bool BelongsTo(Point point, double Z = 0) 
            => Math.Pow(center.X - point.X, 2) + Math.Pow(center.Y - point.Y, 2) < Math.Pow(radius, 2);
        public static TDisk operator *(TDisk obj1, double number) => new TDisk(obj1.radius * number, obj1.center);
        public static TDisk operator *(double number, TDisk obj1) => obj1 * number;
        public override string ToString() => $"{GetType().ToString().Split('.').Last()}: radius:{radius} center:({center.X};{center.Y})";
    }
}
