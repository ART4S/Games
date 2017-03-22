using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint
{
    public class Curve : IGraphicObject
    {
        private readonly Pen pen;
        private List<PointF> points;
        private readonly PointMover pointMover;

        public Curve(Pen pen)
        {
            this.pen = pen;

            points = new List<PointF>();
            pointMover = new PointMover();
        }

        public void AddPoint(PointF point)
        {
            points.Add(point);
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawCurve(pen, points.ToArray());
        }

        public void Move(MoveDirection direction, int moveRange)
        {
            points = points.Select(point => pointMover.GetMovedPoint(point, direction, moveRange)).ToList();
        }
    }
}