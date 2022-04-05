using Lab_2_3.Logic.Render;
using Lab_2_3.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab_2_3.Logic.Models
{
    public class Horse : PropertyChangedNotifier, IRenderable
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Position { get; set; }
        public TimeSpan Time { get; set; }
        public double Coeff { get; set; }
        public int Track { get; set; }

        public List<ImageSource> Animations { get; set; }

        private bool isStarted;
        public async void StartRace(int traceEnds)
        {
            Random rnd = new Random();
            isStarted = true;
            speed = average_speed;
            speed_difference = 0;
            while (Position < traceEnds && isStarted)
            {
                await Task.Delay(200);
                var acceleration = rnd.Next(-5, 15);
                if (speed < average_speed / 2) acceleration += 10;
                if (speed > average_speed * 3 / 2) acceleration -= 10;
                speed_difference = acceleration;
            }
            isStarted = false;
            speed = 0;
            speed_difference = 0;
        }

        private const int average_speed = 100;
        private int speed;
        private int speed_difference;
        private double frame;
        public void Render(DrawingContext dc, int cameraPos)
        {
            dc.DrawImage(Animations[(int)Math.Floor(frame)], new Rect(new Point(Position - cameraPos, 370 - 70 * Track), new Size(100, 100)));

            if (speed > 0)
            {
                frame = (frame + 0.7 * (1.0 * speed / average_speed)) % Animations.Count;
            }
            Position += speed;
            if (speed_difference != 0) speed += speed_difference / 10;
            OnPropertyChanged(nameof(Position));
        }
    }
}