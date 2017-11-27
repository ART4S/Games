using System.Drawing;
using System.Collections.Generic;

namespace AirForce.AirObjects
{
    public sealed class BigShip : AirObject
    {
        public BigShip(Point2D position, int radius, int movespeed)
            : base(position, radius, movespeed, Properties.Resources.big_enemy_ship)
        {
            Durability = 3;
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Position -= new Point2D(Movespeed, 0);

            if (IsPositionOutOfGameFieldLeftBorder(Position))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case Meteor _:
                case PlayerShip _:
                    Durability = 0;
                    break;

                case PlayerBullet _:
                    Durability--;
                    break;
            }
        }
    }
}
