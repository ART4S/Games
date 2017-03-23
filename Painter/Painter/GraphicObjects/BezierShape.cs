using System.Drawing;
using System.Linq;

namespace Paint.GraphicObjects
{
    public class BezierShape : GraphicObject, IRotatable
    {
        private PointF[] curve;
        private PointF middlePoint;

        private readonly Pen pen;

        public BezierShape(PointF[] curve, PointF middlePoint, Pen pen)
        {
            this.curve = curve;
            this.middlePoint = middlePoint;
            this.pen = pen;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawBeziers(pen, curve);
        }

        public override void Move(MoveDirection direction, int moveRange)
        {
            curve = curve.Select(point => PointMover.GetMovedPoint(point, direction, moveRange)).ToArray();
            middlePoint = PointMover.GetMovedPoint(middlePoint, direction, moveRange);
        }

        public void RotateClockwise(double angle)
        {
            curve = curve.Select(point => PointMover.GetFirstPointAfterRotateRelativeSecondPoint(point, middlePoint, angle)).ToArray();
        }
    }
}