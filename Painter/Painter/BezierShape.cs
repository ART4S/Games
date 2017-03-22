using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SimplePainter
{
    public class BezierShape : DrawingShape
    {
        private readonly PointMover pointMover;

        private BezierCurve[] curves;
        private PointF middlePoint;

        public BezierShape(BezierCurve[] curves, PointF middlePoint, Pen pen, TextureBrush textureBrush) : base(pen, textureBrush)
        {
            this.curves = curves;
            this.middlePoint = middlePoint;

            pointMover = new PointMover();
        }

        public override void Draw(Graphics graphics)
        {
            foreach (BezierCurve curve in curves)
                graphics.DrawBezier(pen, curve.firstPoint, curve.firstBendingPoint, curve.secondBendingPoint, curve.secondPoint);
        }

        public override void Move(MoveDirrection dirrection, int moveRange)
        {
            curves = curves.Select(curve =>
            new BezierCurve(
                pointMover.GetMovedPoint(curve.firstPoint, dirrection, moveRange),
                pointMover.GetMovedPoint(curve.secondPoint, dirrection, moveRange),
                pointMover.GetMovedPoint(curve.firstBendingPoint, dirrection, moveRange),
                pointMover.GetMovedPoint(curve.secondBendingPoint, dirrection, moveRange)))
                .ToArray();

            middlePoint = pointMover.GetMovedPoint(middlePoint, dirrection, moveRange);
        }

        public void RotateClockwise(double angle)
        {
            curves = curves.Select(curve =>
            new BezierCurve(
                pointMover.GetFirstPointAfterRotateRelativeSecondPoint(curve.firstPoint, middlePoint, angle),
                pointMover.GetFirstPointAfterRotateRelativeSecondPoint(curve.secondPoint, middlePoint, angle),
                pointMover.GetFirstPointAfterRotateRelativeSecondPoint(curve.firstBendingPoint, middlePoint, angle),
                pointMover.GetFirstPointAfterRotateRelativeSecondPoint(curve.secondBendingPoint, middlePoint, angle)
                )).ToArray();
        }
    }
}