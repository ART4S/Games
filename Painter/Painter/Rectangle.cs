using System;
using System.Drawing;

namespace Paint
{
    public class Rectangle : DrawingShape
    {
        private Point topLeftPoint;

        private readonly int width;
        private readonly int height;

        public Rectangle(Point topLeftPoint, int width, int height, Pen pen, TextureBrush textureBrush) : base(pen, textureBrush)
        {
            this.topLeftPoint = topLeftPoint;
            this.width = width;
            this.height = height;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(pen, topLeftPoint.X, topLeftPoint.Y, width, height);
            graphics.FillRectangle(textureBrush, topLeftPoint.X, topLeftPoint.Y, width, height);
        }

        public override void Move(MoveDirrection dirrection, int moveRange)
        {
            Point dirrectionPoint = dirrection.ToPoint();

            topLeftPoint = new Point(topLeftPoint.X + moveRange * dirrectionPoint.X, topLeftPoint.Y + moveRange * dirrectionPoint.Y);
        }
    }
}