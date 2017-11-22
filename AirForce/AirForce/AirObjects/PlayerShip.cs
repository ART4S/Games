using System;
using System.Drawing;
using AirForce.AirObjects.Bullets;
using AirForce.AirObjects.EnemyAI;

namespace AirForce.AirObjects
{
    public sealed class PlayerShip : AirObject
    {
        private event Action PlayerShipDeathEvent;

        private int strength = 5;
        public int Strength
        {
            get => strength;
            private set
            {
                strength = value;

                if (strength <= 0)
                    OnPlayerShipDeathEvent();
            }
        }

        public PlayerShip(Point2D startPosition, int radius, int movespeedShift, Action playerShipDeathMethod)
            : base(startPosition, radius, movespeedShift, Properties.Resources.player_ship)
        {
            PlayerShipDeathEvent += playerShipDeathMethod;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case BigShip _:
                    Strength -= 2;
                    break;

                case Bird _:
                case ChaserShip _:
                case EnemyBullet _:
                    Strength--;
                    break;

                case Meteor _:
                    Strength = 0;
                    break;
            }
        }

        public void Move(int movespeedModiferX, int movespeedModiferY, Size spaceSize, Line groundLine)
        {
            Point2D nextPosition = new Point2D
            {
                X = Position.X + MovespeedShift * movespeedModiferX,
                Y = Position.Y + MovespeedShift * movespeedModiferY
            };

            if (IsNextPositionAreBeingInSpace(nextPosition, spaceSize))
                Position = nextPosition;

            if (!IsNextPositionAboveGroundLine(nextPosition, groundLine))
                Strength = 0;
        }

        public bool IsInFrontAirObject(AirObject airObject)
        {
            int playerTopBorderY = Position.Y - Radius;
            int playerBottomBorderY = Position.Y + Radius;

            int airObjectTopBorderY = airObject.Position.Y - airObject.Radius;
            int airObjectBottomBorderY = airObject.Position.Y + airObject.Radius;

            return Position.X < airObject.Position.X
                   && Math.Max(airObjectTopBorderY, playerTopBorderY) < Math.Min(airObjectBottomBorderY, playerBottomBorderY);
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

        public void Restore(Point2D position)
        {
            Position = position;
            Strength = 5;
        }
    }
}
