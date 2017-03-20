using System.Drawing;

namespace Paint
{
    public class Circle : ShapeForDrawing
    {
        private readonly int radius;
        private readonly Point middlePoint;

        public Circle(Point middlePoint, int radius, Pen pen, TextureBrush textruBrush) : base(pen, textruBrush)
        {
            this.middlePoint = middlePoint;
            this.radius = radius;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawEllipse(pen, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
            graphics.FillEllipse(textruBrush, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
        }
    }
}