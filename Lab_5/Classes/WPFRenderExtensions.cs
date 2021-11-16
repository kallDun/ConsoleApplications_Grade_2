using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab_5.Classes
{
    static class WPFRenderExtensions
    {
        public static PointCollection GetPointCollection(this List<Point> points)
        {
            PointCollection collection = new() { };
            foreach (var point in points)
            {
                collection.Add(point);
            }
            return collection;
        }

        public static void DrawSpline(this DrawingContext dc, PointCollection pointCollection, double offsetX, double offsetY, Brush background, Pen pen)
        {
            if (pointCollection.Count == 0) return;

            pointCollection = PointCollection.Parse(
                string.Join(" ", pointCollection
                .Select(x => new Point(x.X + offsetX, x.Y + offsetY))
                .Select(x => $"{x.X},{x.Y}"))
                );

            StreamGeometry streamGeometry = new();

            using (StreamGeometryContext gcx = streamGeometry.Open())
            {
                gcx.BeginFigure(pointCollection.First(), true, true);
                gcx.PolyLineTo(pointCollection, true, true);
            }

            dc.DrawGeometry(background, pen, streamGeometry);
        }

        private static GeometryDrawing CreateArcDrawing(Point point, int radius, int degree_start, int degree_sweep)
        {
            var startRadians = degree_start * Math.PI / 180.0;
            var sweepRadians = degree_sweep * Math.PI / 180.0;

            var xs = point.X + (Math.Cos(startRadians) * radius);
            var ys = point.Y + (Math.Sin(startRadians) * radius);

            var xe = point.X + (Math.Cos(startRadians + sweepRadians) * radius);
            var ye = point.Y + (Math.Sin(startRadians + sweepRadians) * radius);


            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext ctx = streamGeometry.Open())
            {
                bool isLargeArc = Math.Abs(degree_sweep) > 180;
                SweepDirection sweepDirection = SweepDirection.Clockwise;

                ctx.BeginFigure(new Point(xs, ys), false, false);
                ctx.ArcTo(new Point(xe, ye), new Size(radius, radius),
                    0, isLargeArc, sweepDirection, true, false);
            }

            GeometryDrawing drawing = new GeometryDrawing();
            drawing.Geometry = streamGeometry;
            return drawing;
        }

        public static void DrawArc(this DrawingContext dc, Point point, int radius, int degree_start, int degree_sweep, Brush background, Pen pen)
            => dc.DrawGeometry(background, pen, CreateArcDrawing(point, radius, degree_start, degree_sweep).Geometry);

        public static List<Point> DrawSolidGrid(this DrawingContext dc, double distance, double width, Point render_start, Point redner_end, Point shift, Brush color)
        {
            var shiftX = shift.X - (Math.Truncate(shift.X / distance) * distance);
            for (double i = render_start.X - shiftX; i < redner_end.X; i += distance)
            {
                dc.DrawRectangle(color, null, new Rect(i - width / 2, render_start.Y, width, redner_end.Y - render_start.Y));
            }

            var shiftY = shift.Y - (Math.Truncate(shift.Y / distance) * distance);
            for (double i = render_start.Y - shiftY; i < redner_end.Y; i += distance)
            {
                dc.DrawRectangle(color, null, new Rect(render_start.X, i - width / 2, redner_end.X - render_start.X, width));
            }

            var points = new List<Point>();
            for (double i = render_start.X - shiftX; i < redner_end.X; i += distance)
            {
                for (double j = render_start.Y - shiftY; j < redner_end.Y; j += distance)
                {
                    points.Add(new Point(i, j));
                }
            }
            return points;
        }
    }
}
