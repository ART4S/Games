using System;
using System.Drawing;
using AirForce.AirObjects.Bullets;
using AirForce.AirObjects.EnemyAI;
using AirForce.Enums;

namespace AirForce.AirObjects
{
    public sealed class PlayerShip : AirObject
    {
        private event Action PlayerShipDeathEvent;

        private int strength = 5;

        public PlayerShip(Size spaceSize, Action playerShipDeathMethod)
            : base(new Point2D(30, spaceSize.Height / 2), 30, 15, Properties.Resources.player_ship)
        {
            PlayerShipDeathEvent += playerShipDeathMethod;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case BigShip _:
                    strength -= 2;
                    break;

                case EnemyBullet _:
                    strength--;
                    break;
            }

            //if (strength <= 0)
            //    OnPlayerShipDeathEvent();
            strength = 0;
        }

        public void Move(Direction direction, Size spaceSize, Line groundLine)
        {
            Point2D nextPosition;

            switch (direction)
            {
                case Direction.Up:
                    nextPosition = new Point2D(Position.X, Position.Y - MovespeedShift);
                    break;
                case Direction.Down:
                    nextPosition = new Point2D(Position.X, Position.Y + MovespeedShift);
                    break;
                case Direction.Left:
                    nextPosition = new Point2D(Position.X - MovespeedShift, Position.Y);
                    break;
                case Direction.Right:
                    nextPosition = new Point2D(Position.X + MovespeedShift, Position.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (IsNextPositionAreBeingInSpace(nextPosition, spaceSize))
                Position = nextPosition;

            if (!IsNextPositionAboveGroundLine(nextPosition, groundLine))
                OnPlayerShipDeathEvent();
        }

        private bool IsNextPositionAreBeingInSpace(Point2D nextPosition, Size spaceSize)
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

        private void OnPlayerShipDeathEvent()
        {
            Position = new Point2D(-200, -200);

            PlayerShipDeathEvent?.Invoke();
        }

        public bool IsInFrontAirObject(AirObject airObject)
        {
            int thisTopBorderY = Position.Y - Radius;
            int thisBottomBorderY = Position.Y + Radius;

            int airObjectTopBorderY = airObject.Position.Y - airObject.Radius;
            int airObjectBottomBorderY = airObject.Position.Y + airObject.Radius;

            return Position.X < airObject.Position.X
                   && Math.Max(airObjectTopBorderY, thisTopBorderY) < Math.Min(airObjectBottomBorderY, thisBottomBorderY);
        }
    }
}
