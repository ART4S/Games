using System.Drawing;
using System.Linq;

namespace SimplePainter
{
    public class Polygon : DrawingShape
    {
        private readonly PointMover pointMover;

        private PointF[] points;
        private PointF middlePoint;

        public Polygon(PointF[] points, PointF middlePoint, Pen pen, TextureBrush textureBrush) : base(pen, textureBrush)
        {
            this.points = points;
            this.middlePoint = middlePoint;

            pointMover = new PointMover();
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(pen, points);
            graphics.FillPolygon(textureBrush, points);
        }

        public override void Move(MoveDirrection dirrection, int moveRange)
        {
            points = points.Select(point => pointMover.GetMovedPoint(point, dirrection, moveRange)).ToArray();
            middlePoint = pointMover.GetMovedPoint(middlePoint, dirrection, moveRange);
        }

        public void RotateClockwise(double angle)
        {
            points = points.Select(point => pointMover.GetFirstPointAfterRotateRelativeSecondPoint(point, middlePoint, angle)).ToArray();
        }
    }
}