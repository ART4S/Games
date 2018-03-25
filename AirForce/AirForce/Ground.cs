using System.Drawing;

namespace AirForce
{
    public class Ground
    {
        public Point2D Location { get; }
        public Size Size { get; }

        public Ground(Point2D location, Size size)
        {
            Location = location;
            Size = size;
        }

        public void Paint(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Green, new Rectangle(Location, Size));
        }
    }
}