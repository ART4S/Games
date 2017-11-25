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

        public static Point2D operator +(Point2D point1, Point2D point2)
        {
            return new Point2D(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point2D operator -(Point2D point1, Point2D point2)
        {
            return new Point2D(point1.X - point2.X, point1.Y - point2.Y);
        }

        public static Point2D operator *(Point2D point1, Point2D point2)
        {
            return new Point2D(point1.X * point2.X, point1.Y * point2.Y);
        }

        public static bool operator ==(Point2D point1, Point2D point2)
        {
            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static bool operator !=(Point2D point1, Point2D point2)
        {
            return !(point1 == point2);
        }
    }
}
