using System.Drawing;

namespace Painter
{
    public class Circle : DrawingShape
    {
        private readonly int radius;
        private Point middlePoint;

        public Circle(Point middlePoint, int radius, Pen pen, TextureBrush textureBrush) : base(pen, textureBrush)
        {
            this.middlePoint = middlePoint;
            this.radius = radius;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawEllipse(pen, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
            graphics.FillEllipse(textureBrush, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
        }

        public override void Move(MoveDirrection dirrection, int moveRange)
        {
            Point dirrectionPoint = dirrection.ToPoint();

            middlePoint = new Point(middlePoint.X + moveRange * dirrectionPoint.X, middlePoint.Y + moveRange * dirrectionPoint.Y);
        }
    }
}