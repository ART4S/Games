using System.Drawing;

namespace AirForce
{
    public struct Point2D
    {
        public int X;
        public int Y;

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point(Point2D point2D)
        {
            return new Point(point2D.X, point2D.Y);
        }
    }
}
