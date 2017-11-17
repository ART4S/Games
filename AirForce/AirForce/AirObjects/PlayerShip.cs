using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public partial class PlayerShip : AirObject
    {
        public PlayerShip(Point positionInSpace, int collisionRadius, int strength) : base(positionInSpace, collisionRadius, strength)
        {
        }

        public override void BumpWithOtherAirObject(AirObject otherAirObject)
        {
            Strength--;
        }

        public void Move(Direction direction, Size spaceSize, Line groundLine)
        {
            const int shift = 15;
            Point nextPositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y);

            switch (direction)
            {
                case Direction.Empty:
                    break;

                case Direction.Up:
                    nextPositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y - shift);
                    break;

                case Direction.Down:
                    nextPositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y + shift);
                    break;

                case Direction.Left:
                    nextPositionInSpace = new Point(PositionInSpace.X - shift, PositionInSpace.Y);
                    break;
                case Direction.Right:
                    nextPositionInSpace = new Point(PositionInSpace.X + shift, PositionInSpace.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (IsBodyInSpace(nextPositionInSpace, CollisionRadius, spaceSize))
                PositionInSpace = nextPositionInSpace;

            if (!IsAboveGroundLine(groundLine))
                Strength = 0;
        }

        public void Shoot()
        {

        }
    }
}
