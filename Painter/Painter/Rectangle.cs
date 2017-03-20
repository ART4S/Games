using System.Drawing;

namespace Paint
{
    public class Rectangle : DrawingShape
    {
        private readonly Point topLeftPoint;
        private readonly int width;
        private readonly int height;

        public Rectangle(Point topLeftPoint, int width, int height, Pen pen, TextureBrush textruBrush) : base(pen, textruBrush)
        {
            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(pen, topLeftPoint.X, topLeftPoint.Y, width, height);
            graphics.FillRectangle(textruBrush, topLeftPoint.X, topLeftPoint.Y, width, height);
        }
    }
}