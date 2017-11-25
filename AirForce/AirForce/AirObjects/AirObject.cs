using System;
using System.Collections.Generic;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point2D Position { get; protected set; }
        public int Durability { get; protected set; } = 1;
        public int Radius { get; }
        public int Movespeed { get; protected set; }

        protected Image Image;

        protected AirObject(Point2D position, int radius, int movespeed, Image image)
        {
            Position = position;
            Radius = radius;
            Movespeed = movespeed;
            Image = image;
        }

        public abstract void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects);
        public abstract void CollisionWithOtherAirObject(AirObject otherAirObject);

        public void Draw(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle
            {
                Location = Position - new Point2D(Radius, Radius),
                Size = new Size(2 * Radius, 2 * Radius)
            };

            graphics.DrawImage(Image, imageRectangle);
        }

        protected bool IsPositionOutOfGameFieldLeftBorder(Point2D position)
        {
            return position.X - Radius < 0;
        }

        protected bool IsPositionOutOfGameFieldRightBorder(Point2D position, Size gameFieldSize)
        {
            return position.X + Radius > gameFieldSize.Width;
        }

        protected bool IsPositionOutOfGameFieldTopBorder(Point2D position)
        {
            return position.Y - Radius < 0;
        }

        protected bool IsPositionOutOfGameFieldBottomBorder(Point2D position, Size gameFieldSize)
        {
            return position.Y + Radius > gameFieldSize.Height;
        }

        protected bool IsPositionOutOfGroundLine(Point2D position, Line groundlLine)
        {
            return position.Y + Radius >= groundlLine.FirstPoint.Y ||
                   position.Y + Radius >= groundlLine.SecondPoint.Y;
        }
    }
}
