using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab_5.Classes
{
    abstract class Figure
    {
        public Point Location { get; protected set; }
        public Brush Color { get; set; } = Brushes.Black;

        protected Figure(Point location)
        {
            Location = location;
        }

        public abstract void Draw(DrawingContext dc);

        public async void MoveRight()
        {
            while (Location.X < 550)
            {
                Location = new Point(Location.X + 5, Location.Y);
                await Task.Delay(50);
            }
            Color = Brushes.White;
        }
    }
}
