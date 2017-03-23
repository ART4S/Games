using System.Drawing;

namespace Paint.GraphicObjects
{
    public class Circle : IGraphicObject
    {
        private PointF middlePoint;
        private readonly int radius;

        private readonly Pen pen;
        private readonly TextureBrush textureBrush;

        public Circle(PointF middlePoint, int radius, Pen pen, TextureBrush textureBrush)
        {
            this.middlePoint = middlePoint;
            this.radius = radius;
            this.pen = pen;
            this.textureBrush = textureBrush;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawEllipse(pen, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
            graphics.FillEllipse(textureBrush, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
        }

        public void Move(MoveDirection direction, int moveRange)
        {
            middlePoint = PointMover.GetMovedPoint(middlePoint, direction, moveRange);
        }
    }
}