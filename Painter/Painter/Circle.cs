using System.Drawing;

namespace Paint
{
    public class Circle : DrawingShape
    {
        private readonly int radius;
        private readonly Point middlePoint;

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
    }
}