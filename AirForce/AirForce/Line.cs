using System.Drawing;

namespace AirForce
{
    public sealed class Line
    {
        public Point FirstPoint { get; }
        public Point SecondPoint { get; }

        public Line(Point firstPoint, Point secondPoint)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
        }
    }
}
