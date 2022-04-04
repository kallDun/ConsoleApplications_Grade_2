using Lab_2_3.Logic.Render;
using System;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab_2_3.Logic.Models
{
    public class Horse : IRenderable
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Position { get; set; }
        public TimeSpan Time { get; set; }
        public double Coeff { get; set; }

        public async void StartRace(int traceEnds)
        {
            while (Position < traceEnds)
            {
                await Task.Delay(10);
                Position += 10;
            }
        }

        public void Render(DrawingContext dc, int cameraPos)
        {
            throw new NotImplementedException();
        }
    }
}