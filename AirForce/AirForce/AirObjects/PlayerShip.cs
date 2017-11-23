using System;
using System.Collections.Generic;
using System.Drawing;

namespace AirForce.AirObjects
{
    public sealed class PlayerShip : AirObject
    {
        public PlayerShip(Point2D startPosition, int radius, int movespeedShift)
            : base(startPosition, radius, movespeedShift, Properties.Resources.player_ship)
        {
            Durability = 5;
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            //Move(0, 0, gameFieldSize, groundLine);
        }

        public void Move(int movespeedModiferX, int movespeedModiferY, Size gameFieldSize, Line groundLine)
        {
            Point2D nextPosition = new Point2D
            {
                X = Position.X + MovespeedShift * movespeedModiferX,
                Y = Position.Y + MovespeedShift * movespeedModiferY
            };

            if (IsNextPositionAreBeingInGameField(nextPosition, gameFieldSize))
                Position = nextPosition;

            if (!IsPositionAboveGroundLine(nextPosition, groundLine))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            //switch (otherAirObject)
            //{
            //    case BigShip _:
            //        Durability -= 2;
            //        break;

            //    case Bird _:
            //    case ChaserShip _:
            //    case EnemyBullet _:
            //        Durability--;
            //        break;

            //    case Meteor _:
            //        Durability = 0;
            //        break;
            //}
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

        public void Restore(Point2D position, int durability)
        {
            Position = position;
            Durability = durability;
        }

        private bool IsNextPositionAreBeingInGameField(Point2D nextPosition, Size gameFieldSize)
        {
            bool isUnderTopBorderLine =
                nextPosition.Y - Radius >= 0;

            bool isAboveBottomBorderLine =
                nextPosition.Y + Radius <= gameFieldSize.Height;

            bool isLeftOfRightBorderLine =
                nextPosition.X + Radius <= gameFieldSize.Width;

            bool isRightOfLeftBorderLine =
                nextPosition.X - Radius >= 0;

            return isUnderTopBorderLine &&
                   isAboveBottomBorderLine &&
                   isLeftOfRightBorderLine &&
                   isRightOfLeftBorderLine;
        }
    }
}
