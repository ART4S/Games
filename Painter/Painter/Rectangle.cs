using System.Drawing;

namespace Paint
{
    public class Rectangle
    {
        private readonly Point topLeftPoint;
        private readonly int width;
        private readonly int height;

        private readonly TextureBrush fillingBrush;
        private readonly Pen pen;

        public Rectangle(Point topLeftPoint, int width, int height, Pen pen, TextureBrush fillingBrush)
        {
            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;

            this.pen = pen;
            this.fillingBrush = fillingBrush;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(pen, topLeftPoint.X, topLeftPoint.Y, width, height);
            graphics.FillRectangle(fillingBrush, topLeftPoint.X, topLeftPoint.Y, width, height);
        }
    }
}