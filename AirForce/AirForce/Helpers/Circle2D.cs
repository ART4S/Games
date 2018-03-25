using System;

namespace AirForce
{
    public struct Circle2D
    {
        public Point2D Center { get; }
        public int Radius { get; }

        public Circle2D(Point2D center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool IsCircleInBack(Circle2D circle)
        {
            int thisTopY = Center.Y - Radius;
            int thisBottomY = Center.Y + Radius;
            int thisLerftX = Center.X - Radius;

            int circleTopY = circle.Center.Y - circle.Radius;
            int circleBottomY = circle.Center.Y + circle.Radius;
            int circleRightX = circle.Center.X + circle.Radius;

            bool isHaveMutualX = circleRightX < thisLerftX;
            bool isHaveMutualY = Math.Max(circleTopY, thisTopY) < Math.Min(circleBottomY, thisBottomY);

            return isHaveMutualX && isHaveMutualY;
        }

        public bool IsIntersect(Circle2D circle)
        {
            return Math.Pow(Center.X - circle.Center.X, 2) +
                   Math.Pow(Center.Y - circle.Center.Y, 2) <=
                   Math.Pow(Radius + circle.Radius, 2);
        }

        public bool IsIntersect(Rectangle2D rectangle)
        {
            Rectangle2D thisRectangle = new Rectangle2D(
                location: Center - new Point2D(Radius, Radius),
                size: new Size2D(2 * Radius, 2 * Radius));

            return thisRectangle.IsIntersect(rectangle);
        }
    }
}