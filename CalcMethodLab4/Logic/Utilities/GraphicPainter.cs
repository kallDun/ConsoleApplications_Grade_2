using CalcMethodLab4.Logic.Functions.Abstract;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CalcMethodLab4.Logic
{
    class GraphicPainter
    {
        public Image DrawImage(Function function, Point[] points, Size render_size)
        {
            Point min_values = new Point(points.Min(s => s.X) - 1, points.Min(s => s.Y) - 4);
            Point max_values = new Point(points.Max(s => s.X) + 1, points.Max(s => s.Y) + 4);
            var spline = function.GetPointsRow(points.Min(s => s.X), points.Max(s => s.X), 100);

            return GetImage(render_size, 
            (dc) => // method draw
            {
                var type_face = new Typeface(new FontFamily("DejaVu Sans"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

                dc.DrawLine(new Pen(Brushes.DarkRed, 1.5),
                    GetPointOnMap(new Point(0, min_values.Y), min_values, max_values, render_size),
                    GetPointOnMap(new Point(0, max_values.Y), min_values, max_values, render_size));
                dc.DrawLine(new Pen(Brushes.DarkRed, 1.5),
                    GetPointOnMap(new Point(min_values.X, 0), min_values, max_values, render_size),
                    GetPointOnMap(new Point(max_values.X, 0), min_values, max_values, render_size));

                for (int i = (int)Math.Floor(min_values.X); i < max_values.X + 1; i++)
                {
                    var formatted_text = new FormattedText($"{i}", new CultureInfo("en-us"), FlowDirection.LeftToRight, type_face, 14, Brushes.Black, 96);
                    var loc = GetPointOnMap(new Point(i, 0.6), min_values, max_values, render_size);
                    dc.DrawText(formatted_text, loc);
                }
                for (int i = (int)Math.Floor(min_values.Y); i < max_values.Y + 1; i++)
                {
                    var formatted_text = new FormattedText($"{i}", new CultureInfo("en-us"), FlowDirection.LeftToRight, type_face, 14, Brushes.Black, 96);
                    var loc = GetPointOnMap(new Point(0, i), min_values, max_values, render_size);
                    dc.DrawText(formatted_text, loc);
                }

                for (int i = 0; i < spline.Count - 1; i++)
                {
                    var prev = GetPointOnMap(spline[i], min_values, max_values, render_size);
                    var next = GetPointOnMap(spline[i + 1], min_values, max_values, render_size);
                    dc.DrawLine(new Pen(Brushes.DimGray, 1), prev, next);
                }

                foreach (var point in points)
                {
                    dc.DrawEllipse(Brushes.Black, null, GetPointOnMap(point, min_values, max_values, render_size), 5, 5);
                }
            });
        }

        private Point GetPointOnMap(Point point, Point min_values, Point max_values, Size render_size)
        {
            Size span = new Size(max_values.X - min_values.X, max_values.Y - min_values.Y);
            double x_step = render_size.Width / span.Width;
            double y_step = render_size.Height / span.Height;
            return new Point((point.X - min_values.X) * x_step, (max_values.Y - point.Y) * y_step);
        }

        private Image GetImage(Size render_size, Action<DrawingContext> render)
        {
            Image image = new Image();
            image.Source = GetRender(render_size, render);
            return image;
        }
        private BitmapSource GetRender(Size render_size, Action<DrawingContext> render)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                (int)Math.Round(render_size.Width), (int)Math.Round(render_size.Height),
                96, 96, PixelFormats.Pbgra32);

            var drawingvisual = new DrawingVisual();

            using (DrawingContext dc = drawingvisual.RenderOpen())
            {
                render(dc);
            }

            bitmap.Render(drawingvisual);
            return bitmap;
        }
    }
}