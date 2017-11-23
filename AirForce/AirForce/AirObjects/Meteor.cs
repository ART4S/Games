﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace AirForce.AirObjects
{
    public sealed class Meteor : AirObject
    {
        public Meteor(Point2D position, int radius, int movespeedShift)
            : base(position, radius, movespeedShift, Properties.Resources.meteor)
        {
            Durability = new Random().Next(5, 9);
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Position = new Point2D(Position.X - 2 * MovespeedShift, Position.Y + MovespeedShift);

            if (IsPositionOutOfGameFieldLeftBorder(Position) || !IsPositionAboveGroundLine(Position, groundLine))
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
