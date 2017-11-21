namespace AirForce
{
    public struct Line
    {
        public Point2D FirstPoint { get; }
        public Point2D SecondPoint { get; }

        public Line(Point2D firstPoint, Point2D secondPoint)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
        }
    }
}
