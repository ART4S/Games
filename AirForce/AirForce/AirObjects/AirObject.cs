using System.Collections.Generic;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point2D Position { get; protected set; }
        public int Durability { get; protected set; } = 1;
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

        public abstract void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects);
        public abstract void CollisionWithOtherAirObject(AirObject otherAirObject);

        public void Draw(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle
            {
                Location = new Point2D(Position.X - Radius, Position.Y - Radius),
                Size = new Size(2 * Radius, 2 * Radius)
            };

            graphics.DrawImage(Image, imageRectangle);
        }

        protected bool IsPositionOutOfGameFieldLeftBorder(Point2D position)
        {
            return position.X + Radius < 0;
        }

        protected bool IsPositionOutOfGameFieldRightBorder(Point2D position, Size gameFieldSize)
        {
            return position.X - Radius > gameFieldSize.Width;
        }

        protected bool IsPositionAboveGroundLine(Point2D position, Line groundlLine)
        {
            return position.Y + Radius < groundlLine.FirstPoint.Y ||
                   position.Y + Radius < groundlLine.SecondPoint.Y;
        }
    }
}
