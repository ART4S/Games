using System;
using System.CodeDom;
using System.Drawing;
using System.Linq;

namespace Painter
{
    public class Polygon : DrawingShape
    {
        private PointF[] points;
        private Point middlePoint; // for rotation

        public Polygon(PointF[] points, Point middlePoint, Pen pen, TextureBrush textureBrush) : base(pen, textureBrush)
        {
            this.points = points;
            this.middlePoint = middlePoint;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(pen, points);
            graphics.FillPolygon(textureBrush, points);
        }

        public override void Move(MoveDirrection dirrection, int moveRange)
        {
            Point dirrectionPoint = dirrection.ToPoint();

            points = points.Select(point => new PointF(point.X + moveRange * dirrectionPoint.X, point.Y + moveRange * dirrectionPoint.Y)).ToArray();
            middlePoint = new Point(middlePoint.X + moveRange * dirrectionPoint.X, middlePoint.Y + moveRange * dirrectionPoint.Y);
        }

        public void RotateClockwise()
        {
            points = points.Select(point => GetNewPointAfterRotateAroundMiddlePoint(point, 0.05)).ToArray();
        }

        public void RotateCounterСlockwise()
        {
            points = points.Select(point => GetNewPointAfterRotateAroundMiddlePoint(point, -0.05)).ToArray();
        }

        private PointF GetNewPointAfterRotateAroundMiddlePoint(PointF rotatablePoint, double angle)
        {
            return new PointF(
                (float)(middlePoint.X + (rotatablePoint.X - middlePoint.X) * Math.Cos(angle)
                                      - (rotatablePoint.Y - middlePoint.Y) * Math.Sin(angle)),
                (float)(middlePoint.Y + (rotatablePoint.X - middlePoint.X) * Math.Sin(angle)
                                      + (rotatablePoint.Y - middlePoint.Y) * Math.Cos(angle)));
        }
    }
}