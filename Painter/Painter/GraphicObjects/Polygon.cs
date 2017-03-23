using System.Drawing;
using System.Linq;

namespace Paint.GraphicObjects
{
    public class Polygon : GraphicObject, IRotatable
    {
        private PointF[] points;
        private PointF middlePoint;

        private readonly Pen pen;
        private readonly TextureBrush textureBrush;

        public Polygon(PointF[] points, PointF middlePoint, Pen pen, TextureBrush textureBrush)
        {
            this.points = points;
            this.middlePoint = middlePoint;
            this.pen = pen;
            this.textureBrush = textureBrush;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(pen, points);
            graphics.FillPolygon(textureBrush, points);
        }

        public override void Move(MoveDirection direction, int moveRange)
        {
            points = points.Select(point => PointMover.GetMovedPoint(point, direction, moveRange)).ToArray();
            middlePoint = PointMover.GetMovedPoint(middlePoint, direction, moveRange);
        }

        public void RotateClockwise(double angle)
        {
            points = points.Select(point => PointMover.GetFirstPointAfterRotateRelativeSecondPoint(point, middlePoint, angle)).ToArray();
        }
    }
}