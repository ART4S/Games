using System.Drawing;
using System.Linq;

namespace Paint
{
    public class Polygon : IGraphicObject
    {
        private PointF[] points;
        private PointF middlePoint;

        private readonly Pen pen;
        private readonly TextureBrush textureBrush;

        private readonly PointMover pointMover;

        public Polygon(PointF[] points, PointF middlePoint, Pen pen, TextureBrush textureBrush)
        {
            this.points = points;
            this.middlePoint = middlePoint;
            this.pen = pen;
            this.textureBrush = textureBrush;

            pointMover = new PointMover();
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(pen, points);
            graphics.FillPolygon(textureBrush, points);
        }

        public void Move(MoveDirrection dirrection, int moveRange)
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