using System.Drawing;

namespace AirForce
{
    public struct Rectangle2D
    {
        public Point2D Location { get; }
        public Size2D Size { get; }

        public Rectangle2D(Point2D location, Size2D size)
        {
            Location = location;
            Size = size;
        }

        public void Paint(Graphics graphics, Brush brush)
        {
            graphics.FillRectangle(brush, this);
        }

        public bool IntersectsWith(Rectangle2D rectangle)
        {
            Rectangle thisRectangle = this;

            return thisRectangle.IntersectsWith(rectangle);
        }

        public bool Contains(Rectangle2D rectangle)
        {
            Rectangle thisRectangle = this;

            return thisRectangle.Contains(rectangle);
        }

        public static implicit operator Rectangle(Rectangle2D rectangle)
        {
            return new Rectangle(rectangle.Location, rectangle.Size);
        }

        public static implicit operator RectangleF(Rectangle2D rectangle)
        {
            return new RectangleF(rectangle.Location, rectangle.Size);
        }
    }
}
