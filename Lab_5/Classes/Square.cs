using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab_5.Classes
{
    class Square : Figure
    {
        public Square(Point location) : base(location) { }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawRectangle(Color, null, new Rect(Location, new Size(100, 100)));
        }
    }
}
