using System.Drawing;

namespace Paint.GraphicObjects
{
    public class Rectangle : GraphicObject
    {
        private PointF topLeftPoint;

        private readonly int width;
        private readonly int height;

        private readonly Pen pen;
        private readonly TextureBrush textureBrush;

        public Rectangle(PointF topLeftPoint, int width, int height, Pen pen, TextureBrush textureBrush)
        {
            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;
            this.pen = pen;
            this.textureBrush = textureBrush;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(pen, topLeftPoint.X, topLeftPoint.Y, width, height);
            graphics.FillRectangle(textureBrush, topLeftPoint.X, topLeftPoint.Y, width, height);
        }

        public override void Move(MoveDirection direction, int moveRange)
        {
            topLeftPoint = PointMover.GetMovedPoint(topLeftPoint, direction, moveRange);
        }
    }
}