using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab_5.Classes
{
    class Circle : Figure
    {
        public Circle(Point location) : base(location) { }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawEllipse(Color, null, new Point(Location.X + 50, Location.Y + 50), 50, 50);
        }
    }
}
