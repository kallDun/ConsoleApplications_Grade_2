using Lab_2_3.Logic.Render;
using Lab_2_3.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab_2_3.Logic.Models
{
    public class Horse : PropertyChangedNotifier, IRenderable
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }
        public int Position { get; private set; }
        public TimeSpan Time { get; private set; }
        public double Coeff { get; private set; }

        public Horse(string name, Color color, double coeff, int track, List<ImageSource> animations)
        {
            Name = name;
            Color = color;
            Coeff = coeff;
            Track = track;
            Animations = animations;
        }

        public async Task StartRace(int traceEnds, Stopwatch timer)
        {
            Random rnd = new Random();
            Position = 0;
            isStarted = true;
            speed = average_speed;
            speed_difference = 0;
            this.timer = timer;
            this.traceEnds = traceEnds;

            while (isStarted)
            {
                await Task.Delay(200);
                var (min, max) = (average_speed / 9 * -1, average_speed / 6.5);
                var acceleration = rnd.NextDouble() * (max - min) + min;
                if (speed < average_speed * 2 / 3) acceleration += average_speed / 17;
                if (speed > average_speed * 3 / 2) acceleration -= average_speed / 17;
                speed_difference = acceleration;
            }
        }
        public void StopRace()
        {
            isStarted = false;
            speed = 0;
            speed_difference = 0;
        }

        Stopwatch timer;
        private int Track;
        private List<ImageSource> Animations;
        private bool isStarted;
        private int traceEnds;
        private const double average_speed = 30;
        private double speed;
        private double speed_difference;
        private double frame;

        public void Render(DrawingContext dc, int cameraPos)
        {
            dc.DrawImage(Animations[(int)Math.Floor(frame)], new Rect(new Point(Position - cameraPos, 160 + 50 * Track), new Size(120 + Track * 8, 120 + Track * 12)));

            if (isStarted)
            {
                frame = (frame + 0.7 * (1.0 * speed / average_speed)) % Animations.Count;
                Position += (int)Math.Round(speed);
                if (speed_difference != 0) speed += speed_difference / 5;
                Time = new TimeSpan(timer.ElapsedTicks);
                if (Position >= traceEnds)
                {
                    Position = traceEnds;
                    frame = 0;
                    StopRace();
                }
                OnPropertyChanged(nameof(Position));
            }            
        }
    }
}