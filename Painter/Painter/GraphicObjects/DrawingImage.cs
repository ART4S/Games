using System.Drawing;

namespace Paint.GraphicObjects
{
    public class DrawingImage : GraphicObject
    {
        private readonly Image image;
        private PointF topLeftPoint;

        public DrawingImage(Image image, PointF topLeftPoint)
        {
            this.image = image;
            this.topLeftPoint = topLeftPoint;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(image, topLeftPoint);
        }

        public override void Move(MoveDirection direction, int moveRange)
        {
            topLeftPoint = PointMover.GetMovedPoint(topLeftPoint, direction, moveRange);
        }
    }
}