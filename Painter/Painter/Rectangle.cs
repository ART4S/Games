using System.Drawing;

namespace Paint
{
    public class Rectangle : IGraphicObject
    {
        private PointF topLeftPoint;

        private readonly int width;
        private readonly int height;

        private readonly Pen pen;
        private readonly TextureBrush textureBrush;

        private readonly PointMover pointMover;

        public Rectangle(PointF topLeftPoint, int width, int height, Pen pen, TextureBrush textureBrush)
        {
            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;
            this.pen = pen;
            this.textureBrush = textureBrush;

            pointMover = new PointMover();
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(pen, topLeftPoint.X, topLeftPoint.Y, width, height);
            graphics.FillRectangle(textureBrush, topLeftPoint.X, topLeftPoint.Y, width, height);
        }

        public void Move(MoveDirrection dirrection, int moveRange)
        {
            topLeftPoint = pointMover.GetMovedPoint(topLeftPoint, dirrection, moveRange);
        }
    }
}