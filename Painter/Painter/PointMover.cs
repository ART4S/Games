using System;
using System.Drawing;

namespace Paint
{
    public class PointMover
    {
        public PointF GetMovedPoint(PointF point, MoveDirrection dirrection, int moveRange)
        {
            return new PointF(point.X + moveRange * dirrection.ToPoint().X, point.Y + moveRange * dirrection.ToPoint().Y);
        }

        public PointF GetFirstPointAfterRotateRelativeSecondPoint(PointF firstPoint, PointF secondPoint, double angle)
        {
            return new PointF(
                (float)(secondPoint.X + (firstPoint.X - secondPoint.X) * Math.Cos(angle)
                                      - (firstPoint.Y - secondPoint.Y) * Math.Sin(angle)),
                (float)(secondPoint.Y + (firstPoint.X - secondPoint.X) * Math.Sin(angle)
                                      + (firstPoint.Y - secondPoint.Y) * Math.Cos(angle)));
        }
    }
}