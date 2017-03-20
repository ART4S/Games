using System.Drawing;

namespace Paint
{
    public class Circle
    {
        private readonly int radius;
        private readonly Point middlePoint;

        private readonly TextureBrush fillingBrush;
        private readonly Pen pen;

        public Circle(Point middlePoint, int radius, Pen pen, TextureBrush fillingBrush)
        {
            this.middlePoint = middlePoint;
            this.radius = radius;

            this.fillingBrush = fillingBrush;
            this.pen = pen;

        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawEllipse(pen, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
            graphics.FillEllipse(fillingBrush, middlePoint.X - radius, middlePoint.Y - radius, 2 * radius, 2 * radius);
        }
    }
}