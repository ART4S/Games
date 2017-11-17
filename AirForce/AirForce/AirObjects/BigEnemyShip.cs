using System;
using System.Drawing;
using AirForce.Enums;

namespace AirForce.AirObjects
{
    public sealed class BigEnemyShip : AirObject
    {
        public BigEnemyShip(Point positionInSpace, Action deathObjectMethod)
            : base(positionInSpace, 50, Program.Random.Next(5, 9), 2, Properties.Resources.bomber_ship, deathObjectMethod)
        {
        }

        public override void BumpWithOtherAirObject(AirObject otherAirObject)
        {
            Strength--;

            if (Strength == 0)
                OnDeathObjectEvent();
        }

        public override void Draw(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle(
                new Point(PositionInSpace.X - Radius, PositionInSpace.Y - Radius),
                new Size(2 * Radius, 2 * Radius));

            graphics.DrawImage(Image, imageRectangle);
        }

        public override void Move(Direction direction, Size spaceSize, Line groundLine)
        {
            if (PositionInSpace.X + Radius - MovespeedShift >= -MovespeedShift)
                PositionInSpace = new Point(PositionInSpace.X - MovespeedShift, PositionInSpace.Y);
        }
    }
}
