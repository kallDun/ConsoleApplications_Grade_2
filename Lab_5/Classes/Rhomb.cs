using System.Windows;
using System.Windows.Media;

namespace Lab_5.Classes
{
    class Rhomb : Figure
    {
        private readonly PointCollection points = new()
        {
            new Point(0, 50),
            new Point(50, 100),
            new Point(100, 50),
            new Point(50, 0)
        };

        public Rhomb(Point location) : base(location) { }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawSpline(points, Location.X, Location.Y, Color, null);
        }
    }
}
