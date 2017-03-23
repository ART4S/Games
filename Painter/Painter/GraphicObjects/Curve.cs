using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint.GraphicObjects
{
    public class Curve : IGraphicObject
    {
        private readonly Pen pen;
        private List<PointF> points;

        public Curve(PointF startingFirstPoint, PointF startingSecondPoint, Pen pen)
        {
            this.pen = pen;

            points = new List<PointF>
            {
                startingFirstPoint,
                startingSecondPoint
            };
        }

        public void AddPoint(PointF point)
        {
            points.Add(point);
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawCurve(pen, points.ToArray());

            foreach (PointF point in points)
                graphics.FillEllipse(new SolidBrush(pen.Color), point.X - pen.Width / 2, point.Y - pen.Width / 2, pen.Width, pen.Width);
        }

        public void Move(MoveDirection direction, int moveRange)
        {
            points = points.Select(point => PointMover.GetMovedPoint(point, direction, moveRange)).ToList();
        }
    }
}