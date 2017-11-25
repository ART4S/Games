using System;
using System.Collections.Generic;
using System.Drawing;

namespace AirForce.AirObjects
{
    public sealed class Meteor : AirObject
    {
        public Meteor(Point2D position, int radius, int movespeed)
            : base(position, radius, movespeed, Properties.Resources.meteor)
        {
            Durability = new Random().Next(5, 8 + 1);

            if (Durability == 8)
                Image = Properties.Resources.asteroid;
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Position += new Point2D(-2 * Movespeed, Movespeed);

            if (IsPositionOutOfGameFieldLeftBorder(Position) || IsPositionOutOfGroundLine(Position, groundLine))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case PlayerShip _:
                case ChaserShip _:
                case BigShip _:
                case EnemyBullet _:
                case PlayerBullet _:
                    Durability--;
                    break;
            }
        }
    }
}
