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

        public static implicit operator Point(Point2D point)
        {
            return new Point(point.X, point.Y);
        }

        public static implicit operator PointF(Point2D point)
        {
            return new PointF(point.X, point.Y);
        }

        public static Point2D operator +(Point2D pointA, Point2D pointB)
        {
            return new Point2D(pointA.X + pointB.X, pointA.Y + pointB.Y);
        }

        public static Point2D operator -(Point2D pointA, Point2D pointB)
        {
            return new Point2D(pointA.X - pointB.X, pointA.Y - pointB.Y);
        }

        public static bool operator ==(Point2D pointA, Point2D pointB)
        {
            return pointA.X == pointB.X && pointA.Y == pointB.Y;
        }

        public static bool operator !=(Point2D pointA, Point2D pointB)
        {
            return !(pointA == pointB);
        }
    }
}
