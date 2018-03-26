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

        public bool IsIntersect(Rectangle2D rectangle)
        {
            Rectangle thisRectangle = this;

            return thisRectangle.IntersectsWith(rectangle);
        }

        public bool IsContains(Rectangle2D rectangle)
        {
            Rectangle thisRectangle = this;

            return thisRectangle.Contains(rectangle);
        }

        public bool IsContains(Circle2D circle)
        {
            Rectangle2D circleToRectangle = new Rectangle2D(
                location: circle.Center - new Point2D(circle.Radius, circle.Radius),
                size: new Size2D(2 * circle.Radius, 2 * circle.Radius));

            return IsContains(circleToRectangle);
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
