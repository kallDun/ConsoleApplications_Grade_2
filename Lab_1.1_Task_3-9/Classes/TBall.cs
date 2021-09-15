using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_1._1_Task_3_9.Classes
{
    class TBall : TDisk, IComparable<TBall>
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

        // additional code
        public override int GetHashCode() => (base.GetHashCode() + Z.GetHashCode()) * 17;
        public override bool Equals(object obj)
        {
            if (!(obj is TBall)) return false;
            return CompareTo(obj as TBall) == 0;
        }
        public int CompareTo(TBall other)
        {
            var our_hash = GetHashCode();
            var other_hash = other.GetHashCode();
            return our_hash > other_hash ? 1 : our_hash == other_hash ? 0 : -1;
        }
        public static bool operator ==(TBall one, TBall another) => one.Equals(another);
        public static bool operator !=(TBall one, TBall another) => !(one == another);
        public static TBall operator +(TBall one, TBall another) => new TBall(one.radius + another.radius, new Point(one.center.X + another.center.X, one.center.Y + another.center.Y), one.Z + another.Z);
        public static TBall operator -(TBall one, TBall another) => new TBall(one.radius - another.radius, new Point(one.center.X - another.center.X, one.center.Y - another.center.Y), one.Z - another.Z);
    }
}
