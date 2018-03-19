using System.Drawing;

namespace AirForce
{
    public class Field
    {
        public Point2D TopLeftPoint { get; }
        public Point2D TopRightPoint { get; }
        public Point2D BottomLeftPoint { get; }
        public Point2D BottomRightPoint { get; }
        public Size Size { get; }

        public Field(Point2D position, Size size)
        {
            TopLeftPoint = position;
            TopRightPoint = TopLeftPoint + new Point2D(size.Width, 0);
            BottomLeftPoint = TopLeftPoint + new Point2D(0, size.Height);
            BottomRightPoint = TopLeftPoint + new Point2D(size.Width, size.Height);
            Size = size;
        }

        public static implicit operator Rectangle(Field field)
        {
            return new Rectangle(field.TopLeftPoint, field.Size);
        }
    }
}