using System.Drawing;

namespace Paint
{
    public class DrawingImage : IGraphicObject
    {
        private readonly Image image;
        private PointF topLeftPoint;

        private readonly PointMover pointMover;

        public DrawingImage(Image image, PointF topLeftPoint)
        {
            this.image = image;
            this.topLeftPoint = topLeftPoint;

            pointMover = new PointMover();
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(image, topLeftPoint);
        }

        public void Move(MoveDirrection dirrection, int moveRange)
        {
            topLeftPoint = pointMover.GetMovedPoint(topLeftPoint, dirrection, moveRange);
        }
    }
}