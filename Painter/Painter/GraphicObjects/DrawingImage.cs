using System.Drawing;

namespace Paint.GraphicObjects
{
    public class DrawingImage : IGraphicObject
    {
        private readonly Image image;
        private PointF topLeftPoint;

        public DrawingImage(Image image, PointF topLeftPoint)
        {
            this.image = image;
            this.topLeftPoint = topLeftPoint;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(image, topLeftPoint);
        }

        public void Move(MoveDirection direction, int moveRange)
        {
            topLeftPoint = PointMover.GetMovedPoint(topLeftPoint, direction, moveRange);
        }
    }
}