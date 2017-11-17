using System;
using System.Drawing;
using AirForce.Enums;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point PositionInSpace { get; protected set; }
        public int Strength { get; protected set; }
        public int Radius { get; }

        protected event Action DeathObjectEvent;
        protected readonly int MovespeedShift;
        protected readonly Image Image;

        protected AirObject(Point positionInSpace, int radius, int strength, int movespeedShift, Image image, Action deathObjectMethod)
        {
            PositionInSpace = positionInSpace;
            Radius = radius;
            Strength = strength;
            MovespeedShift = movespeedShift;
            Image = image;

            DeathObjectEvent += deathObjectMethod;
        }

        public abstract void BumpWithOtherAirObject(AirObject otherAirObject);

        public abstract void Draw(Graphics graphics);

        public abstract void Move(Direction direction, Size spaceSize, Line groundLine);

        protected bool IsNextPositionAreBeingInSpace(Point nextPosition, Size spaceSize)
        {
            bool isUnderTopBorderLine =
                nextPosition.Y - Radius >= 0;

            bool isAboveBottomBorderLine =
                nextPosition.Y + Radius <= spaceSize.Height;

            bool isLeftOfRightBorderLine =
                nextPosition.X + Radius <= spaceSize.Width;

            bool isRightOfLeftBorderLine =
                nextPosition.X - Radius >= 0;

            return isUnderTopBorderLine &&
                   isAboveBottomBorderLine &&
                   isLeftOfRightBorderLine &&
                   isRightOfLeftBorderLine;
        }

        protected bool IsAboveGroundLine(Line groundlLine)
        {
            return PositionInSpace.Y + Radius < groundlLine.FirstPoint.Y ||
                   PositionInSpace.Y + Radius < groundlLine.SecondPoint.Y;
        }

        protected void OnDeathObjectEvent()
        {
            DeathObjectEvent?.Invoke();
        }


    }
}
