using System.Drawing;

namespace AirForce
{
    public class Ground
    {
        public Point2D Position { get; }
        public Size Size { get; }

        public Ground(Point2D position, Size size)
        {
            Position = position;
            Size = size;
        }

        public void Paint(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Green, new Rectangle(Position, Size));
        }
    }
}