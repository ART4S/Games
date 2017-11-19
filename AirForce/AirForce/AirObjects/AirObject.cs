using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point PositionInSpace { get; protected set; }
        public int Strength { get; protected set; }
        public int Radius { get; }

        protected readonly int MovespeedShift;
        protected readonly Image Image;

        protected AirObject(Point positionInSpace, int radius, int strength, int movespeedShift, Image image)
        {
            PositionInSpace = positionInSpace;
            Radius = radius;
            Strength = strength;
            MovespeedShift = movespeedShift;
            Image = image;
        }

        public abstract void CollisionWithOtherAirObject(AirObject otherAirObject);

        protected bool IsAboveGroundLine(Line groundlLine)
        {
            return PositionInSpace.Y + Radius < groundlLine.FirstPoint.Y ||
                   PositionInSpace.Y + Radius < groundlLine.SecondPoint.Y;
        }

        public void Draw(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle(
                new Point(PositionInSpace.X - Radius, PositionInSpace.Y - Radius),
                new Size(2 * Radius, 2 * Radius));

            graphics.DrawImage(Image, imageRectangle);
        }
    }
}
