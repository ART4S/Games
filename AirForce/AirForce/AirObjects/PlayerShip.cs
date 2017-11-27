using System.Drawing;
using System.Collections.Generic;


namespace AirForce.AirObjects
{
    public sealed class PlayerShip : AirObject
    {
        public PlayerShip(Point2D startPosition, int radius, int movespeed)
            : base(startPosition, radius, movespeed, Properties.Resources.player_ship)
        {
            Durability = 100;
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Move(new Point2D(), gameFieldSize, groundLine);
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case BigShip _:
                    Durability -= 2;
                    break;

                case Meteor _:
                case ChaserShip _:
                case EnemyBullet _:
                    Durability--;
                    break;

                case FlyingSaucer _:
                    Durability -= 5;
                    break;
            }
        }

        public void Move(Point2D movespeedModifer, Size gameFieldSize, Line groundLine)
        {
            Point2D nextPosition = new Point2D
            {
                X = Position.X + Movespeed * movespeedModifer.X,
                Y = Position.Y + Movespeed * movespeedModifer.Y
            };

            if (!IsPositionOutOfGameFieldTopBorder(nextPosition) &&
                !IsPositionOutOfGameFieldBottomBorder(nextPosition, gameFieldSize) &&
                !IsPositionOutOfGameFieldLeftBorder(nextPosition) &&
                !IsPositionOutOfGameFieldRightBorder(nextPosition, gameFieldSize))
                Position = nextPosition;

            if (IsPositionOutOfGroundLine(nextPosition, groundLine))
                Durability = 0;
        }

        public void Refresh(Point2D position, int durability)
        {
            Position = position;
            Durability = durability;
        }
    }
}
