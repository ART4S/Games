using System.Drawing;

namespace SimplePainter
{
    public class DrawingImage
    {
        private readonly PointMover pointMover;
        private readonly Image image;
        private PointF topLeftPoint;

        public DrawingImage(Image image, PointF topLeftPoint)
        {
            this.image = image;
            this.topLeftPoint = topLeftPoint;

            pointMover = new PointMover();
        }

        public void Display(Graphics graphics)
        {
            graphics.DrawImage(image, topLeftPoint);
        }

        public void Move(MoveDirrection dirrection, int moveRange)
        {
            topLeftPoint = pointMover.GetMovedPoint(topLeftPoint, dirrection, moveRange);
        }
    }
}