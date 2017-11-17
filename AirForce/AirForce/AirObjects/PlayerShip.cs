using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public class PlayerShip : AirObject
    {
        public PlayerShip(Size spaceSize)
            : base(new Point(30, spaceSize.Height / 2), 30, 5, 15, Properties.Resources.player_ship)
        {

        }

        public override void BumpWithOtherAirObject(AirObject otherAirObject)
        {
            Strength--;
        }

        public override void Draw(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle(
                new Point(PositionInSpace.X - Radius, PositionInSpace.Y - Radius),
                new Size(2 * Radius, 2 * Radius));

            graphics.DrawImage(Image, imageRectangle);
        }

        public void Move(Direction direction, Size spaceSize, Line groundLine)
        {
            Point nextPositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y);

            switch (direction)
            {
                case Direction.Empty:
                    break;

                case Direction.Up:
                    nextPositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y - SpeedMovingShift);
                    break;

                case Direction.Down:
                    nextPositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y + SpeedMovingShift);
                    break;

                case Direction.Left:
                    nextPositionInSpace = new Point(PositionInSpace.X - SpeedMovingShift, PositionInSpace.Y);
                    break;
                case Direction.Right:
                    nextPositionInSpace = new Point(PositionInSpace.X + SpeedMovingShift, PositionInSpace.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (IsNextPositionAreBeingInSpace(nextPositionInSpace, spaceSize))
                PositionInSpace = nextPositionInSpace;

            if (!IsAboveGroundLine(groundLine))
                OnDeathObjectEvent();
        }

        public void Shoot()
        {

        }
    }
}
