using System;

namespace AirForce
{
    public partial class CollisionHandler
    {
        public static bool IsIntersects(FlyingObject objA, FlyingObject objB)
        {
            return IsIntersects(objA.Position, objA.Radius, objB.Position, objB.Radius);
        }

        public static bool IsIntersects(Point2D positionA, int radiusA, Point2D positionB, int radiusB)
        {
            return Math.Pow(positionA.X - positionB.X, 2) +
                   Math.Pow(positionA.Y - positionB.Y, 2) <=
                   Math.Pow(radiusA + radiusB, 2); // пересечение окружностей
        }

        public static bool IsInFront(FlyingObject source, FlyingObject other)
        {
            return IsInFront(source.Position, source.Radius, other.Position, other.Radius);
        }

        public static bool IsInFront(Point2D positionA, int radiusA, Point2D positionB, int radiusB)
        {
            int aTopBorder = positionA.Y - radiusA;
            int aBottomBorder = positionA.Y + radiusA;
            int aRightBorder = positionA.X + radiusA;

            int bTopBorder = positionB.Y - radiusB;
            int bBottomBorder = positionB.Y + radiusB;
            int bLerftBorder = positionB.X - radiusB;

            bool isHaveMutualX = aRightBorder <= bLerftBorder;
            bool isHaveMutualY = Math.Max(aTopBorder, bTopBorder) <= Math.Min(aBottomBorder, bBottomBorder);

            return isHaveMutualX && isHaveMutualY;
        }

        public static bool IsOutOfFieldLeftBorder(Point2D position, int radius, Rectangle2D field)
        {
            return position.X + radius <= field.Location.X;
        }

        public static bool IsIntersectFieldTopBorder(Point2D position, int radius, Rectangle2D field)
        {
            return position.Y - radius < field.Location.Y;
        }

        public static bool IsIntersectGround(FlyingObject source, Rectangle2D ground)
        {
            return IsIntersectGround(source.Position, source.Radius, ground);
        }

        public static bool IsIntersectGround(Point2D position, int radius, Rectangle2D ground)
        {
            Rectangle2D objRectangle = new Rectangle2D(
                location: position - new Point2D(radius, radius),
                size: new Size2D(2 * radius, 2 * radius));

            return objRectangle.IntersectsWith(ground);
        }

        public static bool IsOutOfField(FlyingObject source, Rectangle2D field)
        {
            return IsOutOfField(source.Position, source.Radius, field);
        }

        public static bool IsOutOfField(Point2D position, int radius, Rectangle2D field)
        {
            Rectangle2D objRectangle = new Rectangle2D(
                location: position - new Point2D(radius, radius),
                size: new Size2D(2 * radius, 2 * radius));

            return !objRectangle.IntersectsWith(field);
        }

        public static bool IsEntirelyOnField(Point2D position, int radius, Rectangle2D field)
        {
            Rectangle2D objRectangle = new Rectangle2D(
                location: position - new Point2D(radius, radius),
                size: new Size2D(2 * radius, 2 * radius));

            return field.Contains(objRectangle);
        }
    }
}