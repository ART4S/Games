using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point PositionInSpace { get; protected set; }
        public int Strength { get; protected set; }
        public int CollisionRadius { get; }

        protected AirObject(Point positionInSpace, int collisionRadius, int strength)
        {
            PositionInSpace = positionInSpace;
            CollisionRadius = collisionRadius;
            Strength = strength;
        }

        public abstract void BumpWithOtherAirObject(AirObject otherAirObject);

        protected bool IsBodyInSpace(Point point, int collisionRadius, Size spaceSize)
        {
            bool isUnderTopBorderLine =
                point.Y - collisionRadius >= 0;

            bool isAboveBottomBorderLine =
                point.Y + collisionRadius <= spaceSize.Height;

            bool isLeftOfRightBorderLine =
                point.X + collisionRadius <= spaceSize.Width;

            bool isRightOfLeftBorderLine =
                point.X - collisionRadius >= 0;

            return isUnderTopBorderLine &&
                   isAboveBottomBorderLine &&
                   isLeftOfRightBorderLine &&
                   isRightOfLeftBorderLine;
        }

        protected bool IsAboveGroundLine(Line groundlLine)
        {
            return PositionInSpace.Y + CollisionRadius < groundlLine.FirstPoint.Y ||
                   PositionInSpace.Y + CollisionRadius < groundlLine.SecondPoint.Y;
        }
    }
}
