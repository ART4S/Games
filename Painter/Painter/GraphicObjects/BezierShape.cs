using System.Drawing;
using System.Linq;

namespace Paint
{
    public class BezierShape : IGraphicObject, IRotatable
    {
        private PointF[] curve;
        private PointF middlePoint;

        private readonly Pen pen;

        private readonly PointMover pointMover;

        public BezierShape(PointF[] curve, PointF middlePoint, Pen pen)
        {
            this.curve = curve;
            this.middlePoint = middlePoint;
            this.pen = pen;

            pointMover = new PointMover();
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawBeziers(pen, curve);
        }

        public void Move(MoveDirection direction, int moveRange)
        {
            curve = curve.Select(point => pointMover.GetMovedPoint(point, direction, moveRange)).ToArray();
            middlePoint = pointMover.GetMovedPoint(middlePoint, direction, moveRange);
        }

        public void RotateClockwise(double angle)
        {
            curve = curve.Select(point => pointMover.GetFirstPointAfterRotateRelativeSecondPoint(point, middlePoint, angle)).ToArray();
        }
    }
}