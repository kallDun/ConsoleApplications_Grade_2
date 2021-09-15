using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_1._1_Task_3_9.Classes
{
    class TDisk : IComparable<TDisk>
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
        
        // aditional code
        public override int GetHashCode() => (center.GetHashCode() * 17 + radius.GetHashCode()) * 17;
        public override bool Equals(object obj)
        {
            if (!(obj is TDisk)) return false;
            return CompareTo(obj as TDisk) == 0;
        }
        public int CompareTo(TDisk other)
        {
            var our_hash = GetHashCode();
            var other_hash = other.GetHashCode();
            return our_hash > other_hash ? 1 : our_hash == other_hash ? 0 : -1;
        }
        public static bool operator ==(TDisk one, TDisk another) => one.Equals(another);
        public static bool operator !=(TDisk one, TDisk another) => !(one == another);
        public static TDisk operator +(TDisk one, TDisk another) => new TDisk(one.radius + another.radius, new Point(one.center.X + another.center.X, one.center.Y + another.center.Y));
        public static TDisk operator -(TDisk one, TDisk another) => new TDisk(one.radius - another.radius, new Point(one.center.X - another.center.X, one.center.Y - another.center.Y));
    }
}
