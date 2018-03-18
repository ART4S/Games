using System;
using System.Drawing;

namespace AirForce
{
    public partial class CollisionHandler
    {
        public static bool IsIntersects(FlyingObject objA, FlyingObject objB)
        {
            return IsIntersects(objA.Position, objA.Radius, objB.Position, objB.Radius);
        }

        public static bool IsIntersects(Point2D firstPosition, int firstRadius, Point2D secondPosition, int secondRadius)
        {
            return Math.Pow(firstPosition.X - secondPosition.X, 2) +
                   Math.Pow(firstPosition.Y - secondPosition.Y, 2) <=
                   Math.Pow(firstRadius + secondRadius, 2); // пересечение окружностей
        }

        public static bool IsInFront(FlyingObject self, FlyingObject other)
        {
            return IsInFront(self.Position, self.Radius, other.Position, other.Radius);
        }

        public static bool IsInFront(Point2D firstPosition, int firstRadius, Point2D secondPosition, int secondRadius)
        {
            int firstTopBorder = firstPosition.Y - firstRadius;
            int firstBottomBorder = firstPosition.Y + firstRadius;
            int firstRightBorder = firstPosition.X + firstRadius;

            int secondTopBorder = secondPosition.Y - secondRadius;
            int secondBottomBorder = secondPosition.Y + secondRadius;
            int secondLerftBorder = secondPosition.X - secondRadius;

            bool isHaveMutualX = firstRightBorder <= secondLerftBorder;
            bool isHaveMutualY = Math.Max(firstTopBorder, secondTopBorder) <= Math.Min(firstBottomBorder, secondBottomBorder);

            return isHaveMutualX && isHaveMutualY;
        }

        public static bool IsOutOfFieldLeftBorder(FlyingObject self, Field field)
        {
            return IsOutOfFieldLeftBorder(self.Position, self.Radius, field);
        }

        public static bool IsOutOfFieldLeftBorder(Point2D position, int radius, Field field)
        {
            return position.X + radius < field.TopLeftPoint.X;
        }

        public static bool IsOutOfFieldRightBorder(FlyingObject self, Field field)
        {
            return IsOutOfFieldRightBorder(self.Position, self.Radius, field);
        }

        public static bool IsOutOfFieldRightBorder(Point2D position, int radius, Field field)
        {
            return position.X - radius >= field.TopRightPoint.X;
        }

        public static bool IsIntersectFieldTopBorder(Point2D position, int radius, Field field)
        {
            return position.Y - radius < field.TopLeftPoint.Y;
        }

        public static bool IsIntersectGround(FlyingObject self, Ground ground)
        {
            return IsIntersectGround(self.Position, self.Radius, ground);
        }

        public static bool IsIntersectGround(Point2D position, int radius, Ground ground)
        {
            Rectangle objRectangle = new Rectangle(
                position - new Point2D(radius, radius),
                new Size(2 * radius, 2 * radius));

            Rectangle groundRectangle = new Rectangle(ground.Position, ground.Size);

            return objRectangle.IntersectsWith(groundRectangle);
        }

        public static bool IsOutOfField(Point2D position, int radius, Field field)
        {
            Rectangle objRectangle = new Rectangle(
                position - new Point2D(radius, radius),
                new Size(2 * radius, 2 * radius));

            return !objRectangle.IntersectsWith(field);
        }

        public static bool IsEntirelyOnField(Point2D position, int radius, Field field)
        {
            Rectangle objRectangle = new Rectangle(
                position - new Point2D(radius, radius),
                new Size(2 * radius, 2 * radius));

            Rectangle fieldRectangle = field;

            return fieldRectangle.Contains(objRectangle);
        }
    }
}