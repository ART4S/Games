using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point2D Position { get; protected set; }
        public int Radius { get; }

        protected readonly int MovespeedShift;
        protected Image Image;

        protected AirObject(Point2D position, int radius, int movespeedShift, Image image)
        {
            Position = position;
            Radius = radius;
            MovespeedShift = movespeedShift;
            Image = image;
        }

        public abstract void CollisionWithOtherAirObject(AirObject otherAirObject);

        protected bool IsNextPositionAboveGroundLine(Point2D nextPosition, Line groundlLine)
        {
            return nextPosition.Y + Radius < groundlLine.FirstPoint.Y ||
                   nextPosition.Y + Radius < groundlLine.SecondPoint.Y;
        }

        public void Draw(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle(
                new Point2D(Position.X - Radius, Position.Y - Radius),
                new Size(2 * Radius, 2 * Radius));

            graphics.DrawImage(Image, imageRectangle);
        }
    }
}
