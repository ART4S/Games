using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point PositionInSpace { get; protected set; }
        public int Strength { get; protected set; }
        public int Radius { get; }
        public event Action DeathObjectEvent;

        protected int SpeedMovingShift;
        protected readonly Image Image;

        protected AirObject(Point positionInSpace, int radius, int strength, int speedMovingShift, Image image)
        {
            PositionInSpace = positionInSpace;
            Radius = radius;
            Strength = strength;
            SpeedMovingShift = speedMovingShift;
            Image = image;
        }

        public abstract void BumpWithOtherAirObject(AirObject otherAirObject);

        public abstract void Draw(Graphics graphics);

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
