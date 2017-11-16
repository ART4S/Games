using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public partial class PlayerShip : AirObject
    {
        public int Strength { get; private set; }

        public PlayerShip(Point positionInSpace, int collisionRadius, int strength) : base(positionInSpace, collisionRadius)
        {
            Strength = strength;
        }

        public override void BumpWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is Meteor)
                Strength = 0;
            else
                Strength--;
        }

        public void Move(Direction direction)
        {
            const int shift = 15;

            switch (direction)
            {
                case Direction.Empty:
                    break;

                case Direction.Up:
                    PositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y - shift);
                    break;

                case Direction.Down:
                    PositionInSpace = new Point(PositionInSpace.X, PositionInSpace.Y + shift);
                    break;

                case Direction.Left:
                    PositionInSpace = new Point(PositionInSpace.X - shift, PositionInSpace.Y);
                    break;
                case Direction.Right:
                    PositionInSpace = new Point(PositionInSpace.X + shift, PositionInSpace.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
